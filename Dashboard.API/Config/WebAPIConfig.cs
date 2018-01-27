using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;

namespace Dashboard.API.Config
{
    public class WebAPIConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            //Formatters
            var jqueryFormatter = config.Formatters.FirstOrDefault(_ => _.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));

            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(jqueryFormatter);

            foreach(var formatter in config.Formatters)
            {
                formatter.RequiredMemberSelector = new SuppressedRequiredMemberSelector();
            }

            //Default services
            config.Services.Replace(typeof(IContentNegotiator),
                new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));
            config.Services.RemoveAll(typeof(ModelValidatorProvider), validator=>!(validator is DataAnnotationsModelValidatorProvider));
        }
    }
}
