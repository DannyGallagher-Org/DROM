using RSG;
using UnityEngine;

namespace Code.Util
{
    public static class PerlinArray
    {
        public static IPromise<float[,]> GetPerlinArray(int width, int height, int seed)
        {
            var promise = new Promise<float[,]>();
            GetPerlinArray(width, height, seed, promise);
            return promise;
        }
        
        private static void GetPerlinArray(int width, int height, int seed, IPendingPromise<float[,]> promise)
        {
            var array = new float[width, height];
            
            for (var ix = 0; ix < width; ix++)
            {
                var time = (float)seed;
                for (var iy = 0; iy < height; iy++)
                {
                    var perlinNoise = Mathf.PerlinNoise(time * ix + seed, time * iy + seed);
                    array[ix, iy] = perlinNoise;
                    time += 0.33f;
                }
            }
            promise.Resolve(array);
        }
    }
}