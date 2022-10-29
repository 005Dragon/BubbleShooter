using UnityEngine;

namespace Code
{
    public class BubbleViewBuilder : ViewBuilderBase<Bubble, BubbleView>
    {
        private readonly BubbleService _bubbleService;

        public BubbleViewBuilder(Transform root, BubbleService bubbleService) : base(root)
        {
            _bubbleService = bubbleService;
        }

        protected override BubbleView CreateView(GameObject gameObject, Bubble model)
        {
            var bubbleView = gameObject.AddComponent<BubbleView>();
            
            bubbleView.Model = model;
            bubbleView.Sprite = _bubbleService.GetSprite(model.BubbleType);
            bubbleView.Color = _bubbleService.GetColor(model.BubbleType);

            return bubbleView;
        }
    }
}