using Game.Model.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Infrastructure.InventorySysytem
{
    public class ItemDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D _detectingCollider;
        [SerializeField] private LayerMask _itemLayers;

        private ContactFilter2D _itemFilter = new ContactFilter2D();

        #region Unity
        private void Awake()
        {
            _itemFilter.SetLayerMask(_itemLayers);
        }
        #endregion

        public List<Item> DetectItems()
        {
            List<Collider2D> itemsColliders = new List<Collider2D>();
            _detectingCollider.OverlapCollider(_itemFilter, itemsColliders);
            return GetItemsFrom(itemsColliders);
        }

        private List<Item> GetItemsFrom(List<Collider2D> itemsColliders)
        {
            List<Item> items = new List<Item>();

            foreach (var itemCollider in itemsColliders)
            {
                if (itemCollider.TryGetComponent(out Item item))
                    items.Add(item);
            }

            return items;
        }
    }
}
