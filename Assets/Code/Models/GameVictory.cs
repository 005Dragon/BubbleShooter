using System;
using System.Linq;

namespace Code.Models
{
    public class GameVictory : IModel
    {
        public event EventHandler<bool> ActiveGameOverChanged;

        private readonly Map _map;
        private readonly Level _level;
        private readonly Action<bool> _stopUpdate;

        public GameVictory(Map map, Level level, Action<bool> stopUpdate)
        {
            _map = map;
            _level = level;
            _stopUpdate = stopUpdate;
            
            _map.FullnessChanged += MapOnFullnessChanged;
        }

        private void MapOnFullnessChanged(object sender, EventArgs eventArgs)
        {
            if (_level.LevelFinished && !_map.EmployedPositions.Any())
            {
                SetActive(true);
            }
        }

        private void SetActive(bool active)
        {
            _stopUpdate(active);
            ActiveGameOverChanged?.Invoke(this, active);
        }
    }
}