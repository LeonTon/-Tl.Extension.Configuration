using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tl.Extension.Configuration.CustomConfiguation;

namespace Microsoft.Extensions.Configuration
{

    public static class CustomConfigSourceExtension
    {
        public static IConfigurationBuilder AddCustomConfig(this IConfigurationBuilder builder, IDictionary<string, string> initialSettings = null)
        {
            var source = new CustomConfigSource();
            builder.Add(source);
            return builder;
        }
    }
}
