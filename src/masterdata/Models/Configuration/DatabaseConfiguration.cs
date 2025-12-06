namespace masterdata.Models.Configuration
{
    public class DatabaseConfiguration
    {
        private string _ConnectionString;
        public string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(this._ConnectionString))
                {
                    return string.Format(_ConnectionString, Host, Database, Username, Password, Schema);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this._ConnectionString = value;
            }
        }

        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Schema { get; set; }
    }
}