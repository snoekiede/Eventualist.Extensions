using System;
using System.Collections.Generic;
using Eventualist.Extensions.Collections;
using Xunit;

namespace Eventualist.Extensions.Tests.Collections
{
    public class ExtendedDictionaryTests
    {
        #region Constructor Tests
        
        [Fact]
        public void DefaultConstructor_CreatesEmptyDictionary()
        {
            // Act
            var dictionary = new ExtendedDictionary<string, int>();
            
            // Assert
            Assert.Empty(dictionary);
        }
        
        [Fact]
        public void ComparerConstructor_UsesCustomComparer()
        {
            // Arrange
            var comparer = StringComparer.OrdinalIgnoreCase;
            
            // Act
            var dictionary = new ExtendedDictionary<string, int>(comparer)
            {
                ["Key"] = 1
            };

            // Assert
            Assert.Equal(1, dictionary["key"]); // Should find case-insensitive
        }
        
        [Fact]
        public void CapacityConstructor_InitializesWithCapacity()
        {
            // Act
            var dictionary = new ExtendedDictionary<string, int>(10);
            
            // Assert
            Assert.Empty(dictionary);
        }
        
        [Fact]
        public void DictionaryConstructor_CopiesEntries()
        {
            // Arrange
            var source = new Dictionary<string, int> 
            {
                ["one"] = 1,
                ["two"] = 2
            };
            
            // Act
            var dictionary = new ExtendedDictionary<string, int>(source);
            
            // Assert
            Assert.Equal(2, dictionary.Count);
            Assert.Equal(1, dictionary["one"]);
            Assert.Equal(2, dictionary["two"]);
        }
        
        [Fact]
        public void CapacityAndComparerConstructor_InitializesCorrectly()
        {
            // Arrange
            var comparer = StringComparer.OrdinalIgnoreCase;
            
            // Act
            var dictionary = new ExtendedDictionary<string, int>(10, comparer)
            {
                ["Key"] = 1
            };

            // Assert
            Assert.Equal(1, dictionary["key"]); // Should find case-insensitive
        }
        
        [Fact]
        public void DictionaryAndComparerConstructor_InitializesCorrectly()
        {
            // Arrange
            var source = new Dictionary<string, int> 
            {
                ["Key"] = 1
            };
            var comparer = StringComparer.OrdinalIgnoreCase;
            
            // Act
            var dictionary = new ExtendedDictionary<string, int>(source, comparer);
            
            // Assert
            Assert.Equal(1, dictionary["key"]); // Should find case-insensitive
        }
        
        #endregion
        
        #region Standard Indexer Tests
        
        [Fact]
        public void StandardIndexer_GetExistingKey_ReturnsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int> { ["key"] = 42 };
            
            // Act
            var result = dictionary["key"];
            
            // Assert
            Assert.Equal(42, result);
        }
        
        [Fact]
        public void StandardIndexer_GetNonExistingKey_ReturnsDefault()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            
            // Act
            var result = dictionary["nonexistent"];
            
            // Assert
            Assert.Equal(0, result); // Default for int is 0
        }
        
        [Fact]
        public void StandardIndexer_SetNewKey_AddsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>
            {
                // Act
                ["new"] = 100
            };

            // Assert
            Assert.Equal(100, dictionary["new"]);
            Assert.Single(dictionary);
        }
        
        [Fact]
        public void StandardIndexer_SetExistingKey_UpdatesValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>
            {
                ["key"] = 42,
                ["key"] = 100
            };

            // Assert
            Assert.Equal(100, dictionary["key"]);
            Assert.Single(dictionary);
        }
        
        #endregion
        
        #region Default Value Indexer Tests
        
        [Fact]
        public void DefaultValueIndexer_GetExistingKey_ReturnsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int> { ["key"] = 42 };
            
            // Act
            var result = dictionary["key", 999];
            
            // Assert
            Assert.Equal(42, result);
        }
        
        [Fact]
        public void DefaultValueIndexer_GetNonExistingKey_ReturnsDefaultValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            
            // Act
            var result = dictionary["nonexistent", 999];
            
            // Assert
            Assert.Equal(999, result);
        }
        
        [Fact]
        public void DefaultValueIndexer_SetNewKey_AddsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>
            {
                // Act
                ["new", 999] = 100
            };

            // Assert
            Assert.Equal(100, dictionary["new"]);
            Assert.Single(dictionary);
        }
        
        [Fact]
        public void DefaultValueIndexer_SetExistingKey_UpdatesValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>
            {
                ["key"] = 42,
                // Act
                ["key", 999] = 100
            };

            // Assert
            Assert.Equal(100, dictionary["key"]);
            Assert.Single(dictionary);
        }
        
        #endregion
        
        #region GetValueOrDefault Tests
        
        [Fact]
        public void GetValueOrDefault_WithSpecifiedDefault_ExistingKey_ReturnsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int> { ["key"] = 42 };
            
            // Act
            var result = dictionary.GetValueOrDefault("key", 999);
            
            // Assert
            Assert.Equal(42, result);
        }
        
        [Fact]
        public void GetValueOrDefault_WithSpecifiedDefault_NonExistingKey_ReturnsSpecifiedDefault()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            
            // Act
            var result = dictionary.GetValueOrDefault("nonexistent", 999);
            
            // Assert
            Assert.Equal(999, result);
        }
        
        [Fact]
        public void GetValueOrDefault_WithoutSpecifiedDefault_ExistingKey_ReturnsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int> { ["key"] = 42 };
            
            // Act
            var result = dictionary.GetValueOrDefault("key");
            
            // Assert
            Assert.Equal(42, result);
        }
        
        [Fact]
        public void GetValueOrDefault_WithoutSpecifiedDefault_NonExistingKey_ReturnsTypeDefault()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            
            // Act
            var result = dictionary.GetValueOrDefault("nonexistent");
            
            // Assert
            Assert.Equal(0, result); // Default for int is 0
        }
        
        #endregion
        
        #region GetOrAdd Tests
        
        [Fact]
        public void GetOrAdd_WithValue_NewKey_AddsAndReturnsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            
            // Act
            var result = dictionary.GetOrAdd("new", 42);
            
            // Assert
            Assert.Equal(42, result);
            Assert.Equal(42, dictionary["new"]);
            Assert.Single(dictionary);
        }
        
        [Fact]
        public void GetOrAdd_WithValue_ExistingKey_ReturnsExistingValueWithoutChanging()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int> { ["key"] = 42 };
            
            // Act
            var result = dictionary.GetOrAdd("key", 999);
            
            // Assert
            Assert.Equal(42, result);
            Assert.Equal(42, dictionary["key"]);
            Assert.Single(dictionary);
        }
        
        [Fact]
        public void GetOrAdd_WithFactory_NewKey_InvokesFactoryAddsAndReturnsValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            bool factoryInvoked = false;
            
            // Act
            var result = dictionary.GetOrAdd("new", key => {
                factoryInvoked = true;
                return 42;
            });
            
            // Assert
            Assert.True(factoryInvoked);
            Assert.Equal(42, result);
            Assert.Equal(42, dictionary["new"]);
            Assert.Single(dictionary);
        }
        
        [Fact]
        public void GetOrAdd_WithFactory_ExistingKey_DoesNotInvokeFactoryAndReturnsExistingValue()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int> { ["key"] = 42 };
            bool factoryInvoked = false;
            
            // Act
            var result = dictionary.GetOrAdd("key", key => {
                factoryInvoked = true;
                return 999;
            });
            
            // Assert
            Assert.False(factoryInvoked);
            Assert.Equal(42, result);
            Assert.Equal(42, dictionary["key"]);
            Assert.Single(dictionary);
        }
        
        [Fact]
        public void GetOrAdd_WithNullFactory_ThrowsArgumentNullException()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<string, int>();
            Func<string, int> factory = null!;
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => dictionary.GetOrAdd("key", factory));
        }
        
        #endregion
        
        #region Reference Type Tests
        
        [Fact]
        public void ReferenceType_GetNonExisting_ReturnsNull()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<int, string>();
            
            // Act
            var result = dictionary[5];
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void ReferenceType_GetValueOrDefault_NonExisting_ReturnsNull()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<int, string>();
            
            // Act
            var result = dictionary.GetValueOrDefault(5);
            
            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void ReferenceType_GetValueOrDefault_NonExisting_ReturnsCustomDefault()
        {
            // Arrange
            var dictionary = new ExtendedDictionary<int, string>();
            
            // Act
            var result = dictionary.GetValueOrDefault(5, "default");
            
            // Assert
            Assert.Equal("default", result);
        }
        
        #endregion
    }
}