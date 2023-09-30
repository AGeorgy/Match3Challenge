using UnityEngine;
using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model.Solvers;

namespace Tactile.TactileMatch3Challenge.Settings
{
    [CreateAssetMenu(fileName = "SolverSettingProvider", menuName = "Tactile/SolverSettingProvider")]
    public class SolverSettingProvider : ScriptableObject
    {
        [SerializeReference, SubclassPicker]
        private List<ISolver> solvers;

        public ISolver[] GetSolvers()
        {
            return solvers.ToArray();
        }
    }
}