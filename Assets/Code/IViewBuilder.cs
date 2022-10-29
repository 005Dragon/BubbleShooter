using System;

namespace Code
{
    public interface IViewBuilder
    {
        Type ModelType { get; }
        IView Build(IModel model);
    }
}