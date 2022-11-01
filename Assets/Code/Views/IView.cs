using Code.Models;

namespace Code.Views
{
    public interface IView
    {
        IModel Model { get; set; }

        void Initialize();
    }
}