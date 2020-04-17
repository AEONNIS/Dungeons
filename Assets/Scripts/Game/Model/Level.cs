using Game.Model.InteractionSystem;
using Game.Model.PlayerCharacter;
using UnityEngine;

namespace Game.Model
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] Interaction _interaction;

        #region Unity
        private void Awake()
        {
            _interaction.Init(_player);
        }
        #endregion
    }
}
