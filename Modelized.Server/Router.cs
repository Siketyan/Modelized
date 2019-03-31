using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modelized.Attributes;
using Modelized.Exceptions;
using Modelized.Models;
using Modelized.Server.Loggers;

namespace Modelized.Server
{
    public class Router
    {
        private readonly IDictionary<string, Type> _routes;

        public Router()
        {
            _routes = new Dictionary<string, Type>();

            var types = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .Where(t => typeof(IModel).IsAssignableFrom(t));

            foreach (var type in types)
            {
                var route = type.GetCustomAttribute<RouteAttribute>();
                if (route is null)
                {
                    continue;
                }

                _routes.Add(route.Path, type);
            }
        }

        public Response Route(Request req)
        {
            Response res;

            try
            {
                try
                {
                    var path = req.Uri.LocalPath;
                    Log.Debug($"{req.Client} requested {path}");

                    if (!_routes.ContainsKey(path))
                    {
                        throw new NotFoundException(
                            "No route of the path found"
                        );
                    }

                    var type = _routes[path];
                    var method =
                        typeof(Modelizer)
                            .GetMethod(nameof(Modelizer.Modelize))
                            ?.MakeGenericMethod(type);

                    if (method is null)
                    {
                        throw new InvalidOperationException(
                            "Failed to call the Modelizer."
                        );
                    }

                    res = method.Invoke(this, new object[] { req }) as Response;
                }
                catch (ModelizedException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw new ModelizedException(e);
                }
            }
            catch (ModelizedException e)
            {
                res = new Response<ErrorModel>(
                    new ErrorModel(e),
                    e.Status
                );
            }

            Log.Debug($"Transaction finished by status {(int) res.Status}");
            return res;
        }
    }
}
