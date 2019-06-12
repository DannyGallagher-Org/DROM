using RSG;
using UnityEngine;

namespace Code
{
    public class DromCloud : MonoBehaviour
    {
        public float[,] PerlinArray;
        
        public int Width;
        public int Height;

        private void Awake()
        {
            Width = Random.Range(0, 7);
            Height = Random.Range(0, 7);
            Util.PerlinArray.GetPerlinArray(Width, Height, 59)
                .Then(pa => PerlinArray = pa)
                .Then(pa => BuildCloud(pa));
        }

        private IPromise<object> BuildCloud(float[,] pa)
        {
            
            
            return Promise<object>.Resolved(null);
        }
    }
}
