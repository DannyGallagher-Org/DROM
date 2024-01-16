using UnityEngine;

namespace Code.CloudBits
{
    public class CloudBit : MonoBehaviour
    {
        protected ParticleSystem[] _particleSystems;

        private void Awake()
        {
            _particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        }
    }
}
