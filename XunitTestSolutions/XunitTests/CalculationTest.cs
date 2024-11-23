using FluentAssertions;
using System.Collections;

namespace XunitTests
{
    public class CalculationTest
    {
        [Theory]
        [InlineData(0,0,0)]
        public void CalculateTest(int x, int y, int expectedResult)
        {
            var result = Calculate.Sum(x, y);

            result.Should().Be(expectedResult);
        }


        [Theory]
        [ClassData(typeof(CalculationData))]
        public void CalculateTestWithClassData(int x, int y, int expectedResult)
        {
            var result = Calculate.Sum(x, y);

            result.Should().Be(expectedResult);
        }

    }

    public class Calculate
    {
        public static int Sum(int x, int y)
        {
            return x + y;
        }
    }

    public class CalculationData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 10, 20, 30 };
            yield return new object[] { -10, -20, -30 };
            yield return new object[] { 10, -20, -10 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}