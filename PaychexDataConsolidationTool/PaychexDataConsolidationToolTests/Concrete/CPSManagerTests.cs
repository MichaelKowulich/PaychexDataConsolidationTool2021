using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaychexDataConsolidationTool.Concrete;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Models;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace PaychexDataConsolidationTool.Concrete.Tests
{
    [TestClass()]
    public class CPSManagerTests
    {

        [Fact]
        public async void getStatuses_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<Status>("SELECT StatusName FROM [dbo].[Status] ORDER BY StatusId ASC", null, CommandType.Text))
                    .Returns(GetSampleStatuses());

                var cls = mock.Create<CPSManager>();
                var expected = GetSampleStatuses();

                var actual = await cls.getStatuses();

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].StatusName, actual[i].StatusName);
                }
            }
        }

        private List<Status> GetSampleStatuses()
        {
            List<Status> output = new List<Status>
            {
                new Status
                {
                    StatusName = "Active"
                },
                new Status
                {
                    StatusName = "Inactive"
                },
                new Status
                {
                    StatusName = "Demo"
                },
                new Status
                {
                    StatusName = "Master"
                },
            };

            return output;
        }
    }
}