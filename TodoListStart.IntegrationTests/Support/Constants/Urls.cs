namespace TodoListStart.IntegrationTests.Support.Constants
{
    public static class Urls
    {
        //
        public static string PROTOCOL = "http";
        public static string PORT = "5000";
        public static string HOST = $"{PROTOCOL}://localhost:{PORT}";
        public static string API = "api/v1";
        //
        public static string NOTE_CONTROLLER = $"/{API}/note";
        public static string LISTNOTE_CONTROLLER = $"/{API}/listnote";
        //
        public static string REGISTRATION = $"/account/registration";
        public static string SIGNIN = $"/account/signin";
        public static string SIGNOUT = $"/account/signout";
    }
}
