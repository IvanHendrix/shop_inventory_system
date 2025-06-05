using Infrastructure;
using UnityEngine;

namespace UI
{
    public class ButtonView<T> : MonoBehaviour where T : ViewData
    {
        public T Data;
        
        public void SetContext(T context)
        {
            Data = context;

            OnContextUpdate(Data);
        }
        
        protected virtual void OnContextUpdate(T context) { }
    }
}