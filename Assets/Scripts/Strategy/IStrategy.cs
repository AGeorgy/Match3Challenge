using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Strategy
{
    public interface IStrategy
    {
        GameObject CreateVisualPiece(Piece piece);
        void Fill(IBoard board, Dictionary<Piece, ChangeInfo> result);
        void Reset();
        bool Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result);
    }
}