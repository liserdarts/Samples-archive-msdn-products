namespace SampleUtils
{
    using System;
    using Microsoft.Rtc.Sip;

    public class AppUtils {
        //
        // Static function to connect to Server
        //
        public static ServerAgent ConnectToServer(object app, string amFile)
        {
            try {
                ServerAgent.WaitForServerAvailable(3);      // 3 Attempts
            }
            catch (Exception e1) {
                Console.WriteLine("ERROR: Server unavailable - {0}", e1.Message);
                if (e1 is UnauthorizedException) {
                    Console.WriteLine("must be running under an account that is a member of the \"RTC Server Applications\" local group");
                }
                return null;
            }


            ApplicationManifest am = ApplicationManifest.CreateFromFile(amFile);
            if (am == null) {
                Console.WriteLine("ERROR: {0} application manifest file not found.", amFile);
                return null;
            }

            try {
                am.Compile();
            }
            catch (CompilerErrorException e2) {
                Console.WriteLine("ERROR: {0} application manifest file contained errors:", amFile);
                foreach( string message in e2.ErrorMessages) {
                    Console.WriteLine(message);
                }
                return null;
            }

            try {
                ServerAgent agent = new ServerAgent(app, am);
                return agent;
            }
            catch (Exception e3) {
                Console.WriteLine("ERROR: Unable to connect to server - {0}", e3.Message);
                return null;
            }
        }
    }
}
