using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.WebApi.Help;
using Newtonsoft.Json;
using WebApplication1.Areas.HelpPage;
using WebApplication1.Areas.HelpPage.ModelDescriptions;

namespace Nancy.WebApi.Demo
{
    public class RouteDetail
    {
        private readonly RouteInfo _routeInfo;
        private readonly Func<Type, ModelDescription> _modelDescriptionCreator;
        public string Url => _routeInfo.Path;
        public string HttpMethod => _routeInfo.HttpMethod.ToString();

        public List<ParameterDescription> UriParameters { get; set; } = new List<ParameterDescription>();
        public List<ParameterDescription> RequestBodyParameters { get; set; } = new List<ParameterDescription>();

        public string RequestDocumentation { get; private set; }

        public string Documentation { get; private set; }

        public Dictionary<string, object> SampleRequests { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> SampleResponses { get; } = new Dictionary<string, object>();

        public ModelDescription ResourceDescription { get; private set; }
        public ModelDescription RequestModelDescription { get; private set; }

        public List<ParameterDescription> ResourceProperties { get; set; } = new List<ParameterDescription>();

        public RouteDetail(RouteInfo routeInfo, Func<Type, ModelDescription> modelDescriptionCreator)
        {
            _routeInfo = routeInfo;
            _modelDescriptionCreator = modelDescriptionCreator;
            LoadUrlParameters();
            LoadBodyParameter();
            LoadReturnParameter();
        }

        private void LoadReturnParameter()
        {
            var returnType = ConvertToResponseMessageTypeIfNeeded();

            ResourceDescription = _modelDescriptionCreator(returnType);

            HandleComplexResourceType(ResourceDescription, ResourceProperties);

            var sample = GetSampleForModel(ResourceDescription);
            SampleResponses.Add(sample.Key, sample.Value);
        }

        private KeyValuePair<string, object> GetSampleForModel(ModelDescription modelDescription)
        {
            var simpleTypeResourceDescription = ResourceDescription as SimpleTypeModelDescription;

            if (simpleTypeResourceDescription != null)
                return new KeyValuePair<string, object>("Text", new ObjectGenerator().GenerateObject(modelDescription.ModelType));
           
            var json = JsonConvert.SerializeObject(new ObjectGenerator().GenerateObject(modelDescription.ModelType), Formatting.Indented);
            return new KeyValuePair<string, object>("json", json);
        }

        private void HandleComplexResourceType(ModelDescription modelDescription, List<ParameterDescription> members)
        {
            var complexTypeResourceDescription = modelDescription as ComplexTypeModelDescription;

            if (complexTypeResourceDescription != null)
                members.AddRange(complexTypeResourceDescription.Properties);
        }

        private Type ConvertToResponseMessageTypeIfNeeded()
        {
            var returnType = _routeInfo.ReturnParameter.ParameterType;

            if (returnType.UnderlyingSystemType.Name == typeof (ResponseMessage<>).Name)
                returnType = returnType.GenericTypeArguments[0].UnderlyingSystemType;

            return returnType;
        }

        private void LoadBodyParameter()
        {
            if (_routeInfo.BodyParameter != null)
            {
                var bodyParameterType = _routeInfo.BodyParameter.ParameterType;
                RequestModelDescription = _modelDescriptionCreator(bodyParameterType);

                HandleComplexResourceType(RequestModelDescription, RequestBodyParameters);

                var sample = GetSampleForModel(RequestModelDescription);
                SampleRequests.Add(sample.Key, sample.Value);              
            }
        }

        private void LoadUrlParameters()
        {
            UriParameters = _routeInfo.QueryParameters.Select(a => new ParameterDescription
            {
                Name = a.Name,
                Documentation = string.Empty,
                TypeDescription = _modelDescriptionCreator(a.ParameterType)
            }).ToList();
        }
    }
}
