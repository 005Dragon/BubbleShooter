using System;
using Code.Models;
using Code.Views;

namespace Code.Builders
{
    public interface IViewBuilder
    {
        Type ModelType { get; }
        IView Build(IModel model);
    }
}