using PipServicesLimitsDotnet.Data.Version1;

using System;
using System.Collections.Generic;

using PipServices.Commons.Data;

using Xunit;

namespace PipServicesLimitsDotnet.Data
{
    public static class TestModel
    {
        public static LimitV1 CreateLimit1()
        {
            return new LimitV1()
            {
                Id = "1",
                UserId = "00001",
                AmountUsed = 5000,
                Limit = 100000
            };
        }

        public static LimitV1 CreateLimit2()
        {
            return new LimitV1()
            {
                Id = "2",
                UserId = "00002",
                AmountUsed = 3000,
                Limit = 100000
            };
        }

        public static LimitV1 CreateLimit3()
        {
            return new LimitV1()
            {
                Id = "3",
                UserId = "00003",
                AmountUsed = 90000,
                Limit = 10000000
            };
        }

        public static void AssertEqual(LimitV1 expectedLimit, LimitV1 actualLimit)
        {
            Assert.Equal(expectedLimit.Id, actualLimit.Id);
            Assert.Equal(expectedLimit.UserId, actualLimit.UserId);
            Assert.Equal(expectedLimit.Limit, actualLimit.Limit);
            Assert.Equal(expectedLimit.AmountUsed, actualLimit.AmountUsed);
        }
    }
}
