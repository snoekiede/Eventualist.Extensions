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
        public void Divide_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.Divide(3).ToList());
        }

        #region WhereNotNull Tests

        [Fact]
        public void WhereNotNull_WithMixedNullAndNonNull_FiltersOutNulls()
        {
            // Arrange
            var items = new[] { "a", null, "b", null, "c" };
            
            // Act
            var result = items.WhereNotNull().ToList();
            
            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "a", "b", "c" }, result);
        }

        [Fact]
        public void WhereNotNull_WithAllNulls_ReturnsEmpty()
        {
            // Arrange
            var items = new string?[] { null, null, null };
            
            // Act
            var result = items.WhereNotNull().ToList();
            
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void WhereNotNull_WithNoNulls_ReturnsAll()
        {
            // Arrange
            var items = new[] { "a", "b", "c" };
            
            // Act
            var result = items.WhereNotNull().ToList();
            
            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(items, result);
        }

        [Fact]
        public void WhereNotNull_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<string> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.WhereNotNull().ToList());
        }

        #endregion

        #region ContainsAll Tests

        [Fact]
        public void ContainsAll_WithAllItemsPresent_ReturnsTrue()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5 };
            
            // Act
            var result = numbers.ContainsAll(2, 4);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsAll_WithSomeItemsMissing_ReturnsFalse()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5 };
            
            // Act
            var result = numbers.ContainsAll(2, 6);
            
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ContainsAll_WithEmptyItemsList_ReturnsTrue()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };
            
            // Act
            var result = numbers.ContainsAll();
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsAll_WithDuplicateItems_ChecksCorrectly()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5 };
            
            // Act
            var result = numbers.ContainsAll(2, 2, 3, 3);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsAll_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.ContainsAll(1, 2));
        }

        #endregion
        
        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}