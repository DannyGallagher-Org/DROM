using UnityEngine;

// ReSharper disable SuspiciousTypeConversion.Global

namespace hStates
{
    public class StateMachineMonoBehaviour : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new StateMachine();

        private void Update() => _stateMachine.Update();
        private void LateUpdate() => _stateMachine.LateUpdate();

        public void ChangeState(IState state) => _stateMachine.ChangeState(state);
        public void PushState(IState state) => _stateMachine.PushState(state);
        public void PopState() => _stateMachine.PopState();

        public void SetServices(ServiceLocator services) => _stateMachine.SetServices(services);

        private void OnApplicationPause(bool pauseStatus) => _stateMachine.OnApplicationPause(pauseStatus);
    }
}