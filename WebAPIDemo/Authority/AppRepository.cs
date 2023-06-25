namespace WebAPIDemo.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "53D3C1E6-4587-4AD5-8C6E-A8E4BD59940E",
                Secret = "0673FC70-0514-4011-B4A3-DF9BC03201BC",
                Scopes = "read,write"
            }
        };        

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
