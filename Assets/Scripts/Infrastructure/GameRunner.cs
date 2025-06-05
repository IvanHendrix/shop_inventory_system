using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
    
        private void Awake()
        {
            _game = new Game(this);
        
            DontDestroyOnLoad(this);
        }
    }
}