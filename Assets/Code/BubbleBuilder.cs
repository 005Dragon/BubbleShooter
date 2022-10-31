using UnityEngine;

namespace Code
{
    public class BubbleBuilder
    {
        private readonly ViewModelDispatcher _viewModelDispatcher;
        private readonly float _diameter;

        public BubbleBuilder(float diameter, ViewModelDispatcher viewModelDispatcher)
        {
            _diameter = diameter;
            _viewModelDispatcher = viewModelDispatcher;
        }

        public Bubble Build(BubbleType bubbleType, Vector2 position = default)
        {
            var bubble = new Bubble(bubbleType, position, _diameter);

            _viewModelDispatcher.ConstructViewModel(bubble);

            return bubble;
        }

        public void Destroy(Bubble bubble)
        {
        }
    }
}