using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Server;

using Scripts.Zulu.Utilities;
using Server.Engines.Magic.HitScripts;
using Server.Misc;

namespace Scripts.Cue
{
    public static class CueHelpers
    {
        public record ReleasesJson
        {
            public Asset[] Assets { get; init; }
            public record Asset
            {
                [JsonPropertyName("browser_download_url")]
                public string Url { get; init; }
            }
        } 
        
        public static void DownloadCueCli(string destination, string fileName)
        {
            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("https://api.github.com/repos/cue-lang/cue/releases/latest").Result;
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadFromJsonAsync<ReleasesJson>().Result;

                var arch = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "arm64" : "amd64";

                var asset = data?.Assets.FirstOrDefault(a =>
                {
                    if (!a.Url.Contains(arch, StringComparison.InvariantCultureIgnoreCase))
                        return false;
                    
                    if (Core.IsWindows && a.Url.Contains("windows", StringComparison.InvariantCultureIgnoreCase))
                        return true;
                    if (Core.IsDarwin && a.Url.Contains("darwin", StringComparison.InvariantCultureIgnoreCase))
                        return true;
                    if (Core.IsLinux && a.Url.Contains("linux", StringComparison.InvariantCultureIgnoreCase))
                        return true;

                    return false;
                });

                if (asset != null)
                {
                    var stream = client.GetStreamAsync(asset.Url).Result;
                    if (asset.Url.Contains(".tar.gz", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Tar.ExtractTarGz(stream, destination);
                    }
                    else if (asset.Url.Contains(".zip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                        {
                            var entry = archive.GetEntry(fileName);
                            entry?.ExtractToFile(Path.Combine(destination, entry.Name));
                        }
                    }
                    stream.Dispose();
                }
            }

            if (!File.Exists(Path.Combine(destination, fileName)))
                throw new ApplicationException($"Failed to extract cue cli to ${destination}");
        }
        
        public static (string stdout, string stderr, int exitCode) RunCueCli(string workingDir, params string[] args)
        {
            var cueFileName = $"cue{(Core.IsWindows ? ".exe" : "")}";
            var cueCliRoot = Path.Combine(Core.BaseDirectory, "Configuration", "cue");
            var cueCliPath = Path.Combine(cueCliRoot, cueFileName);

            if (!File.Exists(cueCliPath))
            {
                Utility.PushColor(ConsoleColor.Yellow);
                Console.Write("CUE cli is missing, downloading latest GitHub release ... ");
                
                DownloadCueCli(cueCliRoot, cueFileName);
                if (Core.IsLinux || Core.IsDarwin) 
                    Process.Start("chmod", $"755 {cueCliPath}");
                
                Utility.PopColor();
                Utility.PushColor(ConsoleColor.Green);
                Console.Write("done; ");
                Utility.PopColor();
                Console.Write("running cli ... ");

            } 
            
            var startInfo = new ProcessStartInfo
            {
                Arguments = string.Join(" ", args),
                FileName = cueCliPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            if (workingDir != null)
                startInfo.WorkingDirectory = workingDir;

            var stdout = string.Empty;
            var stderr = string.Empty;
            var exitCode = 1;

            try
            {
                var proc = new Process { StartInfo = startInfo, };
                proc.Start();
                proc.WaitForExit();
                stdout = proc.StandardOutput.ReadToEnd();
                stderr = proc.StandardError.ReadToEnd();
                exitCode = proc.ExitCode;

                proc.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception running cue cli: {e.Message}");
            }

            return (stdout, stderr, exitCode);
        }

        private static System.Reflection.Assembly Load(string path)
        {
            var runtimeAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");
            var paths = new List<string>(runtimeAssemblies) { path, "ModernUO.dll", "Assemblies/CueFSharp.dll" };

            var resolver = new PathAssemblyResolver(paths);
            var context = new MetadataLoadContext(resolver);

            return context.LoadFromAssemblyPath(path);
        }

        public static void ConvertTypesToCue(string dir, string name, string module, string[] types)
        {
            
            var path = Path.Combine(dir, $"{name}.dll");
            var asm = Load(path);

            var cfg = new CueFSharp.DotnetToCue.Config.Config
            {
                Cue =
                {
                    IgnoreClassMethods = true,
                    ReferenceTypesAsNullable = false,
                    Module =
                    {
                        DomainNamer = Microsoft.FSharp.Core.FuncConvert.FromFunc((Assembly a) => module)
                    }
                }
            };

            cfg.Dotnet.Types.Filter = Microsoft.FSharp.Core.FuncConvert.FromFunc((Type t) => types.Contains(t.FullName));
            cfg.Write.RootDir = "Configuration/Generated";
            cfg.Write.RootModule = $"{module}";

            var registry = CueFSharp.DotnetToCue.Register.Registry.New(cfg);

            registry.AddTypeAlias(typeof(Type).FullName, "string | null");
            registry.AddTypeAlias(typeof(TimeSpan).FullName, @"=~ ""^[0-9]+:[0-9]+:[0-9]+$""");
            registry.AddTypeAlias(typeof(Body).FullName, @"int & >= 0");
            registry.AddTypeAlias(typeof(Poison).FullName, string.Join(" | ", Poison.Poisons.Select(p => $"\"{p.Name}\"")) + " | null");
            registry.AddTypeAlias(typeof(InhumanSpeech).FullName, @"string | null");

            var weaponAbilities = typeof(WeaponAbility)
                .GetInheritedClasses()
                .Select(t => $"\"{t.Name}\"")
                .ToList();

            weaponAbilities.Add(
                "{\n" +
                "    Type: \"SpellStrike\" \n" +
                "    SpellType: string \n" +
                "}\n"
            );

            registry.AddTypeAlias(typeof(WeaponAbility).FullName, string.Join(" | \n", weaponAbilities));

            registry.Assembly(asm);
            registry.Write();
        }
    }
}