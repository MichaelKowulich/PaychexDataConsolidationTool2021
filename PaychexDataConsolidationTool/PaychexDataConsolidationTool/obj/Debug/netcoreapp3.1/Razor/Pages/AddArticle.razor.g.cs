#pragma checksum "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\AddArticle.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eddfa2856d10658f29dbbacf3e928108b60c65fa"
// <auto-generated/>
#pragma warning disable 1591
namespace PaychexDataConsolidationTool.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool.Concrete;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool.Contracts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using PaychexDataConsolidationTool.DataAccess;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/addArticle")]
    public partial class AddArticle : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>\r\n    Add Article\r\n</h1>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "row");
            __builder.AddMarkupContent(3, "\r\n    ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "col-md-4");
            __builder.AddMarkupContent(6, "\r\n        ");
            __builder.OpenElement(7, "form");
            __builder.AddMarkupContent(8, "\r\n            ");
            __builder.OpenElement(9, "div");
            __builder.AddAttribute(10, "class", "form-group");
            __builder.AddMarkupContent(11, "\r\n                ");
            __builder.AddMarkupContent(12, "<label for=\"Name\" class=\"control-label\">Name</label>\r\n                ");
            __builder.OpenElement(13, "input");
            __builder.AddAttribute(14, "for", "Name");
            __builder.AddAttribute(15, "class", "form-control");
            __builder.AddAttribute(16, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 13 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\AddArticle.razor"
                                                                     article.Title

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(17, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => article.Title = __value, article.Title));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n            ");
            __builder.OpenElement(20, "div");
            __builder.AddAttribute(21, "class", "form-group");
            __builder.AddMarkupContent(22, "\r\n                ");
            __builder.OpenElement(23, "button");
            __builder.AddAttribute(24, "type", "button");
            __builder.AddAttribute(25, "class", "btn btn-primary");
            __builder.AddAttribute(26, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 16 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\AddArticle.razor"
                                                                        () => CreateArticle()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(27, "Save");
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\r\n                ");
            __builder.OpenElement(29, "button");
            __builder.AddAttribute(30, "type", "button");
            __builder.AddAttribute(31, "class", "btn btn-danger");
            __builder.AddAttribute(32, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 17 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\AddArticle.razor"
                                                                       () => cancel()

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(33, "Cancel");
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 23 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\AddArticle.razor"
       

    Article article = new Article();

    protected async Task CreateArticle()
    {
        await articleManager.Create(article);
        navigationManager.NavigateTo("/articlelist");
    }

    void cancel()
    {
        navigationManager.NavigateTo("/articlelist");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.AspNetCore.Components.NavigationManager navigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IArticleManager articleManager { get; set; }
    }
}
#pragma warning restore 1591
