using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace Redmask.Taghelpers
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        private const string ThemeKey = "theme";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var cache = (IMemoryCache)context.ActionContext.HttpContext.RequestServices.GetService(typeof(IMemoryCache));
            var setting = cache.Get<string>("Theme");
            if (setting != null)
                context.Values[ThemeKey] =setting;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (!context.Values.TryGetValue(ThemeKey, out var theme)) return viewLocations;
            if (string.IsNullOrWhiteSpace(theme)) return viewLocations;

            IEnumerable<string> themeLocations = new[]
            {
                $"/Themes/{theme}/{{1}}/{{0}}.cshtml",
                $"/Themes/{theme}/Shared/{{0}}.cshtml"
            };

            var enumerable = viewLocations.ToList();
            var areas = enumerable.Where(x => x.StartsWith("/Areas/")).ToList();
            var others = enumerable.Where(x => !x.StartsWith("/Areas/")).ToList();
            viewLocations = areas.Concat(themeLocations.Concat(others)).ToList();

            //string tenant;
            //if (context.Values.TryGetValue(TENANT_KEY, out tenant))
            //{
            //    themeLocations = ExpandTenantLocations(tenant, themeLocations);
            //}


            return viewLocations;
        }




    }
}
