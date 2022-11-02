using System;
using Code.GameScene.Models;
using Code.GameScene.Views;

namespace Code.GameScene.Builders
{
    public interface IViewBuilder
    {
        Type ModelType { get; }
        IView Build(IModel model);
    }
}