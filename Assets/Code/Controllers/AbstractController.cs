﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Controllers
{
    public abstract class AbstractController : MonoBehaviour
    {
        private void FixedUpdate()
        {
            var gamePad = Gamepad.current;
            var mouse = Mouse.current;
            var holdBlocked = mouse != null && mouse.leftButton.wasPressedThisFrame || gamePad != null && gamePad.buttonSouth.wasPressedThisFrame;
            
            if (mouse != null && mouse.leftButton.wasPressedThisFrame || gamePad != null && gamePad.buttonSouth.wasPressedThisFrame)
            {
                GetMainButtonDown();
            }

            if (mouse != null && mouse.leftButton.wasReleasedThisFrame || gamePad != null && gamePad.buttonSouth.wasReleasedThisFrame)
            {
                GetMainButtonUp();
            }

            if ((mouse != null && mouse.leftButton.isPressed || gamePad != null && gamePad.buttonSouth.isPressed) && !holdBlocked)
            {
                GetMainButton();
            }
            
            if (mouse != null && mouse.rightButton.wasPressedThisFrame || gamePad != null && gamePad.buttonEast.wasPressedThisFrame)
            {
                GetSecondaryButtonDown();
            }

            if (mouse != null && mouse.rightButton.wasReleasedThisFrame || gamePad != null && gamePad.buttonEast.wasReleasedThisFrame)
            {
                GetSecondaryButtonUp();
            }

            if ((mouse != null && mouse.rightButton.isPressed || gamePad != null && gamePad.buttonEast.isPressed) && !holdBlocked)
            {
                GetSecondaryButton();
            }
        }

        public abstract void GetMainButtonDown();
        public abstract void GetMainButtonUp();
        public abstract void GetMainButton();
        public abstract void GetSecondaryButtonDown();
        public abstract void GetSecondaryButtonUp();
        public abstract void GetSecondaryButton();
    }
}