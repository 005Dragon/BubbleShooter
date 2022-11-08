using System;
using System.Linq;
using Code.Common;
using Code.Infrastructure.Models;
using Code.Services;
using UnityEngine.SceneManagement;

namespace Code.GameScene.Models
{
    public class GameVictoryGameState : ModelBase<GameVictoryGameState.Settings>
    {
        public class Settings : ModelSettingsBase
        {
            public Map Map { get; set; }
            public Level Level { get; set; }
            public GameUserInputService UserInputService { get; set; }
            public SceneService SceneService { get; set; }
        }

        public event EventHandler<bool> ActiveGameOverChanged;
        
        public GameVictoryGameState(Settings settings) : base(settings)
        {
            settings.Map.FullnessChanged += MapOnFullnessChanged;
        }

        public void LoadMainMenuScene() => _settings.SceneService.LoadScene(SceneKey.MainMenu);

        private void MapOnFullnessChanged(object sender, EventArgs eventArgs)
        {
            if (_settings.Level.LevelFinished && !_settings.Map.EmployedPositions.Any())
            {
                SetActive(true);
            }
        }

        private void SetActive(bool active)
        {
            if (active)
            {
                _settings.UserInputService.Disable();
            }
            else
            {
                _settings.UserInputService.Enable();
            }
            
            ActiveGameOverChanged?.Invoke(this, active);
        }
    }
}