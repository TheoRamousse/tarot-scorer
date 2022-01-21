using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    public class SessionController
    {

        private readonly ILogger _logger = null;
        public SessionController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
