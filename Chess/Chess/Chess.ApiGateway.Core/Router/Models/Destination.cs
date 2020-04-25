using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.ApiGateway.Core.Router.Models
{
    public class Destination
    {
        /// <summary>
        /// Represents the scheme of the URL(http, https
        /// </summary>
        public string DestinationScheme { get; set; }

        /// <summary>
        /// Represents the path of the destination endpoint
        /// </summary>
        public string EndpointPath { get; set; }

        /// <summary>
        /// Represents the host of the destination url
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Represents the port of the destination url
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Indicates whether the destination endpoint needs authentication
        /// </summary>
        public bool RequiresAuthentication { get; set; }
    }
}
