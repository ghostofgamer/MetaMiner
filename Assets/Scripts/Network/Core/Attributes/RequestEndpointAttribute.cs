using System;

namespace MetaMiners.Network.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequestEndpointAttribute : Attribute
    {
        public string Endpoint { get; }
        public BackendService.RequestMethod Method { get; }

        public RequestEndpointAttribute(string endpoint, BackendService.RequestMethod method)
        {
            Endpoint = endpoint;
            Method = method;
        }
    }
}