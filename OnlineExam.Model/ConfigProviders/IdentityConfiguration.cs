namespace OnlineExam.Model.ConfigProviders
{
    public class IdentityConfiguration
    {
        string _tokenEncryptingKey { get; set; }

        public string TokenSigningKey { get; set; }
        public string TokenEncryptingKey {
            get => _tokenEncryptingKey;
            set
            {
                if (value.Length != 16)
                    throw new ArgumentException($"the length of {nameof(TokenEncryptingKey)} must be 16 characters");

                _tokenEncryptingKey = value;
            }
        }
        public TimeSpan ExpirationMinutes { get; set; }
        public string Audience { get; set; }
    }
}
