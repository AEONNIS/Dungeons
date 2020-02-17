using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Environment", menuName = "Model/Environment")]
    public class EnvironmentBase : ScriptableObject
    {
        [SerializeField] private EnvironmentType _type;
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 7)] private string _description;
        [SerializeField] private float _minStrengthForNoDamage = 90.0f;
        [SerializeField] private float _minStrengthForStatic = 30.0f;
        [SerializeField] private float _shutdownTimeWhenDestroyed = 5.0f;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _damagedSprite;
        [SerializeField] private Sprite _destroyedSprite;

        public EnvironmentType Type => _type;
        public string Name => _name;
        public string Description => _description;
        public float MinStrengthForNoDamage => _minStrengthForNoDamage;
        public float MinStrengthForStatic => _minStrengthForStatic;
        public float ShutdownTimeWhenDestroyed => _shutdownTimeWhenDestroyed;
        public Sprite DefaultSprite => _defaultSprite;
        public Sprite DamagedSprite => _damagedSprite;
        public Sprite DestroyedSprite => _destroyedSprite;
    }

    public enum EnvironmentType { BlackStone, GrayStone, RedStone, Ground, Ice, Tree }
}
