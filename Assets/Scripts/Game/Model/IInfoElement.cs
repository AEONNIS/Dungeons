﻿using UnityEngine;

namespace Game.Model
{
    public interface IInfoElement
    {
        Sprite Sprite { get; }
        string Name { get; }
        string Description { get; }
        float Strength { get; }
    }
}
