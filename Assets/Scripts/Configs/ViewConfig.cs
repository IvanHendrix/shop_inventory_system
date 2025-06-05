using System;
using Infrastructure;

namespace Configs
{
    [Serializable]
    public class ViewConfig
    {
        public string Id;
        public BaseView View;

        public ViewConfig(BaseView baseView)
        {
            View = baseView;
            Id = baseView.name;
        }
    }
}