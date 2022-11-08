using Code.MainMenuScene.Models;
using Code.Storage;
using UnityEngine;

namespace Code.MainMenuScene
{
    public class BootstrapMainMenuBehavior : MonoBehaviour
    {
        [SerializeField] private StorageSet _storageSet;
        
        private void Awake()
        {
            var services = new MainMenuServices(_storageSet);
            
            CreateMainMenuGameState(services);
        }

        private void CreateMainMenuGameState(MainMenuServices services)
        {
            var mainMenuGameState = new MainMenuGameState(
                new MainMenuGameState.Settings
                {
                    GameConfigService = services.GameConfigService,
                    SceneService = services.SceneService
                }
            );
            
            services.ViewModelService.ConstructViewModel(mainMenuGameState);
        }
    }
}