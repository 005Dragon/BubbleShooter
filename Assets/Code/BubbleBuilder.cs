using UnityEngine;

namespace Code
{
    public class BubbleBuilder
    {
        public float Diameter { get; set; } = 1.0f;
        
        private readonly ViewModelDispatcher _viewModelDispatcher;

        public BubbleBuilder(ViewModelDispatcher viewModelDispatcher)
        {
            _viewModelDispatcher = viewModelDispatcher;
        }

        public Bubble Build(BubbleType bubbleType, Vector2 position = default)
        {
            var bubble = new Bubble(bubbleType, position, Diameter);

            _viewModelDispatcher.ConstructViewModel(bubble);

            return bubble;
        }

        public void Destroy(Bubble bubble)
        {
        }
    }
}