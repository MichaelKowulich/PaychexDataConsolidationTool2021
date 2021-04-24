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
    public class FetchCPBTests
    {
        [Fact]
        public void ValidateInitialRenderOfComponents()
        {
            // Arrange
            using var ctx = new Bunit.TestContext();
            using AutoMock mock = AutoMock.GetLoose();
            mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<ClientsPerBrandCountType>($"SELECT ClientsPerBrandCountTypeName FROM [dbo].[ClientsPerBrandCountType] ORDER BY ClientsPerBrandCountTypeId ASC", null, CommandType.Text))
                    .Returns(GetSampleCountTypes());
            var cls = mock.Create<CPBManager>();
            ctx.Services.AddSingleton<ICPBManager>(cls);
            var cut = ctx.RenderComponent<FetchCPB>();

            // Cut
            var startDatePicker = cut.Find("#StartDate");
            var endDatePicker = cut.Find("#EndDate");
            var searchButton = cut.Find("button");
            var countTypePicker = cut.Find("#CountType");

            // Assert
            DateTime now = DateTime.Now;
            var formatted = now.ToString("yyyy-MM-dd");
            startDatePicker.MarkupMatches(@"<input type=""date"" id=""StartDate"" placeholder=""Start Date"" value=""" + formatted + @""" >");
            endDatePicker.MarkupMatches(@"<input type=""date"" id=""EndDate"" class=""padded-right"" placeholder=""End Date"" value=""" + formatted + @""" >");
            searchButton.MarkupMatches(@"<button type=""button"" class=""btn btn-primary btn-block p-1"" ><i class=""fa fa-search""></i>Search</button>");
            countTypePicker.MarkupMatches(@"<select type=""string"" id=""CountType"" class=""padded-right"" value=""Active Client Count"" >
      <option value=""Active Client Count"" selected="""">
        Active Client Count
      </option>
      <option value=""Active EE Count"">
        Active EE Count
      </option>
    </select>");
        }

        private List<ClientsPerBrandCountType> GetSampleCountTypes()
        {
            List<ClientsPerBrandCountType> output = new List<ClientsPerBrandCountType>
            {
                new ClientsPerBrandCountType
                {
                    ClientsPerBrandCountTypeName = "Active Client Count"
                },
                new ClientsPerBrandCountType
                {
                    ClientsPerBrandCountTypeName = "Active EE Count"
                },
            };

            return output;
        }

    }
}
