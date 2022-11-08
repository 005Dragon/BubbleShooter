using Code.Infrastructure.Models;

namespace Code.Infrastructure.Views
{
    public interface IView
    {
        bool Initialized { get; }
        
        void Initialize(IModel model);
    }
}