using System;
using UnityEngine.UI;

namespace Code.ControlScript
{
    public class UIImageControlScript : AbstractControlScript
    {
        public enum UIIMageType
        {
            Alpha,
        }

        public Image UIImage;
        public UIIMageType Type = UIIMageType.Alpha;

        private void Update()
        {
            switch(Type)
            {
                case UIIMageType.Alpha:
                    var c = UIImage.color;
                    c.a = 1-GetCurrentFloatFromKnob();
                    UIImage.color = c;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}