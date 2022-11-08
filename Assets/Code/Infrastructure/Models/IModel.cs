using System;
using UnityEngine;

namespace Code.Infrastructure.Models
{
    public interface IModel
    {
        event EventHandler PositionChanged;
        event EventHandler AngleChanged;
        event EventHandler ScaleChanged;
        event EventHandler Destroyed;
        
        Vector2 Position { get; set; }
        float Angle { get; set; }
        Vector2 Scale { get; set; }

        void Destroy();
    }
}