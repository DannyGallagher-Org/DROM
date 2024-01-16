using UnityEngine;

namespace Code.CloudBits
{
    public class CloudBitGrower : CloudBit
    {
        [SerializeField] private float MaxSize = 60f;
        [SerializeField] private float GrowthSpeed = 1f;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Grow();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopGrowing();
            }

            if (Input.GetMouseButtonDown(0))
            {
                StartGrowing();
            }
        }

        private void StartGrowing()
        {
            foreach (var system in _particleSystems)
            {
                var main = system.main;
                main.startLifetime = 1f;
                system.Clear();
                system.Stop();
                system.Play();
            }
        }

        private void StopGrowing()
        {
            foreach (var system in _particleSystems)
            {
                var main = system.main;
                main.startLifetime = 30f;
            }
        }

        public void Grow()
        {
            foreach (var system in _particleSystems)
            {
                var main = system.main;
                if (main.startSize.constant < MaxSize)
                {
                    main.startSize = new ParticleSystem.MinMaxCurve(main.startSize.constant + Time.deltaTime * GrowthSpeed);
                }
            }
        }
    }
}
