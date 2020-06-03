using System;
using System.Collections.Generic;
using System.Text;
using TicketManagement.API.Extensions;
using Xunit;

namespace TicketManagement.API_TEST.Extensions
{
    public class ValidationExtensionsTest
    {
        [Fact]
        public void ContainsUpperTrue()
        {
            //Arrange
            var test = "Test";

            //Act
            var action = test.ContainsUpper();

            //Assert
            Assert.True(action);
        }

        [Fact]
        public void ContainsUpperFalse()
        {
            //Arrange
            var test = "test";

            //Act
            var action = test.ContainsUpper();

            //Assert
            Assert.False(action);
        }

        [Fact]
        public void ContainsDigitTrue()
        {
            //Arrange
            var test = "test1";

            //Act
            var action = test.ContainsDigit();

            //Assert
            Assert.True(action);
        }

        [Fact]
        public void ContainsDigitFalse()
        {
            //Arrange
            var test = "test";

            //Act
            var action = test.ContainsDigit();

            //Assert
            Assert.False(action);
        }
    }
}
