using Game.Model.PlayerCharacter;
using UnityEngine;

namespace Game.Model
{
    [CreateAssetMenu(fileName = "Interaction", menuName = "Model/Interaction")]
    public class Interaction : ScriptableObject
    {
        [SerializeField] private DamagesTemplate _damagesTemplate;

        private Player _player;

        public void Init(Player player)
        {
            _player = player;
        }

        public void ApplyDamageToEnvironmentAndItemInHands(Environment environment)
        {
            Damage damage = _damagesTemplate.GetDamage(_player.Inventory.InHandsItem.Type, environment.Type);
            environment.ToDamage(damage.Environment);
            _player.Inventory.InHandsItem.ToDamage(damage.Item);
        }
    }
}
