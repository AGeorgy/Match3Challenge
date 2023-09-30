using UnityEngine;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Goals;

namespace Tactile.TactileMatch3Challenge.Settings
{
    [CreateAssetMenu(fileName = "GoalProvider", menuName = "Tactile/GoalProvider")]
    public class GoalProvider : BaseSettingsProvider<IGoalSetting, IGoal>
    {
        public override IGoal[] GetAll()
        {
            var count = settings.Count;
            var result = new IGoal[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = settings[i].GetGoal();
            }

            return result;
        }
    }
}