using Code.CloudBits;
using UnityEngine;

namespace Code.Controllers
{
    public class ControllerGrower : AbstractController
    {
        [SerializeField] private GameObject GrowingCloudPrefab;
        private CloudBitGrower _currentCloud;

        public override void GetMainButtonDown()
        {
            var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenToWorldPoint.z = 50f;
            Debug.Log("screenToWorldPoint = " + screenToWorldPoint);
            if (_currentCloud == null)
            {
                var cloudSpawned = Instantiate(GrowingCloudPrefab, screenToWorldPoint, Quaternion.identity);
                _currentCloud = cloudSpawned.GetComponent<CloudBitGrower>();
                _currentCloud.StartGrowing();
            }
        }

        public override void GetMainButton()
        {
            var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenToWorldPoint.z = 50f;
            Debug.Log("screenToWorldPoint = " + screenToWorldPoint);
            _currentCloud.Grow();
        }

        public override void GetMainButtonUp()
        {
            _currentCloud.StopGrowing();
            _currentCloud = null;
        }

        public override void GetSecondaryButtonDown()
        {
            
        }

        public override void GetSecondaryButtonUp()
        {
            
        }

        public override void GetSecondaryButton()
        {
            
        }
    }
}