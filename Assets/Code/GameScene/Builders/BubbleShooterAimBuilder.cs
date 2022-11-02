using System;
using Code.GameScene.Common;
using Code.GameScene.Models;
using Code.GameScene.Views;
using UnityEngine;

namespace Code.GameScene.Builders
{
    public class BubbleShooterAimBuilder
    {
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly Map _map;
        private readonly UserInput _userInput;
        private readonly Action<IUpdatable> _addToUpdate;

        public BubbleShooterAimBuilder(
            ViewModelDispatcher viewModelDispatcher, 
            Map map, 
            UserInput userInput, 
            Action<IUpdatable> addToUpdate)
        {
            _viewModelDispatcher = viewModelDispatcher;
            _map = map;
            _userInput = userInput;
            _addToUpdate = addToUpdate;
        }

        public BubbleShooterAim Build(Vector2 position, int maxIntersections)
        {
            var bubbleShooterAim = new BubbleShooterAim(position, maxIntersections, _map, _userInput);
            
            _viewModelDispatcher.ConstructViewModel(bubbleShooterAim);
            _addToUpdate(bubbleShooterAim);
            
            return bubbleShooterAim;
        }
    }
}