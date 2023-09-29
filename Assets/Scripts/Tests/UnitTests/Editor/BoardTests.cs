using System;
using NUnit.Framework;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class BoardTests
    {
        [Test]
        public void BoardUpdate()
        {
            // Arrange
            int[,] state = {
                {0, 1, 2}
            };
            var board = Board.Create(state);

            // Act
            var result = board.GetBoardStateAsArrayWithTypes();
            // Assert
            Assert.AreEqual(result, state);

            // Arrange
            int[,] stateUpdated = {
                {0, 1, 2, 3},
                {0, 1, 2, 3}
            };
            board.Update(stateUpdated);

            // Act
            result = board.GetBoardStateAsArrayWithTypes();
            // Assert
            Assert.AreEqual(result, stateUpdated);
        }

        [Test]
        public void Width_GivenBoardDefinition_ShouldReturnLengthOfXAxis()
        {
            // Arrange
            int[,] state = {
                {0, 0, 0}
            };
            var board = Board.Create(state);

            // Act
            var width = board.Width;

            // Assert
            Assert.That(width, Is.EqualTo(3));
        }

        [Test]
        public void Height_GivenBoardDefinition_ShouldReturnLengthOfYAxis()
        {
            // Arrange
            int[,] state = {
                {0, 0, 0},
                {0, 0, 0}
            };
            var board = Board.Create(state);

            // Act
            var height = board.Height;

            // Assert
            Assert.That(height, Is.EqualTo(2));
        }

        [TestCase(0, 0, ExpectedResult = 1, TestName = "Coordinate: (0,0) = 1")]
        [TestCase(1, 1, ExpectedResult = 2, TestName = "Coordinate: (1,1) = 2")]
        [TestCase(2, 1, ExpectedResult = 3, TestName = "Coordinate: (2,1) = 3")]
        [TestCase(1, 0, ExpectedResult = 4, TestName = "Coordinate: (1,0) = 4")]
        public int GetAt_GivenCoordinate_ShouldReturnExpectedValue(int x, int y)
        {
            // Arrange
            int[,] state = {
                {1, 4, 0},
                {0, 2, 3}
            };
            var board = Board.Create(state);

            // Act & Assert
            return board.GetAt(x, y).Type;
        }

        [Test]
        public void GetAt_GivenCoordinatesOutsideBounds_ShouldThrowException()
        {
            // Arrange
            int[,] state = {
                {1, 0, 0},
                {0, 0, 0}
            };
            var board = Board.Create(state);

            // Act - Assert
            Assert.Throws<IndexOutOfRangeException>(() => board.GetAt(x: 42, y: 0));
        }

        [Test]
        public void Board_GivenDefinition_StateArrayHasSameOrientation()
        {
            // Arrange
            int[,] state = {
                {0, 1, 1, 1, 1 },
                {0, 2, 3, 4, 5 },
            };

            //Act            
            var board = Board.Create(state);

            //Assert
            Assert.That(board.GetAt(0, 0).Type, Is.EqualTo(0));
            Assert.That(board.GetAt(4, 1).Type, Is.EqualTo(5));
        }
    }

}