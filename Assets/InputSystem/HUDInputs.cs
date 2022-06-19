using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class HUDInputs : MonoBehaviour
    {
        public InputActionAsset UIInputActionAsset;

        private InputAction m_ToogleInventory;
        private InputAction m_ToogleHelp;

        private void Awake()
        {
            m_ToogleInventory = UIInputActionAsset.FindActionMap("UI").FindAction("ToogleInventory");
            m_ToogleHelp = UIInputActionAsset.FindActionMap("UI").FindAction("ToogleHelp");


            m_ToogleInventory.performed += OnToogleInventory;
            m_ToogleHelp.performed += OnToggleHelp;

            m_ToogleInventory.Enable();
            m_ToogleHelp.Enable();
        }

        private void OnToggleHelp(InputAction.CallbackContext obj)
        {
            HUD.Instance.ToogleHelp();
        }

        public void OnToogleInventory(InputAction.CallbackContext obj)
        {
            HUD.Instance.ToogleInventory();

        }

        void OnDisable()
        {
            m_ToogleInventory.Disable();
            m_ToogleHelp.Disable();

        }


    }

}
