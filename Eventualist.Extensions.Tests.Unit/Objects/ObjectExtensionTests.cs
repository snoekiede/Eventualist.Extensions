using System;
using Eventualist.Extensions.Objects;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Objects
{
    public class ObjectExtensionTests
    {
        #region IsNull Tests

        [Fact]
        public void IsNull_WithNullObject_ReturnsTrue()
        {
            // Arrange
            string? nullString = null;
            
            // Act
            var result = nullString.IsNull();
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNull_WithNonNullObject_ReturnsFalse()
        {
            // Arrange
            string nonNullString = "hello";
            
            // Act
            var result = nonNullString.IsNull();
            
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNull_WithNullReferenceType_ReturnsTrue()
        {
            // Arrange
            object? nullObject = null;
            
            // Act
            var result = nullObject.IsNull();
            
            // Assert
            Assert.True(result);
        }

        #endregion

        #region IsNotNull Tests

        [Fact]
        public void IsNotNull_WithNullObject_ReturnsFalse()
        {
            // Arrange
            string? nullString = null;
            
            // Act
            var result = nullString.IsNotNull();
            
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNotNull_WithNonNullObject_ReturnsTrue()
        {
            // Arrange
            string nonNullString = "hello";
            
            // Act
            var result = nonNullString.IsNotNull();
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNotNull_WithEmptyString_ReturnsTrue()
        {
            // Arrange
            string emptyString = "";
            
            // Act
            var result = emptyString.IsNotNull();
            
            // Assert
            Assert.True(result);
        }

        #endregion

        #region ThrowIfNull Tests

        [Fact]
        public void ThrowIfNull_WithNullObject_ThrowsArgumentNullException()
        {
            // Arrange
            string? nullString = null;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullString.ThrowIfNull());
        }

        [Fact]
        public void ThrowIfNull_WithNonNullObject_ReturnsObject()
        {
            // Arrange
            string nonNullString = "hello";
            
            // Act
            var result = nonNullString.ThrowIfNull();
            
            // Assert
            Assert.Equal(nonNullString, result);
        }

        [Fact]
        public void ThrowIfNull_WithNullObject_ExceptionContainsParameterName()
        {
            // Arrange
            string? testVariable = null;
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => testVariable.ThrowIfNull());
            Assert.Contains("testVariable", exception.ParamName);
        }

        [Fact]
        public void ThrowIfNull_WithCustomMessage_IncludesMessage()
        {
            // Arrange
            string? nullString = null;
            var customMessage = "Custom error message";
            
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => 
                nullString.ThrowIfNull(customMessage));
            Assert.Contains(customMessage, exception.Message);
        }

        [Fact]
        public void ThrowIfNull_WithCustomMessage_NonNull_ReturnsObject()
        {
            // Arrange
            string nonNullString = "hello";
            
            // Act
            var result = nonNullString.ThrowIfNull("Should not throw");
            
            // Assert
            Assert.Equal(nonNullString, result);
        }

        [Fact]
        public void ThrowIfNull_AllowsMethodChaining()
        {
            // Arrange
            string text = "hello";
            
            // Act
            var result = text.ThrowIfNull().ToUpper();
            
            // Assert
            Assert.Equal("HELLO", result);
        }

        #endregion
    }
}
