using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Nancy.Helpers;
using Nancy.WebApi.Help;
using WebApplication1.Areas.HelpPage.ModelDescriptions;

namespace Nancy.WebApi.Demo
{
    public class HelpApi : NancyModule
    {
        private IApiExplorer _apiExplorer { get; }

        public HelpApi(IRootPathProvider pathProvider):base("/help")
        {
            var xmlDocPath = Path.Combine(pathProvider.GetRootPath(), ConfigurationManager.AppSettings["xmlDocumentationFile"]);
            _apiExplorer =  new DefaultApiExplorer(ModulePath, xmlDocPath);

            Get["/"] = q => View["RouteCatelog.cshtml", _apiExplorer.ModuleRouteCatelog];

            Get["/{api*}"] = context =>
            {
                var model = _apiExplorer.GetRouteDetail(Request.Path);
                return View["RouteDetail.cshtml", model];
            };

            Get["/resourceModel/{modelName}"] = context => View["ResourceModel.cshtml", _apiExplorer.GetResourceDetail(Request.Path)];
        }

    }
}
