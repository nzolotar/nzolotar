using Xunit;
using System;
using ConsoleApp_StackProblem;

namespace ConsoleApp_StackProblemTests
{
    public class CustomStackTests

    {
        [Fact]
        public void Push_ValidItem_IncreasesCount()
        {
            // Arrange
            var stack = new CustomStack<int>();

            // Act
            stack.Push(1);

            // Assert
            Assert.Equal(1, stack.Count);
            Assert.False(stack.IsEmpty);
        }

        [Fact]
        public void Push_WhenStackIsFull_ThrowsException()
        {
            // Arrange
            var stack = new CustomStack<int>(maxSize: 1);
            stack.Push(1);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => stack.Push(2));
            Assert.Equal("Stack is full", exception.Message);
        }

        [Fact]
        public void Pop_EmptyStack_ThrowsException()
        {
            // Arrange
            var stack = new CustomStack<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => stack.Pop());
            Assert.Equal("Stack is empty", exception.Message);
        }

        [Fact]
        public void Pop_ValidStack_ReturnsLastItem()
        {
            // Arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);

            // Act
            var result = stack.Pop();

            // Assert
            Assert.Equal(2, result);
            Assert.Equal(1, stack.Count);
        }

        [Fact]
        public void Peek_EmptyStack_ThrowsException()
        {
            // Arrange
            var stack = new CustomStack<int>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => stack.Peek());
            Assert.Equal("Stack is empty", exception.Message);
        }

        [Fact]
        public void Peek_ValidStack_ReturnsLastItemWithoutRemoving()
        {
            // Arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);

            // Act
            var result = stack.Peek();

            // Assert
            Assert.Equal(2, result);
            Assert.Equal(2, stack.Count);
        }

        [Fact]
        public void TryPeek_EmptyStack_ReturnsFalse()
        {
            // Arrange
            var stack = new CustomStack<int>();

            // Act
            bool success = stack.TryPeek(out int result);

            // Assert
            Assert.False(success);
            Assert.Equal(default, result);
        }

        [Fact]
        public void TryPeek_ValidStack_ReturnsTrueAndValue()
        {
            // Arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);

            // Act
            bool success = stack.TryPeek(out int result);

            // Assert
            Assert.True(success);
            Assert.Equal(2, result);
        }

        [Fact]
        public void TryPop_EmptyStack_ReturnsFalse()
        {
            // Arrange
            var stack = new CustomStack<int>();

            // Act
            bool success = stack.TryPop(out int result);

            // Assert
            Assert.False(success);
            Assert.Equal(default, result);
        }

        [Fact]
        public void TryPop_ValidStack_ReturnsTrueAndRemovesItem()
        {
            // Arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);

            // Act
            bool success = stack.TryPop(out int result);

            // Assert
            Assert.True(success);
            Assert.Equal(2, result);
            Assert.Equal(1, stack.Count);
        }

        [Fact]
        public void Clear_RemovesAllItems()
        {
            // Arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);

            // Act
            stack.Clear();

            // Assert
            Assert.Equal(0, stack.Count);
            Assert.True(stack.IsEmpty);
        }

        [Fact]
        public void ToArray_ReturnsCorrectArray()
        {
            // Arrange
            var stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            // Act
            var array = stack.ToArray();

            // Assert
            Assert.Equal(3, array.Length);
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(3, array[2]);
        }

        [Fact]
        public void Constructor_ValidMaxSize_CreatesEmptyStack()
        {
            // Act
            var stack = new CustomStack<int>(5);

            // Assert
            Assert.True(stack.IsEmpty);
            Assert.Equal(0, stack.Count);
        }
    }
}