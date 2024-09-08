using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OstrataTemplate._1
{
    public partial class PluginBase
    {
        public static class ExecutionStage
        {
            public static readonly int PreValidation = 10;
            public static readonly int PreOperation = 20;
            public static readonly int PostOperation = 40;
        }

        public static class ExecutionMode
        {
            public static readonly int Synchronous = 0;
            public static readonly int Asynchronous = 1;
        }

        public static class Messages
        {
            public static readonly string Update = "Update";
            public static readonly string Create = "Create";
            public static readonly string Delete = "Delete";
            public static readonly string Retrieve = "Retrieve";
        }

    }
}
