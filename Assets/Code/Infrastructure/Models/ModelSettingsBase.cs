using UnityEngine;

namespace Code.Infrastructure.Models
{
    public class ModelSettingsBase
    {
        public Vector2 Position { get; set; }
        public float Angle { get; set; }
        public Vector2 Scale { get; set; } = new(1.0f, 1.0f);
    }
}