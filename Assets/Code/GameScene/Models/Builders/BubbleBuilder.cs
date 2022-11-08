using Code.Common;
using Code.Infrastructure.Models;

namespace Code.GameScene.Models.Builders
{
    public class BubbleBuilder : ModelBuilderBase<Bubble, Bubble.Settings>
    {
        public float Diameter { get; set; }

        public BubbleBuilder(float diameter)
        {
            Diameter = diameter;
        }

        protected override Bubble Build(Bubble.Settings settings)
        {
            settings.Diameter = Diameter;

            return new Bubble(settings);
        }
    }
}