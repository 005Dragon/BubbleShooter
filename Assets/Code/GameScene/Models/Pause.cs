using System;
using Code.Common;

namespace Code.GameScene.Models
{
    public class Pause : IModel
    {
        public event EventHandler<bool> ActivePauseChanged;

        private readonly Action<bool> _stopUpdate;

        private bool _active;

        public Pause(GameUserInput userInput, Action<bool> stopUpdate)
        {
            _stopUpdate = stopUpdate;
            
            userInput.Pause += UserInputOnPause;
        }

        public void ChangeActivePause()
        {
            _active = !_active;

            _stopUpdate(_active);
            ActivePauseChanged?.Invoke(this, _active);
        }

        private void UserInputOnPause(object sender, EventArgs e)
        {
            ChangeActivePause();
        }
    }
}