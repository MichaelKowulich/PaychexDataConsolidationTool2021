using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaychexDataConsolidationTool.Concrete;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using Xunit;
using Moq;
using Dapper;

namespace PaychexDataConsolidationTool.Concrete.Tests
{
    [TestClass()]
    public class CPSManagerTests
    {

        //
        //  Get Dates
        //
        [Fact]
        public async void getDates_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPS>($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerStatus] WHERE DateOfReport >= @startDate AND DateOfReport <= @endDate ORDER BY DateOfReport ASC", It.IsAny<DynamicParameters>(), CommandType.Text))
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

        //
        //  Get Statuses
        //
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

        //
        //  List All Valid Call
        //

        [Fact]
        public async void ListAll_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPSStatus>($"Select FORMAT ([dbo].[ClientsPerStatus].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Status].StatusName as StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate as StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= @startDate " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= @endDate " +
                $"ORDER BY DateOfReport ASC OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;", It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(GetSampleListAll());

                var cls = mock.Create<CPSManager>();
                var expected = GetSampleListAll();

                var actual = await cls.ListAll(0, 7, "DateOfReport", "2021-03-01", "2021-04-03", "ASC");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].StatusCountAsOfDate, actual[i].StatusCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].StatusName, actual[i].StatusName);
                }
            }
        }
        private List<CPSStatus> GetSampleListAll()
        {
            List<CPSStatus> output = new List<CPSStatus>
            {
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Active",
                    StatusCountAsOfDate = 20000,
                    
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Inactive",
                    StatusCountAsOfDate = 1000,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Demo",
                    StatusCountAsOfDate = 500,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Master",
                    StatusCountAsOfDate = 130,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Suspended",
                    StatusCountAsOfDate = 200,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Deleted",
                    StatusCountAsOfDate = 200,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Implementation",
                    StatusCountAsOfDate = 201,
                },
            };

            return output;
        }
        //
        //  get Status Report Data Valid Call
        //

        [Fact]
        public async void getStatusReportData_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPSStatus>($"Select FORMAT ([dbo].[ClientsPerStatus].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= @startDate " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= @endDate " +
                $"AND [dbo].[Status].StatusName = @statusName " +
                $"ORDER BY[dbo].[ClientsPerStatus].DateOfReport", It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(GetSampleGetStatusReportData());

                var cls = mock.Create<CPSManager>();
                var expected = GetSampleGetStatusReportData();

                var actual = await cls.getStatusReportData("2021-03-01", "2021-04-03", "Active");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].StatusCountAsOfDate, actual[i].StatusCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].StatusName, actual[i].StatusName);
                }
            }
        }
        private List<CPSStatus> GetSampleGetStatusReportData()
        {
            List<CPSStatus> output = new List<CPSStatus>
            {
                new CPSStatus
                {
                    DateOfReport = "2021-03-13",
                    StatusName = "Active",
                    StatusCountAsOfDate = 20000,

                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-20",
                    StatusName = "Active",
                    StatusCountAsOfDate = 20001,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-03-27",
                    StatusName = "Active",
                    StatusCountAsOfDate = 20002,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Active",
                    StatusCountAsOfDate = 20003,
                },
            };

            return output;
        }

        //
        //  Count Valid Call
        //

        [Fact]
        public async void Count_ValidCall()
        {
            string query = $"select COUNT(*) " +
                $"from [dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= @startDate " +
                $"AND [dbo].[ClientsPerStatus].DateOfReport <= @endDate ;";

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.Get<int>(query, It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(28);

                var cls = mock.Create<CPSManager>();
                var expected = 28;

                var actual = await cls.Count("2021-03-01", "2021-04-03");

                Xunit.Assert.Equal(expected, actual);
                
            }
        }

        //
        //  Get Most Recent Date Valid Call
        //

        [Fact]
        public async void GetMostRecentDate_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPS>($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerStatus]", null, CommandType.Text))
                    .Returns(GetSampleGetMostRecentDate());

                var cls = mock.Create<CPSManager>();
                var expected = GetSampleGetMostRecentDate();

                var actual = await cls.getMostRecentDate();

                string jsonActual = JsonConvert.SerializeObject(actual);
                string jsonExpected = JsonConvert.SerializeObject(expected);
                Xunit.Assert.Equal(jsonExpected, jsonActual);

            }
        }
        private List<CPS> GetSampleGetMostRecentDate()
        {
            List<CPS> output = new List<CPS>
            {
                new CPS
                {
                    ClientStatusId = 0,
                    DateOfReport = "2021-04-03",
                    StatusId = 0,
                    StatusCountAsOfDate = 0,
                },
            };
            return output;
        }
        //
        // Get Most Recent Status Counts Valid Call
        //
        [Fact]
        public async void getMostRecentStatusCounts_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPSStatus>($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"FROM [dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE DateOfReport = @date " +
                $"ORDER BY [dbo].[Status].StatusId", It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(GetSampleGetMostRecentStatusCounts());

                var cls = mock.Create<CPSManager>();
                var expected = GetSampleGetMostRecentStatusCounts();

                var actual = await cls.getMostRecentStatusCounts("2021-04-03");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].StatusCountAsOfDate, actual[i].StatusCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].StatusName, actual[i].StatusName);
                }
            }
        }
        private List<CPSStatus> GetSampleGetMostRecentStatusCounts()
        {
            List<CPSStatus> output = new List<CPSStatus>
            {
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Inactive",
                    StatusCountAsOfDate = 2000,

                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Active",
                    StatusCountAsOfDate = 20001,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Demo",
                    StatusCountAsOfDate = 200,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Master",
                    StatusCountAsOfDate = 100,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Suspended",
                    StatusCountAsOfDate = 300,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Deleted",
                    StatusCountAsOfDate = 500,
                },
                new CPSStatus
                {
                    DateOfReport = "2021-04-03",
                    StatusName = "Implementation",
                    StatusCountAsOfDate = 100,
                },
            };

            return output;
        }
    }
}