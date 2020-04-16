using System.Collections.Generic;
using UnityEngine;

namespace Game.Infrastructure
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private Collider2D _detectingCollider;
        [SerializeField] private LayerMask _layers;

        private ContactFilter2D _filter = new ContactFilter2D();

        #region Unity
        private void Awake()
        {
            _filter.SetLayerMask(_layers);
        }
        #endregion

        public List<T> Detect<T>() where T : MonoBehaviour
        {
            List<Collider2D> colliders = new List<Collider2D>();
            _detectingCollider.OverlapCollider(_filter, colliders);
            return GetFrom<T>(colliders);
        }

        private List<T> GetFrom<T>(List<Collider2D> colliders) where T : MonoBehaviour
        {
            List<T> components = new List<T>();

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out T component))
                    components.Add(component);
            }

            return components;
        }
    }
}
