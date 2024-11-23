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
        [InlineData(int.MaxValue, 0)]
        [Trait("Exception", null)]
        public void DivideByZeroException(int x, int y)
        {
            Action result = ()=> Calculate.Divide(x, y);

            result.Should().Throw<DivideByZeroException>();
        }

        [Theory]
        [ClassData(typeof(CalculationData))]
        public void CalculateTestWithClassData(int x, int y, int expectedResult)
        {
            var result = Calculate.Sum(x, y);

            result.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(GetCalculationDatas))]
        public void CalculateTestWithMemberData(int x, int y, int expectedResult)
        {
            var result = Calculate.Sum(x, y);

            result.Should().Be(expectedResult);
        }


        [Fact]
        [Trait("Exception", null)]
        public void CustomExceptionTest()
        {
             Action action = ()=> Calculate.PerformOperation();

            action.Should().Throw<CustomException>()
                .WithMessage("Operation Failed")
                .Where(ex=>ex.ErrorCode ==404);
        }

        [Fact(Skip = "This test is temporary disabled")]
        public void SkipTemporaryDisabledTest()
        {

        }


        public static IEnumerable<object[]> GetCalculationDatas()
        {
            return new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { -1, -2, -3 }
            };
        }
    }

    public class Calculate
    {
        public static int Sum(int x, int y)
        {
            return x + y;
        }

        public static int Divide(int x, int y)
        {
            return x / y;
        }

        public static void PerformOperation()
        {
            throw new CustomException("Operation Failed", 404);
        }
    }

    public class CustomException : Exception
    {
        public int ErrorCode { get; }

        public CustomException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
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