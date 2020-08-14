using Dungeons.Model.InteractionSystem;
using Dungeons.Model.LocalizationSystemOLD2;
using Dungeons.Model.PlayerCharacter;
using UnityEngine;

namespace Dungeons.Operations
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private Player _player;
        [SerializeField] private Interaction _interaction;

        #region Unity
        private void Start()
        {
            StartLaunch();
        }
        #endregion

        private void StartLaunch()
        {
            _localizer.Init();
            _player.Init();
            _interaction.Init(_player);
        }
    }
}
