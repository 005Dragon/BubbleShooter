using UnityEngine.SceneManagement;

namespace Code.Common.Models
{
    public class MainMenuNavigationPoint : IModel
    {
        private readonly string _mainMenuSceneName;

        public MainMenuNavigationPoint(string mainMenuSceneName)
        {
            _mainMenuSceneName = mainMenuSceneName;
        }

        public void Active()
        {
            SceneManager.LoadScene(_mainMenuSceneName);
        }
    }
}