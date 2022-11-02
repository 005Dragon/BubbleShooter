using System;
using Code.Common;
using UnityEngine;

namespace Code.GameScene.Models.Builders
{
    public class BubbleShooterAimBuilder
    {
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly Map _map;
        private readonly GameUserInput _gameUserInput;
        private readonly Action<IUpdatable> _addToUpdate;

        public BubbleShooterAimBuilder(
            ViewModelDispatcher viewModelDispatcher, 
            Map map, 
            GameUserInput gameUserInput, 
            Action<IUpdatable> addToUpdate)
        {
            _viewModelDispatcher = viewModelDispatcher;
            _map = map;
            _gameUserInput = gameUserInput;
            _addToUpdate = addToUpdate;
        }

        public BubbleShooterAim Build(Vector2 position, int maxIntersections)
        {
            var bubbleShooterAim = new BubbleShooterAim(position, maxIntersections, _map, _gameUserInput);
            
            _viewModelDispatcher.ConstructViewModel(bubbleShooterAim);
            _addToUpdate(bubbleShooterAim);
            
            return bubbleShooterAim;
        }
    }
}