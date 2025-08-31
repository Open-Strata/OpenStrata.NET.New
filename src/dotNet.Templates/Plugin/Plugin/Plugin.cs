using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstrataTemplate._1
{
    public class Plugin : PluginBase
    {

        public Plugin(string unsecureConfiguration, string secureConfiguration) : base(typeof(Plugin))
        {
        }

        protected override void ExecuteDataversePlugin(ILocalPluginContext lpc)
        {
        }
    }
}