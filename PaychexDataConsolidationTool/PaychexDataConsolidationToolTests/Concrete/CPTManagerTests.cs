using Autofac.Extras.Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaychexDataConsolidationTool.Concrete;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Xunit;
using Type = PaychexDataConsolidationTool.Entities.ClientType;

namespace PaychexDataConsolidationTool.Concrete.Tests
{
    [TestClass()]
    public class CPTManagerTests
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
                    .Setup(x => x.GetAll<CPT>($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerType] WHERE DateOfReport >= '2021-03-01' AND DateOfReport <= '2021-04-03' ORDER BY DateOfReport ASC", null, CommandType.Text))
                    .Returns(GetSampleDates());

                var cls = mock.Create<CPTManager>();
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
        private List<CPT> GetSampleDates()
        {
            List<CPT> output = new List<CPT>
            {
                new CPT
                {
                    DateOfReport = "2021-03-13"
                },
                new CPT
                {
                    DateOfReport = "2021-03-20"
                },
                new CPT
                {
                    DateOfReport = "2021-03-27"
                },
                new CPT
                {
                    DateOfReport = "2021-04-03"
                },
            };

            return output;
        }

        //
        //  Get Types
        //
        [Fact]
        public async void getTypes_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<ClientType>("SELECT ClientTypeName FROM [dbo].[ClientType] ORDER BY ClientTypeId ASC", null, CommandType.Text))
                    .Returns(GetSampleTypes());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleTypes();

                var actual = await cls.getTypes();

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].ClientTypeName, actual[i].ClientTypeName);
                }
            }
        }

        private List<ClientType> GetSampleTypes()
        {
            List<ClientType> output = new List<ClientType>
            {
                new ClientType
                {
                    ClientTypeName = "Standalone Clients"
                },
                new ClientType
                {
                    ClientTypeName = "Nettime Organic Clients"
                },
                new ClientType
                {
                    ClientTypeName = "FlexTime Clients"
                },
                new ClientType
                {
                    ClientTypeName = "Essentials Clients"
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
                    .Setup(x => x.GetAll<CPTType>($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName as ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate as TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= '2021-03-01' " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= '2021-04-03' " +
                $"ORDER BY DateOfReport ASC OFFSET 0 ROWS FETCH NEXT 8 ROWS ONLY;", null, CommandType.Text))
                    .Returns(GetSampleListAll());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleListAll();

                var actual = await cls.ListAll(0, 8, "DateOfReport", "2021-03-01", "2021-04-03", "ASC");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].TypeCountAsOfDate, actual[i].TypeCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].ClientTypeName, actual[i].ClientTypeName);
                }
            }
        }
        private List<CPTType> GetSampleListAll()
        {
            List<CPTType> output = new List<CPTType>
            {
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "Essentials Clients",
                    TypeCountAsOfDate = 3000,

                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "FlexTime Clients",
                    TypeCountAsOfDate = 32000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "Nettime Organic Clients",
                    TypeCountAsOfDate = 100,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "Standalone",
                    TypeCountAsOfDate = 2000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "C2C All Clients",
                    TypeCountAsOfDate = 200,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "StratusTime Integrated Clients",
                    TypeCountAsOfDate = 8000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "C2C CLients with same FEIN",
                    TypeCountAsOfDate = 40000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "C2C CLients with different FEIN",
                    TypeCountAsOfDate = 330,
                },
            };

            return output;
        }
        //
        //  get Type Report Data Valid Call
        //

        [Fact]
        public async void getTypeReportData_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPTType>($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE  " +
                $"[dbo].[ClientsPerType].DateOfReport >= '2021-03-01' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '2021-04-03' " +
                $"AND [dbo].[ClientType].ClientTypeName = 'Standalone' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport", null, CommandType.Text))
                    .Returns(GetSampleGetTypeReportData());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleGetTypeReportData();

                var actual = await cls.getTypeReportData("2021-03-01", "2021-04-03", "Standalone");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].TypeCountAsOfDate, actual[i].TypeCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].ClientTypeName, actual[i].ClientTypeName);
                }
            }
        }
        private List<CPTType> GetSampleGetTypeReportData()
        {
            List<CPTType> output = new List<CPTType>
            {
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    ClientTypeName = "Standalone",
                    TypeCountAsOfDate = 20000,

                },
                new CPTType
                {
                    DateOfReport = "2021-03-20",
                    ClientTypeName = "Standalone",
                    TypeCountAsOfDate = 20001,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-27",
                    ClientTypeName = "Standalone",
                    TypeCountAsOfDate = 20002,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "Standalone",
                    TypeCountAsOfDate = 20003,
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
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.Get<int>($"select COUNT(*) " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= '2021-03-01' " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= '2021-04-03';", null, CommandType.Text))
                    .Returns(GetSampleCount());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleCount();

                var actual = await cls.Count("2021-03-01", "2021-04-03");

                Xunit.Assert.Equal(actual, expected);

            }
        }
        private int GetSampleCount()
        {
            return 32;
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
                    .Setup(x => x.GetAll<CPT>($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerType]", null, CommandType.Text))
                    .Returns(GetSampleGetMostRecentDate());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleGetMostRecentDate();

                var actual = await cls.getMostRecentDate();

                string jsonActual = JsonConvert.SerializeObject(actual);
                string jsonExpected = JsonConvert.SerializeObject(expected);
                Xunit.Assert.Equal(jsonExpected, jsonActual);

            }
        }
        private List<CPT> GetSampleGetMostRecentDate()
        {
            List<CPT> output = new List<CPT>
            {
                new CPT
                {
                    ClientsPerTypeId = 0,
                    DateOfReport = "2021-04-03",
                    ClientTypeId = 0,
                    TypeCountAsOfDate = 0,
                },
            };
            return output;
        }

        //
        // Get Most Recent Status Counts Valid Call
        //
        [Fact]
        public async void getMostRecentTypeCounts_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPTType>($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"FROM [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE DateOfReport = '2021-04-03' " +
                $"ORDER BY [dbo].[ClientType].ClientTypeId", null, CommandType.Text))
                    .Returns(GetSampleGetMostRecentStatusCounts());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleGetMostRecentStatusCounts();

                var actual = await cls.getMostRecentTypeCounts("2021-04-03");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].TypeCountAsOfDate, actual[i].TypeCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].ClientTypeName, actual[i].ClientTypeName);
                }
            }
        }
        private List<CPTType> GetSampleGetMostRecentStatusCounts()
        {
            List<CPTType> output = new List<CPTType>
            {
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "Standalone",
                    TypeCountAsOfDate = 2000,

                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "Nettime Organic",
                    TypeCountAsOfDate = 20001,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "FlexTime",
                    TypeCountAsOfDate = 200,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "Essentials",
                    TypeCountAsOfDate = 100,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "C2C All",
                    TypeCountAsOfDate = 300,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "StratusTime Integrated",
                    TypeCountAsOfDate = 500,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "C2C Clients with same FEIN",
                    TypeCountAsOfDate = 40000,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    ClientTypeName = "C2C Clients with different FEIN",
                    TypeCountAsOfDate = 100,
                },
            };

            return output;
        }
    }
}
