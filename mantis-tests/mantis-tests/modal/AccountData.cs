namespace mantis_tests
{
    public class AccountData
    {
        public AccountData(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public AccountData() {}

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}