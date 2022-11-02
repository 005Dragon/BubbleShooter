using Code.Common;
using Code.Common.Models;
using Code.Common.Models.Builders;
using Code.Common.Views;
using Code.Services;
using Code.Storage;
using UnityEngine;

namespace Code.MainMenuScene
{
    public class BootstrapMainMenuBehavior : MonoBehaviour
    {
        [SerializeField] private GameConfigData _gameConfig;
        
        [Header("Storages")]
        [SerializeField] private SceneStorage _sceneStorage;
        
        private void Awake()
        {
            var viewModelDispatcher = new ViewModelDispatcher(GetViewBuilders());

            var sceneService = new SceneService(_sceneStorage);

            new StartGameNavigationPointBuilder(sceneService, viewModelDispatcher).Build();
            new GameLevelChangerBuilder(_gameConfig, viewModelDispatcher).Build();
        }

        private IViewBuilder[] GetViewBuilders()
        {
            Transform cachedTransform = transform;

            return new IViewBuilder[]
            {
                new ExistsViewBuilder<StartGameNavigationPoint, StartGameNavigationPointView>(
                    cachedTransform,
                    FindObjectOfType<StartGameNavigationPointView>
                ),

                new ExistsViewBuilder<GameLevelChanger, GameLevelChangerView>(
                    cachedTransform,
                    FindObjectOfType<GameLevelChangerView>
                )
            };
        }
    }
}