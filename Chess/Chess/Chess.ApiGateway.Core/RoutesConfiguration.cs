using Chess.ApiGateway.Core.Router.Models;
using System.Collections.Generic;

namespace Chess.ApiGateway.Core
{
    public class RoutesConfiguration
    {
        /// <summary>
        /// Represents a collection of the routes in the configuration.json
        /// </summary>
        public IEnumerable<Route> Routes{ get; set; }

        /// <summary>
        /// Represents the route to the authentication service
        /// </summary>
        public Route AuthenticationService { get; set; }
    }
}
