namespace OnlineExam.Model.ConfigProviders
{
    public class IdentityConfiguration
    {
        public string Key { get; set; }
        public TimeSpan ExpirationMinutes { get; set; }
        public string Audience { get; set; }
    }
}
