using System;
using UnityEngine;

namespace Dungeons.Infrastructure.LocalizationSystem
{
    [Serializable]
    public class LocalizationFileItem
    {
        [SerializeField] private int _id;
        [SerializeField] private string _text;
    }
}
