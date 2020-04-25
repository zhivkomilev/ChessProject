namespace Chess.ApiGateway.Core.Router.Models
{
    public class Route
    {
        /// <summary>
        /// Contains the api gateway endpoint path without the host and port.
        /// </summary>
        public string EndpointPath { get; set; }

        /// <summary>
        /// Represents the destination endpoint
        /// </summary>
        public Destination Destination { get; set; }
    }
}
