using UnityEngine;
using System;

namespace Game.Model
{
    [Serializable]
    public class InteractionCouple
    {
        [SerializeField] private ItemBase _item;
        [SerializeField] private EnvironmentBase _environment;

        public ItemBase Item => _item;
        public EnvironmentBase Environment => _environment;
    }
}
