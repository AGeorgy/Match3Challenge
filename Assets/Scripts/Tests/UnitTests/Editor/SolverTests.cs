using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Solvers;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class SolverTests
    {
        [TestCase(1, 1, ExpectedResult = new int[0])]
        [TestCase(0, 0, ExpectedResult = new int[0])]
        [TestCase(3, 1, ExpectedResult = new[] { 4, 4, 4 })]
        [TestCase(2, 0, ExpectedResult = new[] { 2, 2, 2 })]
        public int[] ConnectedSameTypeSolver_ReturnsAllSolvedPieces(int x, int y)
        {
            // Arrange
            int[,] state = {
                {1, 2, 2, 4},
                {2, 3, 2, 4},
                {1, 2, 1, 4}
            };
            var board = Board.Create(state);
            var solver = new ConnectedSameTypeSolver();

            // Act
            var result = new Dictionary<Piece, ChangeInfo>();
            solver.Solve(x, y, board, result);

            // Assert
            return GetTypesFromPieces(result);
        }

        [Test]
        public void ConnectedSameTypeSolver_GivenLoopedCluster_ShouldReturnConnectionOfSameType()
        {
            // Arrange
            int[,] state = {
                {0, 0, 0, 0, 0, 0},
                {0, 1, 1, 1, 0, 0},
                {0, 1, 0, 1, 0, 0},
                {0, 1, 0, 1, 0, 0},
                {0, 1, 1, 1, 0, 0},
                {0, 0, 0, 0, 1, 1},
            };
            var board = Board.Create(state);
            var solver = new ConnectedSameTypeSolver();

            // Act
            var result = new Dictionary<Piece, ChangeInfo>();
            solver.Solve(1, 1, board, result);

            // Assert
            Assert.That(result.Count, Is.EqualTo(10));
        }

        [TestCase(1, 1, ExpectedResult = new[] { 2, 3, 2 })]
        [TestCase(0, 0, ExpectedResult = new[] { 1, 2, 1 })]
        [TestCase(3, 1, ExpectedResult = new[] { 4, 4, 4 })]
        [TestCase(2, 0, ExpectedResult = new[] { 2, 2, 1 })]
        public int[] VerticalLineSolver_ReturnsAllSolvedPieces(int x, int y)
        {
            // Arrange
            int[,] state = {
                {1, 2, 2, 4},
                {2, 3, 2, 4},
                {1, 2, 1, 4}
            };
            var board = Board.Create(state);
            var solver = new VerticalLineSolver();

            // Act
            var result = new Dictionary<Piece, ChangeInfo>();
            solver.Solve(x, y, board, result);

            // Assert
            return GetTypesFromPieces(result);
        }

        [TestCase(1, 1, ExpectedResult = new[] { 2, 3, 2, 4 })]
        [TestCase(0, 0, ExpectedResult = new[] { 1, 2, 2, 4 })]
        [TestCase(3, 1, ExpectedResult = new[] { 2, 3, 2, 4 })]
        [TestCase(2, 0, ExpectedResult = new[] { 1, 2, 2, 4 })]
        public int[] HorizontalLineSolver_ReturnsAllSolvedPieces(int x, int y)
        {
            // Arrange
            int[,] state = {
                {1, 2, 2, 4},
                {2, 3, 2, 4},
                {1, 2, 1, 4}
            };
            var board = Board.Create(state);
            var solver = new HorizontalLineSolver();

            // Act
            var result = new Dictionary<Piece, ChangeInfo>();
            solver.Solve(x, y, board, result);

            // Assert
            return GetTypesFromPieces(result);
        }

        private int[] GetTypesFromPieces(Dictionary<Piece, ChangeInfo> result)
        {
            return result.Select(p => p.Key.type).ToArray();
        }
    }
}