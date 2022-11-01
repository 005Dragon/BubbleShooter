using System;
using Code.Models;
using Code.Views;

namespace Code.Builders
{
    public class GameOverBuilder
    {
        private readonly Map _map;
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly Action<bool> _stopUpdate;

        public GameOverBuilder(Map map, ViewModelDispatcher viewModelDispatcher, Action<bool> stopUpdate)
        {
            _map = map;
            _viewModelDispatcher = viewModelDispatcher;
            _stopUpdate = stopUpdate;
        }

        public GameOver Build()
        {
            var gameOver = new GameOver(_map, _stopUpdate);
            
            _viewModelDispatcher.ConstructViewModel(gameOver);

            return gameOver;
        }
    }
}