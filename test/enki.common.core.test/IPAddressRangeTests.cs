namespace enki.common.core.test
{
    using enki.common.core;
    using System;
    using System.Net;
    using Xunit;

    public class IPAddressRangeTests
    {
        [Fact]
        public void IPAddressRange_Constructor_ValidCIDRRange_ShouldInitializeCorrectly()
        {
            // Arrange
            var ipRangeString = "192.168.0.0/24";

            // Act
            var ipRange = new IPAddressRange(ipRangeString);

            // Assert
            Assert.Equal(IPAddress.Parse("192.168.0.0"), ipRange.Begin);
            Assert.Equal(IPAddress.Parse("192.168.0.255"), ipRange.End);
        }

        [Fact]
        public void IPAddressRange_Constructor_ValidSingleAddress_ShouldInitializeCorrectly()
        {
            // Arrange
            var ipRangeString = "127.0.0.1";

            // Act
            var ipRange = new IPAddressRange(ipRangeString);

            // Assert
            Assert.Equal(IPAddress.Parse("127.0.0.1"), ipRange.Begin);
            Assert.Equal(IPAddress.Parse("127.0.0.1"), ipRange.End);
        }

        [Fact]
        public void IPAddressRange_Constructor_ValidRange_ShouldInitializeCorrectly()
        {
            // Arrange
            var ipRangeString = "169.254.0.0-169.254.0.255";

            // Act
            var ipRange = new IPAddressRange(ipRangeString);

            // Assert
            Assert.Equal(IPAddress.Parse("169.254.0.0"), ipRange.Begin);
            Assert.Equal(IPAddress.Parse("169.254.0.255"), ipRange.End);
        }

        [Fact]
        public void IPAddressRange_Constructor_ValidBitMask_ShouldInitializeCorrectly()
        {
            // Arrange
            var ipRangeString = "192.168.0.0/255.255.255.0";

            // Act
            var ipRange = new IPAddressRange(ipRangeString);

            // Assert
            Assert.Equal(IPAddress.Parse("192.168.0.0"), ipRange.Begin);
            Assert.Equal(IPAddress.Parse("192.168.0.255"), ipRange.End);
        }

        [Fact]
        public void IPAddressRange_Contains_IPAddressWithinRange_ShouldReturnTrue()
        {
            // Arrange
            var ipRangeString = "192.168.0.0-192.168.0.255";
            var ipRange = new IPAddressRange(ipRangeString);
            var ipAddress = IPAddress.Parse("192.168.0.128");

            // Act
            var result = ipRange.Contains(ipAddress);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IPAddressRange_Contains_IPAddressOutsideRange_ShouldReturnFalse()
        {
            // Arrange
            var ipRangeString = "192.168.0.0-192.168.0.255";
            var ipRange = new IPAddressRange(ipRangeString);
            var ipAddress = IPAddress.Parse("192.168.1.1");

            // Act
            var result = ipRange.Contains(ipAddress);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IPAddressRange_Constructor_InvalidRange_ShouldThrowFormatException()
        {
            // Arrange
            var ipRangeString = "invalid-ip-range";

            // Act & Assert
            Assert.Throws<FormatException>(() => new IPAddressRange(ipRangeString));
        }
    }

}