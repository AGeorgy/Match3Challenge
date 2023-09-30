using UnityEngine;
using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Settings
{
    [CreateAssetMenu(fileName = "SettingsProvider", menuName = "Tactile/SettingsProvider")]
    public abstract class BaseSettingsProvider<T, U> : ScriptableObject
    {
        [SerializeReference, SubclassPicker]
        protected List<T> settings;

        public virtual U[] GetAll()
        {
            return new U[0];
        }
    }
}