using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Environment", menuName = "Model/Environment")]
    public class EnvironmentBase : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 7)] private string _description;
        [SerializeField] private float _maxStrength;
        [SerializeField] private float _shutdownTimeWhenDestroyed = 5;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _damagedSprite;
        [SerializeField] private Sprite _destroyedSprite;

        public string Name => _name;
        public string Description => _description;
        public float MaxStrength => _maxStrength;
        public float ShutdownTimeWHenDestroyed => _shutdownTimeWhenDestroyed;
        public Sprite DefaultSprite => _defaultSprite;
        public Sprite DamagedSprite => _damagedSprite;
        public Sprite DestroyedSprite => _destroyedSprite;
    }
}
