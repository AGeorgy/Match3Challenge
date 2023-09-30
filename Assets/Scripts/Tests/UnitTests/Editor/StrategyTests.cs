using System.Collections.Generic;
using NUnit.Framework;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Model.Board;
using Tactile.TactileMatch3Challenge.Model.PieceGenerators;
using Tactile.TactileMatch3Challenge.Model.Solvers;
using Tactile.TactileMatch3Challenge.Model.Strategy;
using Tactile.TactileMatch3Challenge.PieceSpawn;

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
            var board = Board.Create(state);
            var pieceGenerator = new PieceGeneratorFake(8, 0, 5);
            var strategy = new SameTypeStrategy(null, pieceGenerator, new SolverProvider(new ConnectedSameTypeSolver()));

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

            var board = Board.Create(state);
            var pieceGenerator = new PieceGeneratorFake(8, 0, 5);
            var strategy = new SameTypeStrategy(null, pieceGenerator, new SolverProvider(new HorizontalLineSolver()));

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

            var board = Board.Create(state);
            var pieceGenerator = new PieceGeneratorFake(8, 0, 5);
            var strategy = new SameTypeStrategy(null, pieceGenerator, new SolverProvider(new VerticalLineSolver()));

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