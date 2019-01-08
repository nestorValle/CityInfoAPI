using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Services
{
    public class CloudMailService: IMailServices
    {
        public string From = "dan@gmail.com";
        public string To = "not@gmail.com";

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail Send From: {From} to {To} this is CloudEmailService");
            Debug.WriteLine($"With the subject: {subject}");
            Debug.WriteLine($"with Message: {message}");
        }
    }
}
