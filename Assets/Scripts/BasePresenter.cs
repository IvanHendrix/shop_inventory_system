using Infrastructure;

public abstract class BasePresenter
{
    public abstract void Start();

    public abstract void Finish();
}
    
public abstract class BasePresenter<T> : BasePresenter where T : BaseView
{
    protected abstract string ViewId { get; }
        
    protected T View;

    public virtual void SetVisibleView(bool isVisible)
    {
        View.gameObject.SetActive(isVisible);
    }
        
    public override void Start()
    {
        View = ViewManager.Instance.SetView(ViewId) as T;
        OnStart();
    }
        
    public override void Finish()
    {
        ViewManager.Instance.RemoveView(View);
        OnFinish();
    }
        
    protected virtual void OnStart()
    {
    }

    protected virtual void OnFinish()
    {
    }
}