using System.Collections.Generic;
using System.Linq;
using Code.Common;
using Code.Models;
using Code.Services;
using Code.Views;
using UnityEngine;

namespace Code.Builders
{
    public class BubbleViewBuilder : ViewBuilderBase<Bubble, BubbleView>
    {
        private readonly BubbleService _bubbleService;
        private readonly Dictionary<BubbleType, List<BubbleView>> _bubbleTypeToBubbleViewsIndex = new();

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

            _bubbleTypeToBubbleViewsIndex.TryAdd(model.BubbleType, new List<BubbleView>());
            _bubbleTypeToBubbleViewsIndex[model.BubbleType].Add(bubbleView);

            return bubbleView;
        }

        protected override bool TryGetViewFromPool(Bubble model, out BubbleView view)
        {
            if (_bubbleTypeToBubbleViewsIndex.TryGetValue(model.BubbleType, out List<BubbleView> bubbleViews))
            {
                view = bubbleViews.FirstOrDefault(x => !x.gameObject.activeInHierarchy);

                if (view != null)
                {
                    view.Model = model;
                    view.gameObject.SetActive(true);
                    return true;
                }
            }

            view = default;
            return false;
        }
    }
}