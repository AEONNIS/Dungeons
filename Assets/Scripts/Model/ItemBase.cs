using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Item", menuName = "Model/Item")]
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] [TextArea(3, 7)] private string _description;
        [SerializeField] private float _maxStrength;
        [SerializeField] private Sprite _sceneSprite;
        [SerializeField] private Sprite _inventorySprite;

        public string Name => _name;
        public string Description => _description;
        public float MaxStrength => _maxStrength;
        public Sprite SceneSprite => _sceneSprite;
        public Sprite InventorySprite => _inventorySprite;
    }
}
