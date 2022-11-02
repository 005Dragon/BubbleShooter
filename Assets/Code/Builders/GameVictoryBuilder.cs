using System;
using Code.Models;
using Code.Views;

namespace Code.Builders
{
    public class GameVictoryBuilder
    {
        private readonly Map _map;
        private readonly Level _level;
        private readonly Action<bool> _stopUpdate;
        
        private readonly ViewModelDispatcher _viewModelDispatcher;

        public GameVictoryBuilder(
            Map map, 
            Level level, 
            Action<bool> stopUpdate, 
            ViewModelDispatcher viewModelDispatcher)
        {
            _map = map;
            _level = level;
            _stopUpdate = stopUpdate;
            _viewModelDispatcher = viewModelDispatcher;
        }

        public GameVictory Build()
        {
            var gameVictory = new GameVictory(_map, _level, _stopUpdate);
            
            _viewModelDispatcher.ConstructViewModel(gameVictory);

            return gameVictory;
        }
    }
}