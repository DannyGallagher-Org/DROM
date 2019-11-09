using System.Collections.Generic;
using Code.ControlScript;
using UnityEngine;

namespace Code.Levels
{
    public abstract class AbstractLevel : MonoBehaviour
    {
        private readonly List<AbstractControlScript> _controllers = new List<AbstractControlScript>();
        
        public void Init()
        {
            foreach (var controlScript in GetComponentsInChildren<AbstractControlScript>())
            {
                _controllers.Add(controlScript);
                controlScript.enabled = false;
                SwitchState(DromLevelState.Sleeping);
            }
        }

        protected abstract void SwitchState(DromLevelState state);
    }
}