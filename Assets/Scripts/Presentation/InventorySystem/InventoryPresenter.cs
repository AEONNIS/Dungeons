using UnityEngine;

namespace Dungeons.Presentation.InventorySystem
{
    public class InventoryPresenter : MonoBehaviour
    {
        public void SwitchState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
