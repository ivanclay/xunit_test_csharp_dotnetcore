using System;
using Xunit;

namespace OnlineCourse.DomainTest
{
    public class FirstTest
    {
        [Fact]
        public void MustVariablesHasSameValue()
        {
            var variable1 = 1;
            var variable2 = 1;
            Assert.Equal(variable1, variable2);
        }
    }
}
