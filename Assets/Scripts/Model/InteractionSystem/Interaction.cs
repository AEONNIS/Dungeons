using Dungeons.Model.Tiles;
using Dungeons.Model.PlayerCharacter;
using UnityEngine;

namespace Dungeons.Model.InteractionSystem
{
    [CreateAssetMenu(fileName = "Interaction", menuName = "Dungeons/Model/InteractionSystem/Interaction")]
    public class Interaction : ScriptableObject
    {
        [SerializeField] private DamagesTemplate _damagesTemplate;

        private Player _player;

        public void Init(Player player)
        {
            _player = player;
        }

        public void ApplyDamageToTileAndItemInHands(Tile environment)
        {
            Damage damage = _damagesTemplate.GetDamage(_player.Inventory.InHandsItem.Type, environment.Type);
            environment.ToDamage(damage.Tile);
            _player.Inventory.InHandsItem.ToDamage(damage.Item);
        }
    }
}
