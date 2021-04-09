using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GlobalizationLocalization.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IStringLocalizer<PersonController> _localizer;
        public PersonController(IStringLocalizer<PersonController> localizer)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var guid = Guid.NewGuid();
            return Ok(_localizer["name", guid.ToString()].Value);
        }
    }
}