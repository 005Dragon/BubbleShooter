using UnityEngine.SceneManagement;

namespace Code.Common.Models
{
    public class StartGameNavigationPoint : IModel
    {
        private readonly string _gameSceneName;

        public StartGameNavigationPoint(string gameSceneName)
        {
            _gameSceneName = gameSceneName;
        }

        public void Active()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
    }
}