using UnityEngine;
using Tactile.TactileMatch3Challenge.Level;
using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Goals;

namespace Tactile.TactileMatch3Challenge.Settings
{
    [CreateAssetMenu(fileName = "GoalProvider", menuName = "Tactile/GoalProvider")]
    public class GoalProvider : ScriptableObject
    {
        [SerializeReference, SubclassPicker]
        private List<IGoalSetting> goals;

        public IGoal[] GetGoals()
        {
            var result = new IGoal[goals.Count];
            for (int i = 0; i < goals.Count; i++)
            {
                result[i] = goals[i].GetGoal();
            }

            return result;
        }
    }
}