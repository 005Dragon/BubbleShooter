using UnityEngine;

namespace Code
{
    public class BubbleBuilder
    {
        private readonly ViewModelDispatcher _viewModelDispatcher;

        public BubbleBuilder(ViewModelDispatcher viewModelDispatcher)
        {
            _viewModelDispatcher = viewModelDispatcher;
        }

        public Bubble Build(BubbleType bubbleType, Vector2 position)
        {
            var bubble = new Bubble(bubbleType, position);
            
            _viewModelDispatcher.ConstructViewModel(bubble);

            return bubble;
        }
    }
}