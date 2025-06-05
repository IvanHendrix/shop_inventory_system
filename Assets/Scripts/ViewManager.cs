using System;
using System.Collections.Generic;
using Infrastructure;
using ResourcesManager;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
        
    public static ViewManager Instance { get; private set; }
        
    private Dictionary<string, BaseView> _viewCache;

    public void Initialize()
    {
        _viewCache = new Dictionary<string, BaseView>();
    }
        
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }

    public BaseView SetView(string viewId)
    {
        if (_viewCache.ContainsKey(viewId))
        {
            return _viewCache[viewId];
        }

        var view = CreateView(viewId);
        if (view != null)
        {
            _viewCache.Add(viewId, view);
        }

        return view;
    }
    
    public void RemoveView(BaseView view)
    {
        List<string> viewsToDelete = new List<string>();
        foreach (var item in _viewCache)
        {
            if (item.Value == view)
            {
                viewsToDelete.Add(item.Key);
            }
        }

        foreach (var item in viewsToDelete)
        {
            _viewCache.Remove(item);
        }
        
        Destroy(view.gameObject);
    }
        
    private BaseView CreateView(string viewId)
    {
        if (!_viewCache.ContainsKey(viewId))
        {
            throw new Exception("Can't find view with such id " + viewId);
        }

        var view = Instantiate(ResourcesCache.GetViewById(viewId), _canvas, false);
        view.name = viewId;
        view.gameObject.SetActive(true);

        return view;
    }
}