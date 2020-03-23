using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Environment", menuName = "Model/Environment")]
    public class EnvironmentBase : ScriptableObject
    {
        [SerializeField] private EnvironmentType _type;
        [SerializeField] private string _name;
        [TextArea(3, 7)]
        [SerializeField] private string _description;
        [SerializeField] private List<EnvironmentStrengthState> _strengthStates;
        [SerializeField] private float _shutdownTimeWhenDestroyed = 5.0f;

        public EnvironmentType Type => _type;
        public string Name => _name;
        public string Description => _description;
        public float ShutdownTimeWhenDestroyed => _shutdownTimeWhenDestroyed;

        public EnvironmentStrengthState GetStrengthState(float strength)
        {
            return _strengthStates.First(state => state.StrengthIsInRangeMinExclusive(strength));
        }
    }

    public enum EnvironmentType { BlackStone, GrayStone, RedStone, Ground, Ice, Tree }
}
