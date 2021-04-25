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
using ClientBrand = PaychexDataConsolidationTool.Entities.ClientBrand;

namespace PaychexDataConsolidationTool.Concrete.Tests
{
    [TestClass()]
    public class CPBManagerTests
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
                    .Setup(x => x.GetAll<CPB>($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerBrand] WHERE DateOfReport >= '2021-03-01' AND DateOfReport <= '2021-04-03' ORDER BY DateOfReport ASC", null, CommandType.Text))
                    .Returns(GetSampleDates());

                var cls = mock.Create<CPBManager>();
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
        private List<CPB> GetSampleDates()
        {
            List<CPB> output = new List<CPB>
            {
                new CPB
                {
                    DateOfReport = "2021-03-13"
                },
                new CPB
                {
                    DateOfReport = "2021-03-20"
                },
                new CPB
                {
                    DateOfReport = "2021-03-27"
                },
                new CPB
                {
                    DateOfReport = "2021-04-03"
                },
            };

            return output;
        }

        //
        //  Get Brands
        //
        [Fact]
        public async void getBrands_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<ClientBrand>("SELECT ClientBrandName FROM [dbo].[ClientBrand] ORDER BY ClientBrandId ASC", null, CommandType.Text))
                    .Returns(GetSampleBrands());

                var cls = mock.Create<CPBManager>();
                var expected = GetSampleBrands();

                var actual = await cls.getBrands();

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].ClientBrandName, actual[i].ClientBrandName);
                }
            }
        }

        private List<ClientBrand> GetSampleBrands()
        {
            List<ClientBrand> output = new List<ClientBrand>
            {
                new ClientBrand
                {
                    ClientBrandName = "Paychex"
                },
                new ClientBrand
                {
                    ClientBrandName = "Brand 2"
                },
                new ClientBrand
                {
                    ClientBrandName = "Brand 3"
                },
                new ClientBrand
                {
                    ClientBrandName = "Brand 4"
                },
                new ClientBrand
                {
                    ClientBrandName = "Brand 5"
                },

            };

            return output;
        }
//
        //  Get Count Types
        //
        [Fact]
        public async void getCountTypes_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<ClientsPerBrandCountType>($"SELECT ClientsPerBrandCountTypeName FROM [dbo].[ClientsPerBrandCountType] ORDER BY ClientsPerBrandCountTypeId ASC", null, CommandType.Text))
                    .Returns(GetSampleCountTypes());

                var cls = mock.Create<CPBManager>();
                var expected = GetSampleCountTypes();

                var actual = await cls.getCountTypes();

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].ClientsPerBrandCountTypeName, actual[i].ClientsPerBrandCountTypeName);
                }
            }
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
        //
        //  List All Valid Call
        //

        [Fact]
        public async void ListAll_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPBCountTypeBrand>($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " +
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate as CountAsOfDate " +
                $"from[dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= '2021-03-01' " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= '2021-04-03' " +
                $"AND [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName = 'Active Client Count' " +
                $"ORDER BY DateOfReport ASC OFFSET 0 ROWS FETCH NEXT 8 ROWS ONLY;", null, CommandType.Text))
                    .Returns(GetSampleListAll());

                var cls = mock.Create<CPBManager>();
                var expected = GetSampleListAll();

                var actual = await cls.ListAll(0, 8, "DateOfReport", "2021-03-01", "2021-04-03", "Active Client Count", "ASC");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].CountAsOfDate, actual[i].CountAsOfDate);
                    Xunit.Assert.Equal(expected[i].ClientBrandName, actual[i].ClientBrandName);
                }
            }
        }
        private List<CPBCountTypeBrand> GetSampleListAll()
        {
            List<CPBCountTypeBrand> output = new List<CPBCountTypeBrand>
            {
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-13",
                    ClientBrandName = "Paychex",
                    CountAsOfDate = 3000,
                    ClientsPerBrandCountTypeName = "Active Client Count"
                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-13",
                    ClientBrandName = "Brand 2",
                    CountAsOfDate = 32000,
                    ClientsPerBrandCountTypeName = "Active Client Count"

                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-13",
                    ClientBrandName = "Brand 3",
                    CountAsOfDate = 100,
                    ClientsPerBrandCountTypeName = "Active Client Count"

                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-13",
                    ClientBrandName = "Brand 4",
                    CountAsOfDate = 2000,
                    ClientsPerBrandCountTypeName = "Active Client Count"

                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-13",
                    ClientBrandName = "Brand 5",
                    CountAsOfDate = 200,
                    ClientsPerBrandCountTypeName = "Active Client Count"
                },
            };

            return output;
        }
        //
        //  get ClientBrand Report Data Valid Call
        //

        [Fact]
        public async void getBrandReportData_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPBCountTypeBrand>($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " + 
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate " +
                $"FROM [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= '2021-03-01' " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= '2021-04-03' " +
                $"AND [dbo].[ClientBrand].ClientBrandName = 'Paychex' " +
                $"AND [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName = 'Active Client Count' " +
                $"ORDER BY[dbo].[ClientsPerBrand].DateOfReport", null, CommandType.Text))
                    .Returns(GetSampleGetClientBrandReportData());

                var cls = mock.Create<CPBManager>();
                var expected = GetSampleGetClientBrandReportData();

                var actual = await cls.getBrandReportData("2021-03-01", "2021-04-03", "Paychex", "Active Client Count");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].CountAsOfDate, actual[i].CountAsOfDate);
                    Xunit.Assert.Equal(expected[i].ClientBrandName, actual[i].ClientBrandName);
                }
            }
        }
        private List<CPBCountTypeBrand> GetSampleGetClientBrandReportData()
        {
            List<CPBCountTypeBrand> output = new List<CPBCountTypeBrand>
            {
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-13",
                    ClientBrandName = "Paychex",
                    CountAsOfDate = 20000,
                    ClientsPerBrandCountTypeName = "Active Client Count"
                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-20",
                    ClientBrandName = "Paychex",
                    CountAsOfDate = 20001,
                    ClientsPerBrandCountTypeName = "Active Client Count"
               },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-03-27",
                    ClientBrandName = "Paychex",
                    CountAsOfDate = 20002,
                    ClientsPerBrandCountTypeName = "Active Client Count"
               },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-04-03",
                    ClientBrandName = "Paychex",
                    CountAsOfDate = 20003,
                    ClientsPerBrandCountTypeName = "Active Client Count"
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
                $"from [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= '2021-03-01' " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= '2021-04-03';", null, CommandType.Text))
                    .Returns(GetSampleCount());

                var cls = mock.Create<CPBManager>();
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
                    .Setup(x => x.GetAll<CPB>($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerBrand]", null, CommandType.Text))
                    .Returns(GetSampleGetMostRecentDate());

                var cls = mock.Create<CPBManager>();
                var expected = GetSampleGetMostRecentDate();

                var actual = await cls.getMostRecentDate();

                string jsonActual = JsonConvert.SerializeObject(actual);
                string jsonExpected = JsonConvert.SerializeObject(expected);
                Xunit.Assert.Equal(jsonExpected, jsonActual);

            }
        }
        private List<CPB> GetSampleGetMostRecentDate()
        {
            List<CPB> output = new List<CPB>
            {
                new CPB
                {
                    ClientsPerBrandId = 0,
                    DateOfReport = "2021-04-03",
                    ClientBrandId = 0,
                    CountAsOfDate = 0,
                },
            };
            return output;
        }

        //
        // Get Most Recent Status Counts Valid Call
        //
        [Fact]
        public async void getMostRecentBrandCountsOfType_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.GetAll<CPBCountTypeBrand>($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " + "" +
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate " +
                $"FROM [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE DateOfReport = '2021-04-03' " +
                $"ORDER BY [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName,[dbo].[ClientBrand].ClientBrandId ASC", null, CommandType.Text))
                    .Returns(GetSampleGetMostRecentStatusCounts());

                var cls = mock.Create<CPBManager>();
                var expected = GetSampleGetMostRecentStatusCounts();

                var actual = await cls.getMostRecentBrandCountsOfType("2021-04-03");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].CountAsOfDate, actual[i].CountAsOfDate);
                    Xunit.Assert.Equal(expected[i].ClientBrandName, actual[i].ClientBrandName);
                }
            }
        }
        private List<CPBCountTypeBrand> GetSampleGetMostRecentStatusCounts()
        {
            List<CPBCountTypeBrand> output = new List<CPBCountTypeBrand>
            {
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-04-03",
                    ClientBrandName = "Paychex",
                    CountAsOfDate = 2000,

                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-04-03",
                    ClientBrandName = "Brand 2",
                    CountAsOfDate = 20001,
                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-04-03",
                    ClientBrandName = "Brand 3",
                    CountAsOfDate = 200,
                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-04-03",
                    ClientBrandName = "Brand 4",
                    CountAsOfDate = 100,
                },
                new CPBCountTypeBrand
                {
                    DateOfReport = "2021-04-03",
                    ClientBrandName = "Brand 5",
                    CountAsOfDate = 300,
                },
            };

            return output;
        }
    }
}
