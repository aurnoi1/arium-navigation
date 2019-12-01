using System;
using Xunit;

namespace TimeoutEx.UnitTests
{
    public class Int32ExTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void s_Should_Returns_Seconds(int value)
        {
            var expected = TimeSpan.FromSeconds(value);
            var actual = value.s();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Seconds_Should_Returns_Seconds()
        {
            int sut = 5;
            var expected = TimeSpan.FromSeconds(sut);

            var actual = sut.Seconds();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Negative_s_Should_Returns_Negative_Seconds()
        {
            int sut = -5;
            var expected = TimeSpan.FromSeconds(sut);

            var actual = sut.s();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ms_Should_Returns_Milliseconds()
        {
            int sut = 5;
            var expected = TimeSpan.FromMilliseconds(sut);

            var actual = sut.ms();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Milliseconds_Should_Returns_Milliseconds()
        {
            int sut = 5;
            var expected = TimeSpan.FromMilliseconds(sut);

            var actual = sut.Milliseconds();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Negative_ms_Should_Returns_Negative_Milliseconds()
        {
            int sut = -5;
            var expected = TimeSpan.FromMilliseconds(sut);

            var actual = sut.ms();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void m_Should_Returns_Minutes()
        {
            int sut = 5;
            var expected = TimeSpan.FromMinutes(sut);

            var actual = sut.m();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Minutes_Should_Returns_Minutes()
        {
            int sut = 5;
            var expected = TimeSpan.FromMinutes(sut);

            var actual = sut.Minutes();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Negative_m_Should_Returns_Negative_Minutes()
        {
            int sut = -5;
            var expected = TimeSpan.FromMinutes(sut);

            var actual = sut.m();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void h_Should_Returns_Hours()
        {
            int sut = 5;
            var expected = TimeSpan.FromHours(sut);

            var actual = sut.h();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Hours_Should_Returns_Hours()
        {
            int sut = 5;
            var expected = TimeSpan.FromHours(sut);

            var actual = sut.Hours();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Negative_h_Should_Returns_Negative_Hours()
        {
            int sut = -5;
            var expected = TimeSpan.FromHours(sut);

            var actual = sut.h();
            Assert.Equal(expected, actual);
        }
    }
}