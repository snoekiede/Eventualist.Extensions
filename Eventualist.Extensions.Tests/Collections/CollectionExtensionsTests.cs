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
        
        [Fact]
        public void EmptyIfNull_WithNullCollection_ReturnsEmptyCollection()
        {
            // Arrange
            IEnumerable<int>? nullList = null;
            
            // Act
            var result = nullList.EmptyIfNull();
            
            // Assert
            Assert.Empty(result);
        }
        
        [Fact]
        public void EmptyIfNull_WithNonNullCollection_ReturnsSameCollection()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };
            
            // Act
            var result = numbers.EmptyIfNull();
            
            // Assert
            Assert.Same(numbers, result);
        }
        
        [Fact]
        public void ForEach_ExecutesActionOnEachElement()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };
            var sum = 0;
            
            // Act
            numbers.ForEach(n => sum += n);
            
            // Assert
            Assert.Equal(6, sum);
        }
        
        [Fact]
        public void ForEach_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<int> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.ForEach(n => { }));
        }
        
        [Fact]
        public void ForEach_WithNullAction_ThrowsArgumentNullException()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };
            Action<int> nullAction = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => numbers.ForEach(nullAction));
        }
        
        [Fact]
        public void DistinctBy_WithDuplicateKeys_RemovesDuplicates()
        {
            // Arrange
            var people = new[]
            {
                new Person { Id = 1, Name = "Alice" },
                new Person { Id = 2, Name = "Bob" },
                new Person { Id = 1, Name = "Alice Clone" }
            };
            
            // Act
            var result = people.DistinctBy(p => p.Id).ToList();
            
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal("Bob", result[1].Name);
        }
        
        [Fact]
        public void DistinctBy_WithNullCollection_ThrowsArgumentNullException()
        {
            // Arrange
            IEnumerable<Person> nullList = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.DistinctBy(p => p.Id).ToList());
        }
        
        [Fact]
        public void DistinctBy_WithNullKeySelector_ThrowsArgumentNullException()
        {
            // Arrange
            var people = new[] { new Person { Id = 1, Name = "Alice" } };
            Func<Person, int> nullSelector = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => people.DistinctBy(nullSelector).ToList());
        }
        
        private class Person
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}