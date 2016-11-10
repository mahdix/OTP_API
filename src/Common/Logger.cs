using System;
using System.IO;

namespace OTP_API.Common
{
	public class Logger
	{
		public static void LogResult(string user, string pass, bool success)
		{
			using (StreamWriter sw = File.AppendText("log.txt")) 
			{
			    string successString = "successful";

			    if ( !success ) successString = "failed";

				sw.WriteLine(String.Format("{0:MM/dd/yy HH:mm:ss}: Login {3} for {1} with password = {2}",
				            DateTime.Now, user, pass, successString));
			}	
		}
    }
}


