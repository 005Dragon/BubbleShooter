using System;
using Code.Common;
using Code.Infrastructure.Models;
using Code.Services;

namespace Code.GameScene.Models
{
    public class PauseGameState : ModelBase<PauseGameState.Settings>
    {
        public class Settings : ModelSettingsBase
        {
            public GameUserInputService UserInputService { get; set; }
            public IUpdateService UpdateService { get; set; }
            public SceneService SceneService { get; set; }
        }
        
        public event EventHandler<bool> ActivePauseChanged;

        private bool _active;

        public PauseGameState(Settings settings) : base(settings)
        {
            _settings.UserInputService.Pause += UserInputServiceOnPause;
        }

        public void ChangeActivePause()
        {
            _active = !_active;

            if (_active)
            {
                _settings.UserInputService.Disable();
            }
            else
            {
                _settings.UserInputService.Enable();
            }
            
            _settings.UpdateService.StopUpdate = _active;
            ActivePauseChanged?.Invoke(this, _active);
        }
        
        public void ReloadGameScene() => _settings.SceneService.LoadScene(SceneKey.Game);

        public void LoadMainMenuScene() => _settings.SceneService.LoadScene(SceneKey.MainMenu);

        private void UserInputServiceOnPause(object sender, EventArgs eventArgs)
        {
            ChangeActivePause();
        }
    }
}