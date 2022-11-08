using System;
using System.Linq;
using Code.Common;
using Code.Infrastructure.Models;
using Code.Services;

namespace Code.GameScene.Models
{
    public class GameOverGameState : ModelBase<GameOverGameState.Settings>
    {
        public class Settings : ModelSettingsBase
        {
            public float MinBubblePositionToActive { get; set; }
            public Map Map { get; set; }
            public BubbleExploder BubbleExploder { get; set; }
            public GameUserInputService UserInputService { get; set; }
            public SceneService SceneService { get; set; }
        }
        
        public event EventHandler<bool> ActiveGameOverChanged;

        public float MinBubblePositionToActive => _settings.MinBubblePositionToActive;

        public GameOverGameState(Settings settings) : base(settings)
        {
            settings.BubbleExploder.ExplosionFinished += BubbleExploderOnExplosionFinished;
        }

        public void ReloadGameScene() => _settings.SceneService.LoadScene(SceneKey.Game);

        public void LoadMainMenuScene() => _settings.SceneService.LoadScene(SceneKey.MainMenu);

        private void BubbleExploderOnExplosionFinished(object sender, bool hasExplosion)
        {
            if (!hasExplosion)
            {
                bool bubbleCrossMinPosition =
                    _settings.Map.EmployedPositions
                        .Any(gridPosition => gridPosition.y < _settings.MinBubblePositionToActive);
                
                if (bubbleCrossMinPosition)
                {
                    SetActive(true);
                }
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