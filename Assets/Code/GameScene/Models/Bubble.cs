using Code.Common;
using Code.Infrastructure.Models;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class Bubble : ModelBase<Bubble.Settings>
    {
        public class Settings : ModelSettingsBase
        {
            public BubbleType BubbleType { get; set; }

            public float Diameter
            {
                get => Scale.x;
                set => Scale = new Vector2(value, value);
            }
        }
        
        public BubbleType BubbleType => _settings.BubbleType;

        public Bubble(Settings settings) : base(settings)
        {
        }
    }
}