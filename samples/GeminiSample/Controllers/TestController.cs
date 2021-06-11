using GeminiSample.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ParentsOptions _parentsOptions;
        private readonly SubOptions _subOptions1;
        private readonly SubOptions _subOptions2;
        public TestController(IOptionsMonitor<ParentsOptions> parentMonitor,IOptionsMonitor<SubOptions> subMoniter)
        {
            _parentsOptions = parentMonitor.CurrentValue;
            _subOptions1 = subMoniter.GeminiGet("Info1");
            _subOptions2 = subMoniter.Get("Gemini:Info2");
        }

        [HttpGet("1")]
        public string Get1()
        {
            return _parentsOptions.Url + _parentsOptions.Description;
        }

        [HttpGet("2")]
        public string Get2()
        {
            return _subOptions1.Name + _subOptions1.Age;
        }

        [HttpGet("3")]
        public string Get3()
        {
            return _subOptions2.Name + _subOptions2.Age;
        }
    }
}
