using Code.Levels;
using UnityEngine;

namespace Code
{
    public class DromManager : MonoBehaviour
    {
        [SerializeField] private DromLevel[] Levels;
        private void Awake()
        {
            foreach (var dromLevel in Levels)
            {
                dromLevel.Init();
            }
        }
    }
}
