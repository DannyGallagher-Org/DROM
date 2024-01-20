using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Code.UI
{
    public class MouseView : MonoBehaviour
    {
        private Color _baseColor = new Color(1f, 1f, 1f, 0.35f);
        private Color _pressColor = new Color(0.47f, 0.49f, 1f, 0.35f);
        private Color _holdColor = new Color(0.38f, 1f, 0.33f, 0.35f);
        
        [SerializeField] private Image MainButton;
        [SerializeField] private Image SecondaryButton;
        private void FixedUpdate()
        {
            var gamePad = Gamepad.current;
            var mouse = Mouse.current;
            var holdBlocked = mouse != null && mouse.leftButton.wasPressedThisFrame || gamePad != null && gamePad.buttonSouth.wasPressedThisFrame;
            
            if (mouse != null && mouse.leftButton.wasPressedThisFrame || gamePad != null && gamePad.buttonSouth.wasPressedThisFrame)
            {
                MainButton.color = _pressColor;
            }

            if (mouse != null && mouse.leftButton.wasReleasedThisFrame || gamePad != null && gamePad.buttonSouth.wasReleasedThisFrame)
            {
                MainButton.color = _baseColor;
            }

            if ((mouse != null && mouse.leftButton.isPressed || gamePad != null && gamePad.buttonSouth.isPressed) && !holdBlocked)
            {
                MainButton.color = _holdColor;
            }
            
            if (mouse != null && mouse.rightButton.wasPressedThisFrame || gamePad != null && gamePad.buttonEast.wasPressedThisFrame)
            {
                SecondaryButton.color = _pressColor;
            }

            if (mouse != null && mouse.rightButton.wasReleasedThisFrame || gamePad != null && gamePad.buttonEast.wasReleasedThisFrame)
            {
                SecondaryButton.color = _baseColor;
            }

            if ((mouse != null && mouse.rightButton.isPressed || gamePad != null && gamePad.buttonEast.isPressed) && !holdBlocked)
            {
                SecondaryButton.color = _holdColor;
            }
        }
    }
}
