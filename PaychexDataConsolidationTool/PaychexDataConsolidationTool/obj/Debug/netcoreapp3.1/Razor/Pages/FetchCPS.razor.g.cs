#pragma checksum "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f34accdc7a4cbe3d897804191fd50cda02b1b256"
// <auto-generated/>
#pragma warning disable 1591
namespace PaychexDataConsolidationTool
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
#nullable restore
#line 15 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\_Imports.razor"
using ChartJs.Blazor;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/cpslist")]
    public partial class FetchCPS : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<link href=\"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\" rel=\"stylesheet\">\r\n\r\n");
            __builder.AddMarkupContent(1, @"<style>
    .sort-th {
        cursor: pointer;
    }

    .fa {
        float: right;
    }

    .padded-left {
        margin-left: 20px;
    }

    .padded-right {
        margin-right: 20px;
    }

    .btn-custom {
        color: black;
        float: left;
        padding: 8px 16px;
        text-decoration: none;
        transition: background-color .3s;
        border: 2px solid #000;
        margin: 0px 5px 0px 5px;
    }
</style>

<br>

");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "form-group row");
            __builder.AddMarkupContent(4, "\r\n    ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "col-xs-2");
            __builder.AddMarkupContent(7, "\r\n        ");
            __builder.AddMarkupContent(8, "<label for=\"StartDate\"> Start Date: </label>\r\n        ");
            __builder.OpenElement(9, "input");
            __builder.AddAttribute(10, "type", "date");
            __builder.AddAttribute(11, "id", "StartDate");
            __builder.AddAttribute(12, "placeholder", "Start Date");
            __builder.AddAttribute(13, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 40 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                                          StartDate

#line default
#line hidden
#nullable disable
            , format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.AddAttribute(14, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => StartDate = __value, StartDate, format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n    ");
            __builder.OpenElement(17, "div");
            __builder.AddAttribute(18, "class", "col-xs-2");
            __builder.AddMarkupContent(19, "\r\n        ");
            __builder.AddMarkupContent(20, "<label for=\"EndDate\" class=\"padded-left\"> End Date: </label>\r\n        ");
            __builder.OpenElement(21, "input");
            __builder.AddAttribute(22, "type", "date");
            __builder.AddAttribute(23, "id", "EndDate");
            __builder.AddAttribute(24, "class", "padded-right");
            __builder.AddAttribute(25, "placeholder", "End Date");
            __builder.AddAttribute(26, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 44 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                                                           EndDate

#line default
#line hidden
#nullable disable
            , format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.AddAttribute(27, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => EndDate = __value, EndDate, format: "yyyy-MM-dd", culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n    ");
            __builder.OpenElement(30, "div");
            __builder.AddAttribute(31, "class", "d-flex flex-column");
            __builder.AddMarkupContent(32, "\r\n        ");
            __builder.OpenElement(33, "button");
            __builder.AddAttribute(34, "type", "button");
            __builder.AddAttribute(35, "class", "btn btn-primary btn-block p-1");
            __builder.AddAttribute(36, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 47 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                                              generateCPSGraph

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(37, "<i class=\"fa fa-search\"></i>Search");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n");
            __builder.CloseElement();
            __builder.AddMarkupContent(40, "\r\n\r\n<canvas id=\"lineGraph\"></canvas>\r\n\r\n");
#nullable restore
#line 53 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
 if (hasSearched == true)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(41, "    ");
            __builder.OpenElement(42, "table");
            __builder.AddAttribute(43, "class", "table table-bordered table-hover");
            __builder.AddMarkupContent(44, "\r\n        ");
            __builder.OpenElement(45, "thead");
            __builder.AddMarkupContent(46, "\r\n            ");
            __builder.OpenElement(47, "tr");
            __builder.AddMarkupContent(48, "\r\n                ");
            __builder.OpenElement(49, "th");
            __builder.AddAttribute(50, "class", "sort-th");
            __builder.AddAttribute(51, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 58 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                () => SortTable("DateOfReport")

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(52, "\r\n                    Date Of Report\r\n                    ");
            __builder.OpenElement(53, "span");
            __builder.AddAttribute(54, "class", "fa" + " " + (
#nullable restore
#line 60 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                      SetSortIcon("DateOfReport")

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(55, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(56, "\r\n                ");
            __builder.OpenElement(57, "th");
            __builder.AddAttribute(58, "class", "sort-th");
            __builder.AddAttribute(59, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 62 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                () => SortTable("StatusName")

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(60, "\r\n                    Status\r\n                    ");
            __builder.OpenElement(61, "span");
            __builder.AddAttribute(62, "class", "fa" + " " + (
#nullable restore
#line 64 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                      SetSortIcon("StatusName")

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(63, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n                ");
            __builder.OpenElement(65, "th");
            __builder.AddAttribute(66, "class", "sort-th");
            __builder.AddAttribute(67, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 66 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                () => SortTable("StatusCountAsOfDate")

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(68, "\r\n                    Total\r\n                    ");
            __builder.OpenElement(69, "span");
            __builder.AddAttribute(70, "class", "fa" + " " + (
#nullable restore
#line 68 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                      SetSortIcon("StatusCountAsOfDate")

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(71, "\r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(72, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(73, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\r\n        ");
            __builder.OpenElement(75, "tbody");
            __builder.AddMarkupContent(76, "\r\n");
#nullable restore
#line 73 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
             if (cpssModel == null || cpssModel.Count == 0)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(77, "                ");
            __builder.AddMarkupContent(78, "<tr>\r\n                    <td colspan=\"3\">\r\n                        No Records to display\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 80 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
            }
            else
            {
                foreach (var cpss in cpssModel)
                {

#line default
#line hidden
#nullable disable
            __builder.AddContent(79, "                    ");
            __builder.OpenElement(80, "tr");
            __builder.AddMarkupContent(81, "\r\n                        ");
            __builder.OpenElement(82, "td");
            __builder.AddContent(83, 
#nullable restore
#line 86 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                             cpss.DateOfReport

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(84, "\r\n                        ");
            __builder.OpenElement(85, "td");
            __builder.AddContent(86, 
#nullable restore
#line 87 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                             cpss.StatusName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(87, "\r\n                        ");
            __builder.OpenElement(88, "td");
            __builder.AddContent(89, 
#nullable restore
#line 88 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                             cpss.StatusCountAsOfDate

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(90, "\r\n                    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(91, "\r\n");
#nullable restore
#line 90 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                }
            }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(92, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(93, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(94, "\r\n    ");
            __builder.OpenElement(95, "div");
            __builder.AddAttribute(96, "class", "pagination");
            __builder.AddMarkupContent(97, "\r\n        ");
            __builder.OpenElement(98, "button");
            __builder.AddAttribute(99, "class", "btn btn-custom");
            __builder.AddAttribute(100, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 96 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                  async () => await NavigateToPage("previous")

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(101, "Prev");
            __builder.CloseElement();
            __builder.AddMarkupContent(102, "\r\n\r\n");
#nullable restore
#line 98 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
         for (int i = startPage; i <= endPage; i++)
        {
            var currentPage = i;

#line default
#line hidden
#nullable disable
            __builder.AddContent(103, "            ");
            __builder.OpenElement(104, "button");
            __builder.AddAttribute(105, "class", "btn" + " btn-custom" + " pagebutton" + " " + (
#nullable restore
#line 101 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                       currentPage == curPage ? "btn-danger" : ""

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(106, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 101 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                                                                               async () => await refreshRecords(currentPage)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(107, "\r\n                ");
            __builder.AddContent(108, 
#nullable restore
#line 102 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                 currentPage

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(109, "\r\n            ");
            __builder.CloseElement();
            __builder.AddMarkupContent(110, "\r\n");
#nullable restore
#line 104 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(111, "\r\n        ");
            __builder.OpenElement(112, "button");
            __builder.AddAttribute(113, "class", "btn btn-custom");
            __builder.AddAttribute(114, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 106 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
                                                  async () => await NavigateToPage("next")

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(115, "Next");
            __builder.CloseElement();
            __builder.AddMarkupContent(116, "\r\n\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(117, "\r\n");
#nullable restore
#line 109 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 112 "C:\Users\Mike\Desktop\PaychexDataConsolidationTool2021\PaychexDataConsolidationTool\PaychexDataConsolidationTool\Pages\FetchCPS.razor"
       

    /////////////////////////////////////////////////////////////
    //
    // Creating a GraphData Object so we can flatten (serialize) it
    // and send it over to javascript interop.js
    //
    public class GraphData
    {
        public int[,] StatusCounts { get; set; }
        public string[] Dates { get; set; }
        public string[] Statuses { get; set; }
        public GraphData(string[] theDates, string[] theStatuses, int i, int j)
        {
            this.Statuses = theStatuses;
            this.StatusCounts = new int[i, j];
            this.Dates = theDates;
        }
    }

    ////////////////////////////////////////////////////////////////////
    //
    // Values that we have bound to input elements of this razor file
    //
    private DateTime startDate = DateTime.Now;
    private DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    protected DateTime endDate = DateTime.Now;
    protected DateTime EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }


    ////////////////////////////////////////////////////////////////////
    //
    // Values that we will use to configure our table after search
    //

    List<CPSStatus> cpssModel;
    CPS cpsEntity = new CPS();


    #region Pagination

    int totalPages;
    int totalRecords;
    int curPage;
    int pagerSize;
    int pageSize;
    int startPage;
    int endPage;
    string sortColumnName = "ClientsPerStatusId";
    string sortDir = "DESC";

    #endregion

    private bool isSortedAscending;
    public bool hasSearched = false;
    private string activeSortColumn;

    /////////////////////////////////////////////////////////////
    //
    // Declaring some things that will always exist in our graph
    //
    string[] dates = new string[0];
    string[] statuses = new string[0];
    string json;

    /////////////////////////////////////////////////////////////
    //
    // Waits for search to return, then sends flattened (serialized)
    // data over to our interop.js file
    //
    protected async Task generateCPSGraph()
    {
        await this.OnSearchAsync();

        await JSRuntime.InvokeAsync<bool>("generateCPSGraph", json);

        await this.generateTable();
    }

    /////////////////////////////////////////////////////////////
    //
    // Main function that handles search between dates
    //
    protected async Task OnSearchAsync()
    {
        //Ensuring that we clean up if user decides to search again
        Array.Resize(ref dates, 0);
        Array.Resize(ref statuses, 0);

        //Getting our range of dates, and properly putting them in the array
        List<CPS> dateRange = await cpsManager.getDates(startDate.ToString(), endDate.ToString());
        foreach (var date in dateRange)
        {
            Array.Resize(ref dates, dates.Length + 1);
            dates[dates.GetUpperBound(0)] = date.DateOfReport;
        }

        //Getting our statuses and properly putting them in the array
        List<Status> Statuses = await cpsManager.getStatuses();
        foreach (var status in Statuses)
        {
            Array.Resize(ref statuses, statuses.Length + 1);
            statuses[statuses.GetUpperBound(0)] = status.StatusName.ToString();
        }

        //Create our graphdata object because now we know what amount of data we are working with
        GraphData obj = new GraphData(dates, statuses, statuses.Length, dates.Length);

        /////////////////////////////////////////////////////////////
        //
        // For each status, get all the calculated totals between the
        // given dates, then store them in an array, that we will
        // place in another array (array of arrays/2d array) so that
        // each index in the statuses array corresponds to the index
        // of the 2d array that holds the totals i.e:
        //
        //  statuses[0] => CountsPerStatus[0]
        //
        for (int i = 0; i < statuses.Length; i++)
        {
            List<CPSStatus> allCountsPerStatusWithinDate = await cpsManager.getStatusReportData(startDate.ToString(), endDate.ToString(), statuses[i]);
            int[] CountsPerStatus = new int[dates.Length];
            for (int j = 0; j < dates.Length; j++)
            {
                if (dates[j] == allCountsPerStatusWithinDate.ElementAt(j).DateOfReport)
                {
                    CountsPerStatus[j] = allCountsPerStatusWithinDate.ElementAt(j).StatusCountAsOfDate;
                }
                else
                {
                    CountsPerStatus[j] = 0;
                }
                obj.StatusCounts[i, j] = CountsPerStatus[j];
            }
        }
        //Flattening our graphdata object to send over for JS to handle
        json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        hasSearched = true;
    }

    ////////////////////////////////////////////////////////////////////
    //
    // Function that generates table and initializes page values
    //
    protected async Task generateTable()
    {
        pagerSize = 3;
        pageSize = statuses.Length;
        curPage = 1;
        cpssModel = await cpsManager.ListAll((curPage - 1) * pageSize, pageSize, sortColumnName, startDate.ToString(), endDate.ToString(), sortDir);
        totalRecords = await cpsManager.Count(startDate.ToString(), endDate.ToString());
        totalPages = (int)Math.Ceiling(totalRecords / (decimal)pageSize);
        SetPagerSize("forward");
        startPage = 1;
    }

    ////////////////////////////////////////////////////////////////////
    //
    // Functions that sort records based on what a user selects
    //
    private async Task<List<CPSStatus>> SortRecords(string columnName, string dir)
    {
        return await cpsManager.ListAll((curPage - 1) * pageSize, pageSize, columnName, startDate.ToString(), endDate.ToString(), dir);
    }

    private async Task SortTable(string columnName)
    {
        if (columnName != activeSortColumn)
        {
            cpssModel = await SortRecords(columnName, "ASC");
            isSortedAscending = true;
            activeSortColumn = columnName;
        }
        else
        {
            if (isSortedAscending)
            {
                cpssModel = await SortRecords(columnName, "DESC");
            }
            else
            {
                cpssModel = await SortRecords(columnName, "ASC");
            }

            isSortedAscending = !isSortedAscending;
        }
        sortColumnName = columnName;
        sortDir = isSortedAscending ? "ASC" : "DESC";
    }

    ////////////////////////////////////////////////////////////////////
    //
    // Setting the icon to be either ^ v because we deserve nice things
    //
    private string SetSortIcon(string columnName)
    {
        if (activeSortColumn != columnName)
        {
            return string.Empty;
        }
        if (isSortedAscending)
        {
            return "fa-sort-up";
        }
        else
        {
            return "fa-sort-down";
        }
    }

    ////////////////////////////////////////////////////////////////////
    //
    // Values will refresh when a user navigates to the next page
    //
    public async Task refreshRecords(int currentPage)
    {
        cpssModel = await cpsManager.ListAll((currentPage - 1) * pageSize, pageSize, sortColumnName, startDate.ToString(), endDate.ToString(), sortDir);
        curPage = currentPage;
        this.StateHasChanged();
    }

    //////////////////////////////////////////////////////////////////////////
    //
    // This controls the forward and backward motion of tabular page turning
    //
    public void SetPagerSize(string direction)
    {
        if (direction == "forward" && endPage < totalPages)
        {
            startPage = endPage + 1;
            if (endPage + pagerSize < totalPages)
            {
                endPage = startPage + pagerSize - 1;
            }
            else
            {
                endPage = totalPages;
            }
            this.StateHasChanged();
        }
        else if (direction == "back" && startPage > 1)
        {
            endPage = startPage - 1;
            startPage = startPage - pagerSize;
        }
        else
        {
            startPage = 1;
            endPage = totalPages;
        }
    }

    ////////////////////////////////////////////////////////////////////
    //
    // This actually navigates to the next page
    //
    public async Task NavigateToPage(string direction)
    {
        if (direction == "next")
        {
            if (curPage < totalPages)
            {
                if (curPage == endPage)
                {
                    SetPagerSize("forward");
                }
                curPage += 1;
            }
        }
        else if (direction == "previous")
        {
            if (curPage > 1)
            {
                if (curPage == startPage)
                {
                    SetPagerSize("back");
                }
                curPage -= 1;
            }
        }
        await refreshRecords(curPage);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private ICPSManager cpsManager { get; set; }
    }
}
#pragma warning restore 1591
