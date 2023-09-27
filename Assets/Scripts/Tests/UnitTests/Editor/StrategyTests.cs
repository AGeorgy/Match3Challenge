using System.Collections.Generic;
using NUnit.Framework;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Solvers;
using Tactile.TactileMatch3Challenge.Strategy;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class StrategyTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Resolve_GivenSelectedPieceInAsymmetricalCluster_ShouldRemoveAllNeighborsAndMovePiecesToAvailableSlots(int swapValue)
        {
            var v = swapValue;
            // Arrange
            int[,] state = {
                {0, 4, 4, 4, 4},
                {0, 2, 3, 1, 4},
                {4, 4, 1, 4, 0},
                {v, v, 4, 0, 0}
            };
            var randomSpawner = new PieceSpawnerFake(8, new ConnectedSameTypeSolver());
            var board = new Board(state);
            var strategy = new SameTypeStrategy(randomSpawner);

            // Act
            var resultTemp = new Dictionary<Piece, ChangeInfo>();
            var isSolved = strategy.Solve(1, 3, board, resultTemp);
            strategy.Fill(board, resultTemp);

            // Assert
            int[,] expected = {
                {8, 8, 4, 4, 4},
                {0, 4, 3, 1, 4},
                {0, 2, 1, 4, 0},
                {4, 4, 4, 0, 0}
            };
            var result = board.GetBoardStateAsArrayWithTypes();

            Assert.That(isSolved, Is.True);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Resolve_GivenSelectedPieceInAsymmetricalCluster_ShouldRemoveHorizontalLineAndMovePiecesToAvailableSlots(int swapValue)
        {
            var v = swapValue;
            // Arrange
            int[,] state = {
                {0, 4, 4, 4, 4},
                {0, 2, 3, 1, 4},
                {4, 4, 1, 4, 0},
                {v, v, 4, 0, 0}
            };
            var randomSpawner = new PieceSpawnerFake(8, new HorizontalLineSolver());
            var board = new Board(state);
            var strategy = new SameTypeStrategy(randomSpawner);

            // Act
            var resultTemp = new Dictionary<Piece, ChangeInfo>();
            var isSolved = strategy.Solve(1, 3, board, resultTemp);
            strategy.Fill(board, resultTemp);

            // Assert
            int[,] expected = {
                {8, 8, 8, 8, 8},
                {0, 4, 4, 4, 4},
                {0, 2, 3, 1, 4},
                {4, 4, 1, 4, 0},
            };
            var result = board.GetBoardStateAsArrayWithTypes();

            Assert.That(isSolved, Is.True);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Resolve_GivenSelectedPieceInAsymmetricalCluster_ShouldRemoveVerticalLineAndMovePiecesToAvailableSlots(int swapValue)
        {
            var v = swapValue;
            // Arrange
            int[,] state = {
                {0, 4, 4, 4, 4},
                {0, 2, 3, 1, 4},
                {4, 4, 1, 4, 0},
                {v, v, 4, 0, 0}
            };
            var randomSpawner = new PieceSpawnerFake(8, new VerticalLineSolver());
            var board = new Board(state);
            var strategy = new SameTypeStrategy(randomSpawner);

            // Act
            var resultTemp = new Dictionary<Piece, ChangeInfo>();
            var isSolved = strategy.Solve(1, 3, board, resultTemp);
            strategy.Fill(board, resultTemp);

            // Assert
            int[,] expected = {
                {0, 8, 4, 4, 4},
                {0, 8, 3, 1, 4},
                {4, 8, 1, 4, 0},
                {v, 8, 4, 0, 0}
            };
            var result = board.GetBoardStateAsArrayWithTypes();

            Assert.That(isSolved, Is.True);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}