@using WebApplication1.Areas.HelpPage.ModelDescriptions
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta charset="utf-8"/>
    <link href="/Content/css/bootstrap.css" rel="stylesheet" />
    <link href="/Content/css/HelpPage.css" rel="stylesheet" />
</head>

<body>
    <div id="body" class="help-page">
    <section class="content-wrapper main-content clear-fix">
        <h1>@Model.HttpMethod @Model.Url</h1>
    <div>
       @*<p>@description.Documentation</p>*@

        <h2>Request Information</h2>

        <h3>URI Parameters</h3>
      @if (Model.UriParameters.Count > 0)
      {
        <table class="help-page-table">
            <thead>
                <tr><th>Name</th><th>Description</th><th>Type</th><th>Additional information</th></tr>
            </thead>
            <tbody>
                @foreach (var parameter in Model.UriParameters)
                {
                    var modelDescription = parameter.TypeDescription;
                    <tr>
                        <td class="parameter-name">@parameter.Name</td>
                        <td class="parameter-documentation">
                            <p>@parameter.Documentation</p>
                        </td>
                        <td class="parameter-type">
                            @if (modelDescription.GetType() == typeof(SimpleTypeModelDescription))
                            {
                                @modelDescription.Name
                            }
                            else
                            {
                                <a href="/help/resourceModel/@modelDescription.ModelType.ToString()">@modelDescription.Name</a>
                            }


                          @* @Html.DisplayFor(m => modelDescription.ModelType, "ModelDescriptionLink", new {modelDescription = modelDescription})*@
                        </td>
                        <td class="parameter-annotations">
                            @if (parameter.Annotations.Count > 0)
                            {
                                foreach (var annotation in parameter.Annotations)
                                {
                                    <p>@annotation.Documentation</p>
                                }
                            }
                            else
                            {
                                <p>None.</p>
                            }
                        </td>
                    </tr>
                }
            </tbody>
        </table>
      }
      else
      {
        <p>None.</p>
      }


        <h3>Body Parameters</h3>

        @*<p>@Model.RequestDocumentation</p>*@

        @if (Model.RequestModelDescription != null)
        {
            if (Model.RequestModelDescription.GetType() == typeof(SimpleTypeModelDescription))
            {
                @Model.RequestModelDescription.Name
            }
            else
            {
                <a href="/help/resourceModel/@Model.RequestModelDescription.ModelType.ToString()">@Model.RequestModelDescription.Name</a>
            }
            //@Html.DisplayFor(m => m.RequestModelDescription.ModelType, "ModelDescriptionLink", new {modelDescription = Model.RequestModelDescription})
            if (Model.RequestBodyParameters != null)
            {
                // @Html.DisplayFor(m => m.RequestBodyParameters, "Parameters")
                if (Model.RequestBodyParameters.Count > 0)
                {
                    <table class="help-page-table">
                        <thead>
                            <tr><th>Name</th><th>Description</th><th>Type</th><th>Additional information</th></tr>
                        </thead>
                        <tbody>
                            @foreach (var parameter in Model.RequestBodyParameters)
                            {
                                var modelDescription = parameter.TypeDescription;
                                <tr>
                                    <td class="parameter-name">@parameter.Name</td>
                                    <td class="parameter-documentation">
                                        <p>@parameter.Documentation</p>
                                    </td>
                                    <td class="parameter-type">
                                        @if (modelDescription.GetType() == typeof(SimpleTypeModelDescription))
                                        {
                                            @modelDescription.Name
                                        }
                                        else
                                        {
                                            <a href="/help/resourceModel/@modelDescription.ModelType.ToString()">@modelDescription.Name</a>
                                        }

                                    </td>
                                    <td class="parameter-annotations">
                                        @if (parameter.Annotations.Count > 0)
                            {
                                foreach (var annotation in parameter.Annotations)
                                {
                                                <p>@annotation.Documentation</p>
                                            }
                                        }
                                        else
                                        {
                                            <p>None.</p>
                                        }
                                    </td>
                                </tr>
                            }
                        </tbody>
                    </table>
                }
                else
                {
                    <p>None.</p>
                }
            }
        }
        else
        {
            <p>None.</p>
        }

        @if (Model.SampleRequests.Count > 0)
        {
            <h3>Request Formats</h3>
            <div>
                @foreach (var sample in Model.SampleRequests)
                {
                    <h4 class="sample-header">@sample.Key</h4>
                    <div class="sample-content">
                        <span><b>Sample:</b></span>
                        <pre class="wrapped">
                        @{                            
                            if (sample.Value == null)
                            {
                                <p>Sample not available.</p>
                            }
                            else
                            {
                                @sample.Value
                            }
                        }
                            </pre>
                    </div>
                }
            </div>
        }

        <h2>Response Information</h2>

        <h3>Resource Description</h3>

       @* <p>@description.ResponseDescription.Documentation</p>*@

        @if (Model.ResourceDescription != null)
        {
            // @Html.DisplayFor(m => m.ResourceDescription.ModelType, "ModelDescriptionLink", new { modelDescription = Model.ResourceDescription })
            if (Model.ResourceDescription.GetType() == typeof(SimpleTypeModelDescription))
            {
                @Model.ResourceDescription.Name
            }
            else
            {
                <a href="/help/resourceModel/@Model.ResourceDescription.ModelType.ToString()">@Model.ResourceDescription.Name</a>
            }        
            if (Model.ResourceProperties != null)
            {
                // @Html.DisplayFor(m => m.RequestBodyParameters, "Parameters")
                if (Model.ResourceProperties.Count > 0)
                {
                    <table class="help-page-table">
                        <thead>
                            <tr><th>Name</th><th>Description</th><th>Type</th><th>Additional information</th></tr>
                        </thead>
                        <tbody>
                            @foreach (var parameter in Model.ResourceProperties)
                            {
                                var modelDescription = parameter.TypeDescription;
                                <tr>
                                    <td class="parameter-name">@parameter.Name</td>
                                    <td class="parameter-documentation">
                                        <p>@parameter.Documentation</p>
                                    </td>
                                    <td class="parameter-type">
                                        @if (modelDescription.GetType() == typeof(SimpleTypeModelDescription))
                                        {
                                            @modelDescription.Name
                                        }
                                        else
                                        {
                                            <a href="/help/resourceModel/@modelDescription.ModelType.ToString()">@modelDescription.Name</a>
                                        }

                                        @* @Html.DisplayFor(m => modelDescription.ModelType, "ModelDescriptionLink", new {modelDescription = modelDescription})*@
                                    </td>
                                    <td class="parameter-annotations">
                                        @if (parameter.Annotations.Count > 0)
                            {
                                foreach (var annotation in parameter.Annotations)
                                {
                                                <p>@annotation.Documentation</p>
                                            }
                                        }
                                        else
                                        {
                                            <p>None.</p>
                                        }
                                    </td>
                                </tr>
                            }
                        </tbody>
                    </table>
                }
                else
                {
                    <p>None.</p>
                }
            }       
        }
        else
        {
            <p>None.</p>
        }

        @if (Model.SampleResponses.Count > 0)
        {
            <h3>Response Formats</h3>
            <div>
                @foreach (var sample in Model.SampleResponses)
                {
                    <h4 class="sample-header">@sample.Key</h4>
                    <div class="sample-content">
                        <span><b>Sample:</b></span>
                        <pre class="wrapped">
                        @{                            
                            if (sample.Value == null)
                            {
                        <p>Sample not available.</p>
                            }
                            else
                            {
                                @sample.Value
                            }
                        }
                            </pre>
                    </div>
    }
            </div>
    }
    </div>
        </section>
        </div>
</body>
</html>
