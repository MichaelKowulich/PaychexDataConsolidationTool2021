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
using Type = PaychexDataConsolidationTool.Entities.Type;

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
                    .Setup(x => x.GetAll<Type>("SELECT TypeName FROM [dbo].[Type] ORDER BY TypeId ASC", null, CommandType.Text))
                    .Returns(GetSampleTypes());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleTypes();

                var actual = await cls.getTypes();

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].TypeName, actual[i].TypeName);
                }
            }
        }

        private List<Type> GetSampleTypes()
        {
            List<Type> output = new List<Type>
            {
                new Type
                {
                    TypeName = "Standalone Clients"
                },
                new Type
                {
                    TypeName = "Nettime Organic Clients"
                },
                new Type
                {
                    TypeName = "FlexTime Clients"
                },
                new Type
                {
                    TypeName = "Essentials Clients"
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
                    .Setup(x => x.GetAll<CPTType>($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName as TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate as TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
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
                    Xunit.Assert.Equal(expected[i].TypeName, actual[i].TypeName);
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
                    TypeName = "Essentials Clients",
                    TypeCountAsOfDate = 3000,

                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "FlexTime Clients",
                    TypeCountAsOfDate = 32000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "Nettime Organic Clients",
                    TypeCountAsOfDate = 100,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "Standalone",
                    TypeCountAsOfDate = 2000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "C2C All Clients",
                    TypeCountAsOfDate = 200,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "StratusTime Integrated Clients",
                    TypeCountAsOfDate = 8000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "C2C CLients with same FEIN",
                    TypeCountAsOfDate = 40000,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-13",
                    TypeName = "C2C CLients with different FEIN",
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
                    .Setup(x => x.GetAll<CPTType>($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"WHERE  " +
                $"[dbo].[ClientsPerType].DateOfReport >= '2021-03-01' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '2021-04-03' " +
                $"AND [dbo].[Type].TypeName = 'Standalone' " +
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
                    Xunit.Assert.Equal(expected[i].TypeName, actual[i].TypeName);
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
                    TypeName = "Standalone",
                    TypeCountAsOfDate = 20000,

                },
                new CPTType
                {
                    DateOfReport = "2021-03-20",
                    TypeName = "Standalone",
                    TypeCountAsOfDate = 20001,
                },
                new CPTType
                {
                    DateOfReport = "2021-03-27",
                    TypeName = "Standalone",
                    TypeCountAsOfDate = 20002,
                },
                new CPTType
                {
                    DateOfReport = "2021-04-03",
                    TypeName = "Standalone",
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
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
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
        //  Count After Search Valid Call
        //

        [Fact]
        public async void CountAfterSearch_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IDapperManager>()
                    .Setup(x => x.Get<int>($"select COUNT(*) from [CPT] WHERE Date >= '2021-03-01' AND Date <= '2021-04-03'", null, CommandType.Text))
                    .Returns(GetSampleCountAfterSearch());

                var cls = mock.Create<CPTManager>();
                var expected = GetSampleCountAfterSearch();

                var actual = await cls.CountAfterSearch("2021-03-01", "2021-04-03");

                Xunit.Assert.Equal(actual, expected);

            }
        }
        private int GetSampleCountAfterSearch()
        {
            return 32;
        }
    }
}
