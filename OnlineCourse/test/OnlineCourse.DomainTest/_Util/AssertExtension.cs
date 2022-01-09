using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnlineCourse.DomainTest._Util
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string message) 
        {
            if (exception.Message == message)
                Assert.True(true, $"Expected the message '{message}'");
            else
                Assert.False(true, $"Expected the message '{message}'");
        }
    }
}
