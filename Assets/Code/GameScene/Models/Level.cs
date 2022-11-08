using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.GameScene.Movers;
using Code.Services;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class Level
    {
        public bool LevelFinished => _levelQueue.Count == 0;
        
        public float BubbleMoveSpeed { get; set; } = 20;
        public int LevelHeight { get; set; } = 20;
        public int StageRowCount { get; set; } = 8;
        
        public BubbleExploder BubbleExploder { get; }
        
        private readonly Map _map;
        private readonly LevelService _levelService;
        private readonly ViewModelService _viewModelService;
        private readonly BubbleMoverService _bubbleMoverService;

        private Queue<BubbleType[]> _levelQueue;

        public Level(Map map, GameServices gameServices)
        {
            _map = map;
            
            _levelService = gameServices.LevelService;
            _viewModelService = gameServices.ViewModelService;
            _bubbleMoverService = gameServices.BubbleMoverService;

            BubbleExploder = new BubbleExploder(_map, _bubbleMoverService);
            BubbleExploder.ExplosionFinished += BubbleExploderOnExplosionFinished;
        }
        public void Construct(LevelKey levelKey)
        {
            _levelQueue = levelKey == LevelKey.Random
                ? _levelService.GetRandomLevelQueue(_map.GridSize.x, LevelHeight)
                : _levelService.GetLevelQueue(levelKey);

            NextStage();
        }
        
        private void BubbleExploderOnExplosionFinished(object sender, bool hasExplosion)
        {
            if (hasExplosion && !_map.EmployedPositions.Any())
            {
                NextStage();
            }
        }

        private void NextStage()
        {
            if (_levelQueue.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < StageRowCount; i++)
            {
                NextRow();
            }
        }

        private void NextRow()
        {
            if (_levelQueue.TryDequeue(out BubbleType[] bubbleTypes))
            {
                ShiftDown();
                AttacheRow(bubbleTypes);
            }
        }

        private void AttacheRow(BubbleType[] bubbleTypes)
        {
            for (int x = 0; x < bubbleTypes.Length; x++)
            {
                if (bubbleTypes[x] == BubbleType.None)
                {
                    continue;
                }
                
                var gridPosition = new Vector2Int(x, 0);

                var bubble = _viewModelService.ConstructViewModel<Bubble, Bubble.Settings>(
                    new Bubble.Settings
                    {
                        BubbleType = bubbleTypes[x]
                    }
                );
                
                _map.Attach(gridPosition, bubble);
            }
        }

        private void ShiftDown()
        {
            for (int y = _map.GridSize.y - 1; y >= 0; y--)
            {
                for (int x = 0; x < _map.GridSize.x; x++)
                {
                    var gridPosition = new Vector2Int(x, y);

                    Bubble bubble = _map.Detach(gridPosition);

                    if (bubble != null)
                    {
                        var targetGridPosition = new Vector2Int(gridPosition.x, gridPosition.y + 1);
                        
                        _map.Attach(targetGridPosition, bubble, false);

                        Vector2 targetPosition = _map.GetWorldPositionByGridPosition(targetGridPosition);

                        var bubbleMover = new BubbleMover(bubble, BubbleMoveSpeed, targetPosition);
                        _bubbleMoverService.Register(bubble, bubbleMover);
                    }
                }
            }
        }
    }
}