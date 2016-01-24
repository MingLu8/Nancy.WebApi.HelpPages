using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Helpers;
using Nancy.TinyIoc;
using Nancy.WebApi.AttributeRouting;
using WebApplication1.Areas.HelpPage.ModelDescriptions;

namespace Nancy.WebApi.Demo
{
    public class DefaultApiExplorer : IApiExplorer
    {
        private readonly string _helpModulePath;
        private readonly IModuleRouteExtractor _moduleRouteExtractor;
        private ModelDescriptionGenerator _modelDesctionGenerator;
        private Func<Type, ModelDescription> _modelDescriptionCreator; 

        private Dictionary<string, RouteInfo> _routeTable;
        private IEnumerable<ModuleRouteSummary> _moduleRouteCatelog;
        public virtual Dictionary<string, RouteInfo> RouteTable => _routeTable ?? (_routeTable = GenerateRouteTable());
        public virtual IEnumerable<ModuleRouteSummary> ModuleRouteCatelog => _moduleRouteCatelog ?? (_moduleRouteCatelog = GetModuleRouteCatelog());

        public DefaultApiExplorer(string helpModulePath, string xmlDocPath=null, IModuleRouteExtractor moduleRouteExtractor = null)
        {
            _helpModulePath = RouteInfo.CleanPath(helpModulePath);
            _moduleRouteExtractor = moduleRouteExtractor ?? new DefaultModuleRouteExtractor();
            _modelDesctionGenerator = new ModelDescriptionGenerator(xmlDocPath);
            _modelDescriptionCreator = type => _modelDesctionGenerator.GetOrCreateModelDescription(type);
        }

        public IEnumerable<ModuleRouteSummary> GetModuleRouteCatelog()
        {
            var routeInfoGroups = GetRouteGroupsByModule();
            return routeInfoGroups.Select(CreateModuleRouteSummary);
        }

        public RouteDetail GetRouteDetail(string requestPath)
        {
            var routeInfo = FindRoute(requestPath);
            return new RouteDetail(routeInfo, _modelDescriptionCreator);
        }

        private RouteInfo FindRoute(string requestPath)
        {
            var path = HttpUtility.UrlDecode(requestPath);
            path = RouteInfo.CleanPath(path);

            var routeName = path.Substring(_helpModulePath.Length + 1);
            var routeInfo = RouteTable[routeName];
            return routeInfo;
        }

        public ModelDescription GetResourceDetail(string requestPath)
        {
            var path = HttpUtility.UrlDecode(requestPath);
            var assemblyQualifyTypeName = path.Substring(path.LastIndexOf('/') + 1);
            Type type = Type.GetType(assemblyQualifyTypeName);
            return _modelDescriptionCreator(type);
        }

        private ModuleRouteSummary CreateModuleRouteSummary(IGrouping<Type, RouteInfo> moduleRouteInfos)
        {
            var moduleRouteSummary = new ModuleRouteSummary(moduleRouteInfos.Key.Name, _helpModulePath);

            foreach (var routeInfo in moduleRouteInfos)
                moduleRouteSummary.Routes.Add(new RouteSummary(routeInfo, _helpModulePath));
            return moduleRouteSummary;
        }

        private IEnumerable<IGrouping<Type, RouteInfo>> GetRouteGroupsByModule()
        {
            var routeInfoGroups = RouteTable.Values.GroupBy(a => a.Method.DeclaringType);
            return routeInfoGroups;
        }

        protected virtual Dictionary<string, RouteInfo> GenerateRouteTable()
        {
            var types = ScanForAttributeRoutingNancyModules();
            return types.SelectMany(type => _moduleRouteExtractor.ExtractRoutes(type)).ToDictionary(a => a.Name);
        }

        protected virtual IEnumerable<Type> ScanForAttributeRoutingNancyModules()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(AssemblyExtensions.SafeGetTypes)
                .Where(a => _moduleRouteExtractor.IsQualifiedType(a));
        }

        protected virtual bool IsSupported(Type type) => type.IsInterface || (!type.IsAbstract && (type.IsPublic || type.IsNestedPublic));
    }
}
