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
using UserType = PaychexDataConsolidationTool.Entities.UserType;
using Moq;
using Dapper;

namespace PaychexDataConsolidationTool.Concrete.Tests
{
    [TestClass()]
    public class UPTManagerTests
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
                    .Setup(x => x.GetAll<UPT>($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[UsersPerType] WHERE DateOfReport >= @startDate AND DateOfReport <= @endDate ORDER BY DateOfReport ASC", It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(GetSampleDates());

                var cls = mock.Create<UPTManager>();
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
        private List<UPT> GetSampleDates()
        {
            List<UPT> output = new List<UPT>
            {
                new UPT
                {
                    DateOfReport = "2021-03-13"
                },
                new UPT
                {
                    DateOfReport = "2021-03-20"
                },
                new UPT
                {
                    DateOfReport = "2021-03-27"
                },
                new UPT
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
                    .Setup(x => x.GetAll<UserType>("SELECT UserTypeName FROM [dbo].[UserType] ORDER BY UserTypeId ASC", null, CommandType.Text))
                    .Returns(GetSampleTypes());

                var cls = mock.Create<UPTManager>();
                var expected = GetSampleTypes();

                var actual = await cls.getTypes();

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].UserTypeName, actual[i].UserTypeName);
                }
            }
        }

        private List<UserType> GetSampleTypes()
        {
            List<UserType> output = new List<UserType>
            {
                new UserType
                {
                    UserTypeName = "Flex"
                },
                new UserType
                {
                    UserTypeName = "Stretch"
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
                    .Setup(x => x.GetAll<UPTType>($"Select FORMAT ([dbo].[UsersPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[UserType].UserTypeName as UserTypeName, [dbo].[UsersPerType].UserTypeCountAsOfDate as UserTypeCountAsOfDate " +
                $"from [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE " +
                $"[dbo].[UsersPerType].DateOfReport >= @startDate " +
                $"AND [dbo].[UsersPerType].DateOfReport <= @endDate " +
                $"ORDER BY DateOfReport ASC OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;", It.IsAny<DynamicParameters>(),CommandType.Text))
                    .Returns(GetSampleListAll());

                var cls = mock.Create<UPTManager>();
                var expected = GetSampleListAll();

                var actual = await cls.ListAll(0, 8, "DateOfReport", "2021-03-01", "2021-04-03", "ASC");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].UserTypeCountAsOfDate, actual[i].UserTypeCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].UserTypeName, actual[i].UserTypeName);
                }
            }
        }
        private List<UPTType> GetSampleListAll()
        {
            List<UPTType> output = new List<UPTType>
            {
                new UPTType
                {
                    DateOfReport = "2021-03-20",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 3000,

                },
                new UPTType
                {
                    DateOfReport = "2021-03-20",
                    UserTypeName = "Stretch",
                    UserTypeCountAsOfDate = 32000,
                },
                new UPTType
                {
                    DateOfReport = "2021-03-27",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 100,
                },
                new UPTType
                {
                    DateOfReport = "2021-03-27",
                    UserTypeName = "Stretch",
                    UserTypeCountAsOfDate = 2000,
                },
                new UPTType
                {
                    DateOfReport = "2021-04-03",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 200,
                },
                new UPTType
                {
                    DateOfReport = "2021-04-03",
                    UserTypeName = "Stretch",
                    UserTypeCountAsOfDate = 8000,
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
                    .Setup(x => x.GetAll<UPTType>($"Select FORMAT ([dbo].[UsersPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[UserType].UserTypeName, [dbo].[UsersPerType].UserTypeCountAsOfDate " +
                $"from [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE  " +
                $"[dbo].[UsersPerType].DateOfReport >= @startDate " +
                $"AND[dbo].[UsersPerType].DateOfReport <= @endDate " +
                $"AND [dbo].[UserType].UserTypeName = @typeName " +
                $"ORDER BY[dbo].[UsersPerType].DateOfReport", It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(GetSampleGetTypeReportData());

                var cls = mock.Create<UPTManager>();
                var expected = GetSampleGetTypeReportData();

                var actual = await cls.getTypeReportData("2021-03-01", "2021-04-03", "Flex");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].UserTypeCountAsOfDate, actual[i].UserTypeCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].UserTypeName, actual[i].UserTypeName);
                }
            }
        }
        private List<UPTType> GetSampleGetTypeReportData()
        {
            List<UPTType> output = new List<UPTType>
            {
                new UPTType
                {
                    DateOfReport = "2021-03-13",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 20000,

                },
                new UPTType
                {
                    DateOfReport = "2021-03-20",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 20001,
                },
                new UPTType
                {
                    DateOfReport = "2021-03-27",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 20002,
                },
                new UPTType
                {
                    DateOfReport = "2021-04-03",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 20003,
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
                $"from [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE " +
                $"[dbo].[UsersPerType].DateOfReport >= @startDate " +
                $"AND [dbo].[UsersPerType].DateOfReport <= @endDate ;", It.IsAny<DynamicParameters>(),
                    CommandType.Text))
                    .Returns(GetSampleCount());

                var cls = mock.Create<UPTManager>();
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
                    .Setup(x => x.GetAll<UPT>($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[UsersPerType]", null, CommandType.Text))
                    .Returns(GetSampleGetMostRecentDate());

                var cls = mock.Create<UPTManager>();
                var expected = GetSampleGetMostRecentDate();

                var actual = await cls.getMostRecentDate();

                string jsonActual = JsonConvert.SerializeObject(actual);
                string jsonExpected = JsonConvert.SerializeObject(expected);
                Xunit.Assert.Equal(jsonExpected, jsonActual);

            }
        }
        private List<UPT> GetSampleGetMostRecentDate()
        {
            List<UPT> output = new List<UPT>
            {
                new UPT
                {
                    UserPerTypeId = 0,
                    DateOfReport = "2021-04-03",
                    UserTypeId = 0,
                    UserTypeCountAsOfDate = 0,
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
                    .Setup(x => x.GetAll<UPTType>($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[UserType].UserTypeName, [dbo].[UsersPerType].UserTypeCountAsOfDate " +
                $"FROM [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE DateOfReport = @date " +
                $"ORDER BY [dbo].[UserType].UserTypeId", It.IsAny<DynamicParameters>(), CommandType.Text))
                    .Returns(GetSampleGetMostRecentTypeCounts());

                var cls = mock.Create<UPTManager>();
                var expected = GetSampleGetMostRecentTypeCounts();

                var actual = await cls.getMostRecentTypeCounts("2021-04-03");

                Xunit.Assert.True(actual != null);
                Xunit.Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Xunit.Assert.Equal(expected[i].DateOfReport, actual[i].DateOfReport);
                    Xunit.Assert.Equal(expected[i].UserTypeCountAsOfDate, actual[i].UserTypeCountAsOfDate);
                    Xunit.Assert.Equal(expected[i].UserTypeName, actual[i].UserTypeName);
                }
            }
        }
        private List<UPTType> GetSampleGetMostRecentTypeCounts()
        {
            List<UPTType> output = new List<UPTType>
            {
                new UPTType
                {
                    DateOfReport = "2021-04-03",
                    UserTypeName = "Flex",
                    UserTypeCountAsOfDate = 2000,

                },
                new UPTType
                {
                    DateOfReport = "2021-04-03",
                    UserTypeName = "Stretch",
                    UserTypeCountAsOfDate = 20001,
                },
            };

            return output;
        }
    }
}
