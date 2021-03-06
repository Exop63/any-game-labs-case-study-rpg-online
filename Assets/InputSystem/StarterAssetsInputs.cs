using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;

        public bool jump;
        public bool attack;

        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = false;
        public bool rightClick = false;

        private PhotonView photonView;

        void Start()
        {
            photonView = GetComponent<PhotonView>();
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
            else
            {
                LookInput(Vector2.zero);
            }
        }
        public void OnStartLook(InputValue value)
        {
            Debug.Log("OnStartLook:" + value.isPressed);
            RightClick(value.isPressed);
        }

        public void OnAttack(InputValue value)
        {
            Debug.Log("Attack");
            AttackInput(value.isPressed);
        }

        public void OnJump(InputValue value)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("JumpInput", RpcTarget.Others, value.isPressed);
            }
            {
                JumpInput(value.isPressed);
            }

        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }
        [PunRPC]
        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }
        public void AttackInput(bool newAttackState)
        {
            attack = newAttackState;
        }

        public void RightClick(bool newRightClick)
        {
            cursorInputForLook = newRightClick;
        }
        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            // SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}