using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Code.Models
{
    public class GameOver : IModel
    {
        public class MinHeightLine : IModel
        {
            public event EventHandler<float> PositionChanged;

            public float Position
            {
                get => _position;
                set
                {
                    _position = value;
                    PositionChanged?.Invoke(this, _position);
                }
            }

            private float _position;
        }
        
        public event EventHandler<bool> ActiveGameOverChanged;

        public float MinBubblePositionToActive
        {
            get => _minHeightLine.Position;
            set => _minHeightLine.Position = value;
        }

        private readonly MinHeightLine _minHeightLine;
        private readonly Map _map;
        private readonly Action<bool> _stopUpdate;

        public GameOver(MinHeightLine minHeightLine, Map map, BubbleExploder bubbleExploder, Action<bool> stopUpdate)
        {
            _minHeightLine = minHeightLine;
            _map = map;
            _stopUpdate = stopUpdate;

            bubbleExploder.ExplosionFinished += BubbleExploderOnExplosionFinished;
        }

        private void BubbleExploderOnExplosionFinished(object sender, bool hasExplosion)
        {
            if (!hasExplosion)
            {
                if (_map.EmployedPositions.Any(gridPosition => gridPosition.y < MinBubblePositionToActive))
                {
                    SetActive(true);
                }
            }
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void SetActive(bool active)
        {
            _stopUpdate(active);
            ActiveGameOverChanged?.Invoke(this, active);
        }
    }
}