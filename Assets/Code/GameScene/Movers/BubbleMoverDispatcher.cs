using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.GameScene.Models;

namespace Code.GameScene.Movers
{
    public class BubbleMoverDispatcher : IUpdatable
    {
        private readonly Dictionary<Bubble, Queue<IUpdatable>> _bubbleToBubbleMoverIndexes = new();

        public void Register(Bubble bubble, IUpdatable bubbleMover)
        {
            if (!_bubbleToBubbleMoverIndexes.TryGetValue(bubble, out Queue<IUpdatable> movers))
            {
                movers = new Queue<IUpdatable>();
                _bubbleToBubbleMoverIndexes[bubble] = movers;
            }
            
            movers.Enqueue(bubbleMover);
        }

        public bool Update()
        {
            List<Bubble> toDelete = new();

            List<KeyValuePair<Bubble, Queue<IUpdatable>>> bubbleToBubbleMoverIndexesCache = 
                _bubbleToBubbleMoverIndexes.ToList();

            foreach (KeyValuePair<Bubble,Queue<IUpdatable>> bubbleToBubbleMoverIndex in bubbleToBubbleMoverIndexesCache)
            {
                if (bubbleToBubbleMoverIndex.Value.TryPeek(out IUpdatable updatableObject))
                {
                    if (!updatableObject.Update())
                    {
                        bubbleToBubbleMoverIndex.Value.Dequeue();
                    }
                }
                else
                {
                    toDelete.Add(bubbleToBubbleMoverIndex.Key);
                }
            }

            foreach (Bubble bubble in toDelete)
            {
                _bubbleToBubbleMoverIndexes.Remove(bubble);
            }

            return true;
        }
    }
}