using UnityEngine;
using System;

namespace Game.Model.InteractionSystem
{
    [Serializable]
    public class Damage
    {
        [SerializeField] private float _item;
        [SerializeField] private float _environment;

        public float Item => _item;
        public float Environment => _environment;
    }
}
