using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class HUDInputs : MonoBehaviour
    {
        public InputActionAsset UIInputActionAsset;

        private InputAction m_UIActions;

        private void Awake()
        {
            m_UIActions = UIInputActionAsset.FindActionMap("UI").FindAction("ToogleInventory");
            m_UIActions.performed += OnToogleInventory;
            m_UIActions.Enable();
        }



        public void OnToogleInventory(InputAction.CallbackContext obj)
        {
            ToogleInventory();
        }

        void OnDisable()
        {
            m_UIActions.Disable();
        }





        public void ToogleInventory()
        {
            Debug.Log("ToogleInventory");
            HUD.Instance.ToogleInventory();
        }

    }

}
