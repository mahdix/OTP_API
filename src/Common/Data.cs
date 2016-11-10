namespace OTP_API.Common
{
    //Common parameters
    public class Data
    {
        //expiration time of passwords
        //I have hard-coded the 30 seconds here but normally on a big project, this goes
        //to a config file
        public static int TTL = 30;

        public static bool LogFailedLogin = true;
        public static bool LogSuccessfulLogin = true;
    }
}


