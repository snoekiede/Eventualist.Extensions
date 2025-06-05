using System;
using System.Collections.Generic;
using System.Linq;
using Eventualist.Extensions.Collections;
using Xunit;

namespace Eventualist.Extensions.Tests.Collections
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void IsEmpty_WithEmptyCollection_ReturnsTrue()
        {
            // Arrange
            var emptyList = Enumerable.Empty<int>();
            
            // Act
            var result = emptyList.IsEmpty();
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void IsEmpty_WithNonEmptyCollection_ReturnsFalse()
        {
            // Arrange
            var nonEmptyList = new[] { 1, 2, 3 };
            
            // Act
            var result = nonEmptyList.IsEmpty();
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void IsEmpty_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.IsEmpty());
        }
        
        [Fact]
        public void IsNotEmpty_WithEmptyCollection_ReturnsFalse()
        {
            // Arrange
            var emptyList = Enumerable.Empty<int>();
            
            // Act
            var result = emptyList.IsNotEmpty();
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void IsNotEmpty_WithNonEmptyCollection_ReturnsTrue()
        {
            // Arrange
            var nonEmptyList = new[] { 1, 2, 3 };
            
            // Act
            var result = nonEmptyList.IsNotEmpty();
            
            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void IsNotEmpty_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.IsNotEmpty());
        }
        
        [Fact]
        public void CreateOrderedString_WithIntegers_ReturnsOrderedString()
        {
            // Arrange
            var numbers = new[] { 3, 1, 4, 2 };
            
            // Act
            var result = numbers.CreateOrderedString(n => n);
            
            // Assert
            Assert.Equal("1,2,3,4", result);
        }
        
        [Fact]
        public void CreateOrderedString_WithCustomSeparator_UsesCustomSeparator()
        {
            // Arrange
            var numbers = new[] { 3, 1, 4, 2 };
            
            // Act
            var result = numbers.CreateOrderedString(n => n, " | ");
            
            // Assert
            Assert.Equal("1 | 2 | 3 | 4", result);
        }
        
        [Fact]
        public void CreateOrderedString_WithCustomFormatter_FormatsElements()
        {
            // Arrange
            var numbers = new[] { 3, 1, 4, 2 };
            
            // Act
            var result = numbers.CreateOrderedString(
                keySelector: n => n,
                separator: ",",
                elementStringSelector: n => $"Item-{n}");
            
            // Assert
            Assert.Equal("Item-1,Item-2,Item-3,Item-4", result);
        }
        
        [Fact]
        public void CreateOrderedString_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.CreateOrderedString(n => n));
        }
        
        [Fact]
        public void CreateOrderedString_WithNullKeySelector_ThrowsArgumentNullException()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };
            Func<int, int> nullSelector = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => numbers.CreateOrderedString(nullSelector));
        }
        
        [Fact]
        public void Divide_WithExactMultiple_CreatesEqualGroups()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5, 6 };
            
            // Act
            var result = numbers.Divide(3).ToList();
            
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(new[] { 1, 2, 3 }, result[0]);
            Assert.Equal(new[] { 4, 5, 6 }, result[1]);
        }
        
        [Fact]
        public void Divide_WithNonExactMultiple_CreatesUnequalLastGroup()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7 };
            
            // Act
            var result = numbers.Divide(3).ToList();
            
            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { 1, 2, 3 }, result[0]);
            Assert.Equal(new[] { 4, 5, 6 }, result[1]);
            Assert.Equal(new[] { 7 }, result[2]);
        }
        
        [Fact]
        public void Divide_WithEmptyCollection_ReturnsEmptyCollection()
        {
            // Arrange
            var emptyList = Enumerable.Empty<int>();
            
            // Act
            var result = emptyList.Divide(3).ToList();
            
            // Assert
            Assert.Empty(result);
        }
        
        [Fact]
        public void Divide_WithZeroGroupSize_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };
            
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => numbers.Divide(0).ToList());
        }
        
        [Fact]
        public void Divide_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.Divide(3).ToList());
        }
        

        
        
        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}