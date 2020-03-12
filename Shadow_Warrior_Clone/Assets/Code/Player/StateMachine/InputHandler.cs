using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateCode
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private MoveSettings moveSettings;
        private IMoveInput moveInput;

        
        private StateManager sManager;

        private void Awake()
        {
            moveInput = new KeyboardInput();
        }

        private void Update()
        {
            moveInput.GetInput();


            UpdateStateManager();
        }

        void UpdateStateManager()
        {
            sManager.MoveVector = ReturnMovementVector(moveInput.Vertical, moveInput.Horizontal);
        }

        Vector3 ReturnMovementVector(float vertical, float horizontal)
        {
            Vector3 vector = Vector3.zero;
            vector.x = horizontal;
            vector.z = vertical;

            vector = vector.normalized;

            return vector;
        }
    }
}