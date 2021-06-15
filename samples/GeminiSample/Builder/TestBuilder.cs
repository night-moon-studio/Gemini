using GeminiSample.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiSample.Builder
{
    public class TestBuilder : IGeminiBuilder<ParentsOptions>
    {
        protected override void ConfigFunctions()
        {
            Debug.WriteLine(_options.Description);
        }


        public void ConfigUrl(string x)
        {
            Debug.WriteLine(_options.Url);
        }
    }
}
