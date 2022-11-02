using System.Collections.Generic;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(SceneStorage), fileName = nameof(SceneStorage))]
    public class SceneStorage : ScriptableObject
    {
        [SerializeField] private List<SceneData> _scenes;

        public IReadOnlyList<SceneData> Scenes => _scenes;
    }
}