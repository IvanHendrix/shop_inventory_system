using UnityEngine;

namespace Infrastructure
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;
        
        public static ViewManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); 
                return;
            }

            Instance = this;
        }

        public void RegisterView(Transform view)
        {
            view.SetParent(_canvas);
        }
    }
}