using Game.Model.PlayerCharacter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlayerPanel : MonoBehaviour
    {
        [SerializeField] private ColorValueBar _healthBar;
        [SerializeField] private Text _powerState;
        [TextArea(1, 2)] [SerializeField] private List<string> _powerStateNames;

        public void Present(float health, PowerState powerState)
        {
            PresentHealth(health);
            PresentPowerState(powerState);
        }

        public void PresentHealth(float healthValue)
        {
            _healthBar.Present(healthValue);
        }

        public void PresentPowerState(PowerState powerState)
        {
            _powerState.text = _powerStateNames[(int)powerState];
        }
    }
}
