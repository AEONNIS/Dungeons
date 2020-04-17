using UnityEngine;

namespace Game.Presentation.UI.InventorySystem
{
    public class InventoryPresenter : MonoBehaviour
    {
        public void SwitchState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
