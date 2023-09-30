using System;
using Tactile.TactileMatch3Challenge.Model;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Views.Animation
{
    public interface IAnimateSequance
    {
        void AnimateSequance(ResolveResult resolveResult, Action<GameObject> onDestroy, Action<Piece> onCreate);
    }
}