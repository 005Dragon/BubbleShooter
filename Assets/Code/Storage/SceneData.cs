using Code.Common;
using UnityEditor;
using UnityEngine;

namespace Code.Storage
{
    [CreateAssetMenu(menuName = "Game/" + nameof(SceneData), fileName = nameof(SceneData))]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private SceneKey _sceneKey;
        [SerializeField] private string _sceneName;

        public SceneKey SceneKey => _sceneKey;
        public string SceneName => _sceneName;
    }
}