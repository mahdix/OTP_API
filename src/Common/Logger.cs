using System;
using System.IO;

namespace OTP_API.Common
{
	public class Logger
	{
		public static void LogFailedLogin(string user, string pass)
		{
			using (StreamWriter sw = File.AppendText("log.txt")) 
			{
				sw.WriteLine(String.Format("{0:MM/dd/yy HH:mm:ss}: Login failed for {1} with password = {2}",
				            DateTime.Now, user, pass));
			}	
		}

		public static void LogSuccessfulLogin(string user, string pass)
		{
			using (StreamWriter sw = File.AppendText("log.txt")) 
			{
				sw.WriteLine(String.Format("{0:MM/dd/yy HH:mm:ss}: Login successful for {1} with password = {2}",
				            DateTime.Now, user, pass));
			}	
		}
	}
}


