using Code.Common;

namespace Code.Services
{
    public interface IUpdateService
    {
        bool StopUpdate { get; set; }
        
        void AddToUpdate(IUpdatable updatableObject);

        void RemoveFromUpdate(IUpdatable updatableObject);
    }
}