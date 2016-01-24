using System.Collections.Generic;

namespace Nancy.WebApi.Demo
{
    public class ModuleRouteSummary
    {
        public string ModuleName { get; set; }
        public string HelpModulePath { get; }

        public List<RouteSummary> Routes { get; set; }

        public ModuleRouteSummary(string moduleName, string helpModulePath)
        {
            ModuleName = moduleName;
            HelpModulePath = helpModulePath;
            Routes = new List<RouteSummary>();
        }
    }
}
