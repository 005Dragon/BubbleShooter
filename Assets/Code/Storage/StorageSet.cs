using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(StorageSet), fileName = nameof(StorageSet))]
    public class StorageSet : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> _storages;

        public TStorage GetStorage<TStorage>()
        {
            foreach (ScriptableObject storages in _storages)
            {
                if (storages is TStorage storage)
                {
                    return storage;
                }
            }

            throw new Exception("Storage set not exists " + nameof(TStorage) + " type.");
        }
    }
}