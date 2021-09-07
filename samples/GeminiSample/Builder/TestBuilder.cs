using GeminiSample.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeminiSample.Builder
{
    public class TestBuilder : IGeminiBuilder
    {

        public void ConfigClient()
        {
            var url = "Client:Url".ConfigString();
            //XXXClientPool.ConfigGlobalUrl(url);

            

        }

        public override void Configuration()
        {
            var redis = GetOptions<GalaxyRedisOptions>();
            var client = GetOptions<GalaxyOptions>();
            Console.WriteLine("Do sth!");
        }

    }
}
