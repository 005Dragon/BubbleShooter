using Code.Common;
using UnityEditor;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(SceneData), fileName = nameof(SceneData))]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private SceneKey _sceneKey;
        [SerializeField] private SceneAsset _scene;

        public SceneKey SceneKey => _sceneKey;
        public SceneAsset Scene => _scene;
    }
}