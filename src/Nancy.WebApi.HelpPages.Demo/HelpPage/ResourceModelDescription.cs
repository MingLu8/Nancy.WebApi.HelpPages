using System.Collections.Generic;
using WebApplication1.Areas.HelpPage.ModelDescriptions;

namespace Nancy.WebApi.Demo
{
    public class ResourceModelDescription
    {
        public ModelDescription ModelDescription { get; set; }
        public List<ParameterDescription> Members { get; set; }

        public ResourceModelDescription(ModelDescription modelDescription, List<ParameterDescription> members)
        {
            ModelDescription = modelDescription;
            Members = members;
        }
    }
}
