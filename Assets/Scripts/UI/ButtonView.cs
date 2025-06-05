using Infrastructure;
using UnityEngine;

namespace UI
{
    public class ButtonView<T> : MonoBehaviour where T : ViewData
    {
        public T Data;

        protected virtual void OnContextUpdate(T data)
        {
            Data = data;
        }
    }
}