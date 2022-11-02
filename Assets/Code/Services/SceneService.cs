using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.Storage;

namespace Code.Services
{
    public class SceneService
    {
        private readonly Dictionary<SceneKey, string> _sceneKeyToSceneNameIndex;

        public SceneService(SceneStorage sceneStorage)
        {
            _sceneKeyToSceneNameIndex = sceneStorage.Scenes.ToDictionary(x => x.SceneKey, x => x.SceneName);
        }

        public string GetSceneName(SceneKey sceneKey) => _sceneKeyToSceneNameIndex[sceneKey];
    }
}