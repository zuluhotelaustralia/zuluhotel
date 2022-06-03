using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using Jose;
using Server.Network;
using Server;
using Server.Accounting;
using Server.Logging;
using Server.Mobiles;

namespace ZuluContent.Accounting;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public record TokenAuthJwt
{
    public string Username { get; init; }
    public string Token { get; init; }
    public string UserId { get; init; }
    public string ExternalUsername { get; init; }

    public int Iat { get; init; }
    public int Exp { get; init; }
    public string Iss { get; init; }
    public string Aud { get; init; }
}

public static class TokenAuthHandler
{
    private const string LinkedAuthIdTagName = "token_auth_linked_id";

    private static readonly JwkSet KeySet = new();
    private static readonly ILogger Logger = LogFactory.GetLogger(typeof(TokenAuthHandler));
    private static readonly ConcurrentDictionary<Account, TokenAuthJwt> ValidTokens = new();

    private static readonly byte[] AcceptedResponse = { 0x82, 0x80 };
    private static readonly byte[] SizeExceededResponse = { 0x82, 0x81 };
    private static readonly byte[] MalformedJwtResponse = { 0x82, 0x82 };
    private static readonly byte[] FailedVerifyResponse = { 0x82, 0x83 };
    private static readonly byte[] NoLinkResponse = { 0x82, 0x84 };

    private static readonly string[] JwkSetUrls =
    {
        "http://127.0.0.1:3334/.well-known/jwks.json"
    };
    
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static void Configure()
    {
        FreeshardProtocol.Register(0x80, false, HandleTokenPreAuth);
        CommandSystem.Register("LinkAccount", AccessLevel.Player, LinkAccount_OnCommand);
        DownloadKeys(JwkSetUrls);
    }

    [Usage("LinkAccount <userId>"),
     Description("Links the account for passwordless authentication")]
    private static void LinkAccount_OnCommand(CommandEventArgs e)
    {
        var from = e.Mobile as PlayerMobile;

        if (from?.Account is Account account)
        {
            account.SetTag(LinkedAuthIdTagName, e.GetString(0));
            from.SendMessage($"Account now linked to {account.GetTag(LinkedAuthIdTagName)}");
        }
        else
        {
            e.Mobile.SendMessage("Failed to link account.");
        }
    }
    
    private static long GetUnixNow() => (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
    
    private static void DownloadKeys(string[] paths)
    {
        using var client = new HttpClient();

        foreach (var path in paths)
        {
            try
            {
                var request = client.GetStringAsync(path);
                request.Wait();
                var set = JwkSet.FromJson(request.Result, JWT.DefaultSettings.JsonMapper);

                Logger.Information("Downloaded JwkSet from '{0}' with {1} keys", path, set.Keys.Count);
                KeySet.Keys.AddRange(set);
            }
            catch (Exception e)
            {
                Logger.Error("Failed to download JwkSet from '{0}': {1}", path, e.Message);
            }
        }

        Logger.Information("Loaded {0} total remote keys into KeySet", KeySet.Keys.Count);
    }

    private static void HandleTokenPreAuth(NetState state, CircularBufferReader reader, int packetLength)
    {
        var version = reader.ReadUInt16();
        var length = reader.ReadInt32();

        if (length > 32768 || reader.Remaining < length)
        {
            Logger.Warning("{0}: JWT payload size exceeded '{1}', {2}, {3}", state, length, version, reader.Position);
            state.Send(SizeExceededResponse);
            state.Disconnect("JWT payload size exceeded");
            return;
        }

        TokenAuthJwt jwt;
        try
        {
            jwt = Verify(reader.ReadAscii(length));
        }
        catch (Exception e)
        {
            Logger.Warning("{0}: Malformed JWT received '{1}'", state, e.Message);
            state.Send(MalformedJwtResponse);
            return;
        }
        
        if (jwt == null || jwt.Exp < GetUnixNow())
        {
            Logger.Warning("{0}: JWT token failed verification", state);
            state.Send(FailedVerifyResponse);
            state.Disconnect("JWT auth failed verification");
            return;
        }

        var account = Accounts.GetAccount(jwt.Username);
        var tag = account?.GetTag(LinkedAuthIdTagName);

        if (account == null || tag == null || tag != jwt.UserId)
        {
            Logger.Warning("{0}: No link exists between username '{1}' and userId '{2}' for token '{3}'", state, jwt);
            state.Send(NoLinkResponse);
            // No disconnection, they can attempt normal password login
        }
        else
        {
            ValidTokens.AddOrUpdate(account, jwt, (_, _) => jwt);
#if DEBUG
            Logger.Information("{0}: JWT token accepted '{1}'", state, jwt);
#endif
            state.Send(AcceptedResponse);
            // Awaiting token auth attempt
        }
    }

    private static TokenAuthJwt Verify(string token)
    {
        var headers = JWT.Headers(token);
        if (!headers.TryGetValue("kid", out var kid) || kid is not string tokenKid)
        {
            Logger.Information("Received invalid TokenAuth payload: missing `kid` field");
            return null;
        }

        var publicKey = KeySet.Keys.FirstOrDefault(k => k.KeyId == tokenKid);
        if (publicKey == null)
        {
            Logger.Information("Token received with no matching stored public key: '{0}'", kid);
            return null;
        }

        return JWT.Decode<TokenAuthJwt>(token, publicKey);
    }
    
    public static bool ConsumeLoginToken(Account acct, string password)
    {
        if (ValidTokens.TryGetValue(acct, out var token) && token.Token == password && token.Exp > GetUnixNow())
            return ValidTokens.Remove(acct, out _);
        
        return false;
    }
}