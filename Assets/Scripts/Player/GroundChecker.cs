using UnityEngine;

namespace Game.Player
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Collider2D _groundCheckingCollider;
        [SerializeField] private LayerMask _groundLayer;

        private ContactFilter2D _groundFilter = new ContactFilter2D();
        private Collider2D[] _groundResultsColliders = new Collider2D[2];

        #region Unity
        private void Awake()
        {
            _groundFilter.SetLayerMask(_groundLayer);
        }
        #endregion

        public bool IsGrounded()
        {
            return Physics2D.OverlapCollider(_groundCheckingCollider, _groundFilter, _groundResultsColliders) >= 1;
        }
    }
}

