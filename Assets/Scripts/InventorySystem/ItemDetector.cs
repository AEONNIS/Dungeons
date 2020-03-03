using Game.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Game.InventorySysytem
{
    public class ItemDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D _detectingCollider;
        [SerializeField] private LayerMask _itemsLayer;

        private ContactFilter2D _itemFilter = new ContactFilter2D();

        #region Unity
        private void Awake()
        {
            _itemFilter.SetLayerMask(_itemsLayer);
        }
        #endregion

        public List<Item> GetItemsNearPlayer()
        {
            List<Collider2D> itemsColliders = new List<Collider2D>();
            Physics2D.OverlapCollider(_detectingCollider, _itemFilter, itemsColliders);
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
