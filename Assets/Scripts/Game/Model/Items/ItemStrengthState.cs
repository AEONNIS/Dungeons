using System;
using UnityEngine;

namespace Game.Model.Items
{
    [Serializable]
    public struct ItemStrengthState
    {
        [SerializeField] private FloatRange _strengthRange;
        [SerializeField] private string _name;
        [TextArea(3, 5)]
        [SerializeField] private string _notifierMessage;

        public string NotifierMessage => _notifierMessage;

        public bool StrengthIsInRangeMinExclusive(float strength)
        {
            return _strengthRange.MinValue < strength && strength <= _strengthRange.MaxValue;
        }
    }
}
