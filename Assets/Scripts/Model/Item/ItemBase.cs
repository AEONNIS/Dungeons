using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Item", menuName = "Model/Item")]
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 7)] private string _description;
        [SerializeField] private float _maxStrength = 100.0f;
        [SerializeField] private float _destructionWarningStrength = 10.0f;
        [TextArea(3, 5)] [SerializeField] private string _destructionWarningMessage;
        [TextArea(3, 5)] [SerializeField] private string _destructionMessage;
        [SerializeField] private Sprite _sceneSprite;
        [SerializeField] private Sprite _inventorySprite;

        public ItemType Type => _type;
        public string Name => _name;
        public string Description => _description;
        public float MaxStrength => _maxStrength;
        public string DestructionWarningMessage => _destructionWarningMessage;
        public string DestructionMessage => _destructionMessage;
        public Sprite SceneSprite => _sceneSprite;
        public Sprite InventorySprite => _inventorySprite;
    }

    public enum ItemType { Axe, Pick, Shovel }
}
