using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Services
{
    public class LocalMailService : IMailServices
    {
        public string From = Startup.Configuaration["mailSettings:mailFromAddress"];
        public string To = Startup.Configuaration["mailSettings:mailToAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail Send From: {From} to {To} this is LocalMailService");
            Debug.WriteLine($"With the subject: {subject}");
            Debug.WriteLine($"with Message: {message}");
        }
    }
}
