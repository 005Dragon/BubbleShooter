using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Code.Models
{
    public class GameOver : IModel
    {
        public event EventHandler<bool> ActiveGameOverChanged;

        public float MinBubblePositionToActive { get; set; }

        private readonly Map _map;
        private readonly Action<bool> _stopUpdate;

        public GameOver(Map map, Action<bool> stopUpdate)
        {
            _map = map;
            _stopUpdate = stopUpdate;

            _map.FullnessChanged += MapOnFullnessChanged;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void MapOnFullnessChanged(object sender, EventArgs e)
        {
            if (_map.EmployedPositions.Any(gridPosition => gridPosition.y < MinBubblePositionToActive))
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