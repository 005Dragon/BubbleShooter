using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.Storage;
using UnityEditor;

namespace Code.Services
{
    public class SceneService
    {
        private readonly Dictionary<SceneKey, SceneAsset> _sceneKeyToSceneIndex;

        public SceneService(SceneStorage sceneStorage)
        {
            _sceneKeyToSceneIndex = sceneStorage.Scenes.ToDictionary(x => x.SceneKey, x => x.Scene);
        }

        public string GetSceneName(SceneKey sceneKey) => _sceneKeyToSceneIndex[sceneKey].name;
    }
}