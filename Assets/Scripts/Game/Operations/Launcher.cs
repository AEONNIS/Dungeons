using Game.Model.PlayerCharacter;
using Game.Operations.LocalizationSystem;
using UnityEngine;

namespace Game.Operations
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private Localizer _localizer;
        [SerializeField] private Player _player;

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
        }
    }
}
