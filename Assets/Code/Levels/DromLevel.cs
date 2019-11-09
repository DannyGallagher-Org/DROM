using System;
using System.Collections.Generic;
using Code.ControlScript;
using UnityEngine;

namespace Code.Levels
{
    public enum DromLevelState
    {
        Sleeping,
        Readying,
        Intro,
        Active,
        Complete,
        Exiting
    }
    public class DromLevel : AbstractLevel
    {
        public DromLevelState State => _state;
        private DromLevelState _state;
        

        public void SetState(DromLevelState state)
        {
            SwitchState(state);
        }

        protected override void SwitchState(DromLevelState state)
        {
            switch(state)
            {
                case DromLevelState.Sleeping:
                    
                    _state = DromLevelState.Readying;
                    break;
                case DromLevelState.Readying:
                    break;
                case DromLevelState.Intro:
                    break;
                case DromLevelState.Active:
                    break;
                case DromLevelState.Complete:
                    break;
                case DromLevelState.Exiting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
