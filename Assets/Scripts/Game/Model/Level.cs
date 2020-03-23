using Game.Model;
using Game.Model.PlayerCharacter;
using UnityEngine;

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
