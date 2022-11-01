using System;
using Code.Models;
using Code.Views;

namespace Code.Builders
{
    public class GameOverBuilder
    {
        private readonly Map _map;
        private readonly BubbleExploder _bubbleExploder;
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly Action<bool> _stopUpdate;

        public GameOverBuilder(
            Map map, 
            BubbleExploder bubbleExploder, 
            ViewModelDispatcher viewModelDispatcher, 
            Action<bool> stopUpdate)
        {
            _map = map;
            _bubbleExploder = bubbleExploder;
            _viewModelDispatcher = viewModelDispatcher;
            _stopUpdate = stopUpdate;
        }

        public GameOver Build()
        {
            var gameOverMinHeightLine = new GameOver.MinHeightLine();
            var gameOver = new GameOver(gameOverMinHeightLine, _map, _bubbleExploder, _stopUpdate);
            
            _viewModelDispatcher.ConstructViewModel(gameOverMinHeightLine);
            _viewModelDispatcher.ConstructViewModel(gameOver);

            return gameOver;
        }
    }
}