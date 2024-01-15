namespace hStates
{
    public sealed class StateMachine
    {
        #region delegates
        public delegate void StateChanged(IState state);
        public event StateChanged OnStateChanged;
        public event StateChanged OnStatePushed;
        public event StateChanged OnStatePopped;
        #endregion
	
        #region fields
        private IState _storedStateFromPush;
        private ServiceLocator _services;
        #endregion
	
        #region properties
        public IState CurrentState { get; set; }
        #endregion
        
        #region interface

        public void SetServices(ServiceLocator services) => _services = services;
        #endregion
        
        #region methods
        public void Update() => (CurrentState as IUpdateable)?.Update();

        public void LateUpdate()
        {
            if (CurrentState == default(IState)) 
                return;
            
            (CurrentState as IUpdateable)?.LateUpdate();
			
            if (!CurrentState.CanTransition()) 
                return;
            
            var nextState = CurrentState.NextState();
            ChangeState(nextState);
        }

        public void PushState(IState pushState)
        {
            _storedStateFromPush = CurrentState;

            CurrentState = pushState;
            OnStatePushed?.Invoke(pushState);
            
            var pushEnterState = pushState as IEnterState;
            pushEnterState?.OnEnter();
        }

        public void PopState()
        {
            CurrentState?.OnExit();

            CurrentState = _storedStateFromPush;
            _storedStateFromPush = null;
            OnStatePopped?.Invoke(CurrentState);
        }

        public void ChangeState(IState nextState)
        {
            CurrentState?.OnExit();

            CurrentState = nextState;
            OnStateChanged?.Invoke(CurrentState);

            var injectable = nextState as IInjectable;
            if(_services != null)
                injectable?.Inject(_services);

            var enterState = nextState as IEnterState;
            enterState?.OnEnter();
        }
	
        public void OnApplicationPause(bool paused)
        {
            if(!paused)
                return;

            (CurrentState as IPausable)?.ApplicationHasGoneIntoBackground();
        }
        #endregion
    }
}