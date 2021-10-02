namespace ZuluContent.Configuration.Types
{
    public record EmailConfiguration
    {
        public bool Enabled { get; init; } = false;
        public string FromAddress { get; init; } = "support@modernuo.com";
        public string FromName { get; init; } = "ModernUO Team";
        public string CrashAddress { get; init; } = "crashes@modernuo.com";
        public string CrashName { get; init; } = "Crash Log";
        public string SpeechLogPageAddress { get; init; } = "support@modernuo.com";
        public string SpeechLogPageName { get; init; } = "GM Support Conversation";
        public string EmailServer { get; init; } = "smtp.gmail.com";
        public int EmailPort { get; init; } = 465;
        public string EmailUsername { get; init; } = "support@modernuo.com";
        public string EmailPassword { get; init; } = "Some Password 123";
        public int EmailSendRetryCount { get; init; } = 5;
        public int EmailSendRetryDelay { get; init; } = 3;
    }
}