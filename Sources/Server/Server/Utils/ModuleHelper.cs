using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Utils
{
    public class ModuleHelper
    {
        public static String ConnectionString { get; set; }
        public static IConfiguration Configuration { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }
        public static IApplicationBuilder ApplicationBuilder { get; set; }
        public static IHostingEnvironment HostingEnvironment { get; set; }
        public static IServiceScope ServiceScope { get; set; }
    }
}
