using Code.GameScene.Models;

namespace Code.GameScene.Views
{
    public interface IView
    {
        IModel Model { get; set; }

        void Initialize();
    }
}