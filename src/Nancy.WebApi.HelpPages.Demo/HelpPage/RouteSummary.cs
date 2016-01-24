namespace Nancy.WebApi.Demo
{
    public class RouteSummary
    {
        private readonly string _helpModulePath;
        public HttpMethod HttpMethod { get; set; }
      
        public RouteSummary(RouteInfo routeInfo, string helpModulePath)
        {
            _helpModulePath = RouteInfo.CleanPath(helpModulePath);
            Name = RouteInfo.CleanPath(routeInfo.Name);
            HttpMethod = routeInfo.HttpMethod;
        }

        private string GetHelpUrl(string name)
        {
            return RouteInfo.CleanPath(_helpModulePath) + "/" + RouteInfo.CleanPath(name);
        }

        public string Url => GetHelpUrl(Name);

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
