using Infrastructure.Services;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string NextScene = "Main";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.Load(NextScene, OnLoaded);
        }

        public void Exit()
        {
        }
        
        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}