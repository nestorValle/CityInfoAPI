﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Services
{
    public interface IMailServices
    {
        void Send(string subject, string message);
    }
}
