using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DependenciesInjection.Models
{
    public static class DependenciesInjection
    {
        public  static void Injection(IServiceCollection services)
        {
            var jsonServices = JObject.Parse(File.ReadAllText("dependency.json"))["services"];
            var requiredServices = JsonConvert.DeserializeObject<List<Service>>(jsonServices.ToString());

            foreach (var service in requiredServices)
            {
                var serviceType = Type.GetType(service.ServiceType.Trim() + ", " + service.Assembly.Trim());
                var implementationType = Type.GetType(service.ImplementationType.Trim() + ", " + service.Assembly.Trim());
                var serviceLifetime = (ServiceLifetime)Enum.Parse(typeof(ServiceLifetime), service.Lifetime.Trim());

                var serviceDescriptor = new ServiceDescriptor(serviceType: serviceType,
                    implementationType: implementationType,
                    lifetime: serviceLifetime);

                services.Add(serviceDescriptor);
            }

        }

    }

    public class Service
    {
        public string ServiceType { get; set; }

        public string ImplementationType { get; set; }

        public string Lifetime { get; set; }
        public string Assembly { get; set; }
        
    }
}
