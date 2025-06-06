using Infrastructure.Data;
using UnityEngine;

public class BaseView : MonoBehaviour
{
}
    
public class BaseView<T> : BaseView where T : IViewData
{
    public T Data;

    public void SetContext(T context)
    {
        Data = context;
        
        OnContextUpdate(Data);
    }

    protected virtual void OnContextUpdate(T context) { }
}