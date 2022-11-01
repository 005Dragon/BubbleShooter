using System.Collections.Generic;
using Code.Common;
using UnityEngine;

namespace Code
{
    public class UpdateBehavior : MonoBehaviour
    {
        private readonly List<IUpdatable> _updatableObjects = new();

        public void AddToUpdate(IUpdatable updatableObject) => _updatableObjects.Add(updatableObject);
        public void RemoveFromUpdate(IUpdatable updatableObject) => _updatableObjects.Remove(updatableObject);

        private void Update()
        {
            _updatableObjects.RemoveAll(x => !x.Update());
        }
    }
}