using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Modelized.Attributes;
using Modelized.Enums;
using Modelized.Exceptions;
using Modelized.Models;
using Modelized.Server.Providers;
using Utf8Json;

namespace Modelized.Server
{
    public static class Modelizer
    {
        public static Response Modelize<T>(Request req) where T : IModel
        {
            var type = typeof(T);
            var providerType = type.GetCustomAttribute<ProvideAttribute>()?.ProviderType;
            if (providerType is null)
            {
                throw new InvalidModelException(
                    "No provider found on the model."
                );
            }

            var provider = Activator.CreateInstance(providerType) as IProvider;

            if (req.Method == HttpMethod.Get)
            {
                return provider.Get<T>();
            }

            if (req.Method == HttpMethod.Post)
            {
                return provider.Post<T>(
                    JsonSerializer.Deserialize<T>(req.Payload)
                );
            }

            return new Response(HttpStatusCode.MethodNotAllowed);
        }

        private static Response Get<T>(this IProvider provider) where T : IModel
        {
            return new ListResponse<T>(
                provider.Load<T>().ToArray()
            );
        }

        private static Response Post<T>(this IProvider provider, T model) where T : IModel
        {
            provider.Add(model);
            provider.Save();

            return new Response<T>(model);
        }
    }
}
