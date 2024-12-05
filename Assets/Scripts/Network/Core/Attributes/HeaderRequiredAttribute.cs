using System;

namespace MetaMiners.Network.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class HeaderRequiredAttribute : Attribute
    {
        public string HeaderKey { get; }
        public bool CanGetFromDataStorage { get; }

        public HeaderRequiredAttribute(string headerKey, bool canGetFromDataStorage = false)
        {
            HeaderKey = headerKey;
            CanGetFromDataStorage = canGetFromDataStorage;
        }
    }
}