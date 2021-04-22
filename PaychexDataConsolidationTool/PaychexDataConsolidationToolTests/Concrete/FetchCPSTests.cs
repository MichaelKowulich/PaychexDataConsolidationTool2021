using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Bunit;
using Moq;
using Autofac.Extras.Moq;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaychexDataConsolidationTool.Concrete;
using PaychexDataConsolidationTool;
using Assert = Xunit.Assert;
using System.Data;

namespace PaychexDataConsolidationToolTests.Concrete
{
    [TestClass()]
    public class FetchCPSTests
    {
        [Fact]
        public void ValidateInitialRenderOfComponents()
        {
            // Arrange
            using var ctx = new Bunit.TestContext();
            using AutoMock mock = AutoMock.GetLoose();
            var cls = mock.Create<CPSManager>();
            ctx.Services.AddSingleton<ICPSManager>(cls);
            var cut = ctx.RenderComponent<FetchCPS>();

            // Cut
            var startDatePicker = cut.Find("#StartDate");
            var endDatePicker = cut.Find("#EndDate");
            var searchButton = cut.Find("button");

            // Assert
            DateTime now = DateTime.Now;
            var formatted = now.ToString("yyyy-MM-dd");
            startDatePicker.MarkupMatches(@"<input type=""date"" id=""StartDate"" placeholder=""Start Date"" value=""" + formatted + @""" >");
            endDatePicker.MarkupMatches(@"<input type=""date"" id=""EndDate"" class=""padded-right"" placeholder=""End Date"" value=""" + formatted + @""" >");
            searchButton.MarkupMatches(@"<button type=""button"" class=""btn btn-primary btn-block p-1"" ><i class=""fa fa-search""></i>Search</button>");
        }
    }
}
