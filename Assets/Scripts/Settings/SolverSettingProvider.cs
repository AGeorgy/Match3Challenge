using UnityEngine;
using Tactile.TactileMatch3Challenge.Model.Solvers;

namespace Tactile.TactileMatch3Challenge.Settings
{
    [CreateAssetMenu(fileName = "SolverSettingProvider", menuName = "Tactile/SolverSettingProvider")]
    public class SolverSettingProvider : BaseSettingsProvider<ISolver, ISolver>
    {
        public override ISolver[] GetAll()
        {
            return settings.ToArray();
        }
    }
}