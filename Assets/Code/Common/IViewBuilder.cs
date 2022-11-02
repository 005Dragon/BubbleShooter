using System;

namespace Code.Common
{
    public interface IViewBuilder
    {
        Type ModelType { get; }
        IView Build(IModel model);
    }
}