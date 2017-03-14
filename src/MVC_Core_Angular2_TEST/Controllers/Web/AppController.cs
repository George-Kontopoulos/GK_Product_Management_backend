using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ACME.Controllers.Web
{
    public class AppController:Controller
    {
        private IConfigurationRoot _config;
       // private ILogger _logger;

        public AppController(IConfigurationRoot config)
        {
            _config = config;
        }

    }
}
