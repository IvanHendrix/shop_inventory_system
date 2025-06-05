using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure
{
    public class BaseView : MonoBehaviour
    {
    }
    
    public class BaseView<T> : BaseView where T : IViewData
    {
        public T Data;

        public void SetContext(T context)
        {
            OnContextUpdate(Data);
        }

        protected virtual void OnContextUpdate(T context) { }
    }
}