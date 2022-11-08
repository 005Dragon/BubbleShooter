using Code.Common;
using Code.Infrastructure.Models;
using Code.Services;

namespace Code.MainMenuScene.Models
{
    public class MainMenuGameState : ModelBase<MainMenuGameState.Settings>
    {
        public class Settings : ModelSettingsBase
        {
            public GameConfigService GameConfigService { get; set; }
            public SceneService SceneService { get; set; }
        }

        public MainMenuGameState(Settings settings) : base(settings)
        {
        }

        public void LoadGameScene() => _settings.SceneService.LoadScene(SceneKey.Game);

        public LevelKey ChangeLevel()
        {
            _settings.GameConfigService.Level = _settings.GameConfigService.Level == LevelKey.TestLevel
                ? LevelKey.Random
                : LevelKey.TestLevel;

            return _settings.GameConfigService.Level;
        }
    }
}