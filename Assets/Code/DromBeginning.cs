using UnityEngine;

namespace Code
{
    public class DromBeginning : MonoBehaviour
    {
        public GameObject[] BeginClouds;

        public void DoBegin()
        {
            foreach (var cloud in BeginClouds)
            {
                cloud.SetActive(false);
            }
        }
    }
}
