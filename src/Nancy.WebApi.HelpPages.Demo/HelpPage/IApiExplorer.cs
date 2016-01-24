using System.Collections.Generic;
using WebApplication1.Areas.HelpPage.ModelDescriptions;

namespace Nancy.WebApi.Demo
{
    public interface IApiExplorer
    {
        Dictionary<string, RouteInfo> RouteTable { get; }
        IEnumerable<ModuleRouteSummary> ModuleRouteCatelog { get; }

        RouteDetail GetRouteDetail(string path);

        ModelDescription GetResourceDetail(string path);
    }
}
