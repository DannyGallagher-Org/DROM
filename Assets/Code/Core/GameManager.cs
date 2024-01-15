using UnityEngine;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        [Header("Scene References")] 
        [SerializeField] private ShapeCheckCamera ShapeCam;
        
        [Header("Levels")]
        [SerializeField] private Level[] Levels;

        
        private float _currentTargetRatio;
        
        public void StartGame()
        {
            
        }
    }
}