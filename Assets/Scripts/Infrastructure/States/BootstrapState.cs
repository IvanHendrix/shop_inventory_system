using Infrastructure.Services;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
    
        private readonly GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private AllServices _services;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, EnterLoadLevel);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
        
        private void RegisterServices()
        {
        }
    }
}