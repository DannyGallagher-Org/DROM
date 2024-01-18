using UnityEngine;

namespace Code.Controllers
{
    public abstract class AbstractController : MonoBehaviour
    {
        private void Update()
        {
            
        }

        public abstract void GetMainButtonDown();
        public abstract void GetMainButtonUp();
        public abstract void GetMainButton();
        public abstract void GetSecondaryButtonDown();
        public abstract void GetSecondaryButtonUp();
        public abstract void GetSecondaryButton();
    }
}