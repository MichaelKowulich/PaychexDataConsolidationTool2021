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
        public async void getDates_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPS>("SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerStatus] WHERE DateOfReport >= '2021-03-01' AND DateOfReport <= '2021-04-03' ORDER BY DateOfReport ASC", null, CommandType.Text))
                    .Returns(GetSampleDates());

                var cls = mock.Create<CPSManager>();
                var expected = GetSampleDates();

                var actual = await cls.getDates("2021-03-01", "2021-04-03");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                }
            }
        }
        private List<CPS> GetSampleDates()
        {
            List<CPS> output = new List<CPS>
            {
                new CPS
                {
                    DateOfReport = "2021-03-13"
                },
                new CPS
                {
                    DateOfReport = "2021-03-20"
                },
                new CPS
                {
                    DateOfReport = "2021-03-27"
                },
                new CPS
                {
                    DateOfReport = "2021-04-03"
                },
            };

            return output;
        }

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