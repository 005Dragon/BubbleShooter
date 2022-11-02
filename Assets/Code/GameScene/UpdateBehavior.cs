using System.Collections.Generic;
using Code.GameScene.Common;
using UnityEngine;

namespace Code.GameScene
{
    public class UpdateBehavior : MonoBehaviour
    {
        public bool StopUpdate { get; set; }
        
        private readonly List<IUpdatable> _updatableObjects = new();
        
        public void AddToUpdate(IUpdatable updatableObject) => _updatableObjects.Add(updatableObject);
        public void RemoveFromUpdate(IUpdatable updatableObject) => _updatableObjects.Remove(updatableObject);

        private void Update()
        {
            if (!StopUpdate)
            {
                _updatableObjects.RemoveAll(x => !x.Update());
            }
        }
    }
}