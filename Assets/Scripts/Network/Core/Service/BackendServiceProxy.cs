using MetaMiners.Network.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MetaMiners.Network.Core
{
    public class BackendServiceProxy : BackendService
    {
        public void InvokeRequest<T>(string methodName, Action<T> callback = null, Action<Exception> callbackError = null, Dictionary<string, string> headers = null, object body = null)
        {
            StartCoroutine(InvokeRequestCoroutine<T>(methodName, callback: callback, callbackError: callbackError, headers: headers, body: body));
        }

        private IEnumerator InvokeRequestCoroutine<T>(string methodName, Action<T> callback = null, Action<Exception> callbackError = null, Dictionary<string, string> headers = null, object body = null)
        {
            MethodInfo methodInfo = this.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var requestEndpointAttribute = methodInfo?.GetCustomAttribute<RequestEndpointAttribute>();
            var headerRequiredAttributes = methodInfo?.GetCustomAttributes<HeaderRequiredAttribute>();

            if (requestEndpointAttribute == null)
            {
                callbackError?.Invoke(new CustomAttributeFormatException($"RequestEndpointAttribute not found for method {methodName}."));
                yield break;
            }

            if (headerRequiredAttributes != null)
            {
                foreach (var requiredHeader in headerRequiredAttributes)
                {
                    if (headers == null || !headers.ContainsKey(requiredHeader.HeaderKey))
                    {
                        if (!requiredHeader.CanGetFromDataStorage)
                        {
                            callbackError?.Invoke(new InvalidOperationException($"Header '{requiredHeader.HeaderKey}' is required but not found."));
                            yield break;
                        }

                        if (DataStorageProvider.TryGetHeader(requiredHeader.HeaderKey, out string storedHeaderValue))
                        {
                            headers = headers ?? new Dictionary<string, string>();
                            headers[requiredHeader.HeaderKey] = storedHeaderValue;
                        }
                        else
                        {
                            callbackError?.Invoke(new InvalidOperationException($"Header '{requiredHeader.HeaderKey}' is required but not found."));
                            yield break;
                        }
                    }
                }
            }

            yield return SendRequest(requestEndpointAttribute.Endpoint, requestEndpointAttribute.Method, callback: callback, callbackError: callbackError, headers: headers, body: body);
        }
    }
}