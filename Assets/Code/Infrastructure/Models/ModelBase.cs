using System;
using UnityEngine;

namespace Code.Infrastructure.Models
{
    public abstract class ModelBase<TSettings> : IModel
        where TSettings : ModelSettingsBase
    {
        public event EventHandler PositionChanged;
        public event EventHandler AngleChanged;
        public event EventHandler ScaleChanged;
        public event EventHandler Destroyed;

        public Vector2 Position
        {
            get => _settings.Position;
            set
            {
                _settings.Position = value;
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public float Angle
        {
            get => _settings.Angle;
            set
            {
                _settings.Angle = value;
                AngleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Vector2 Scale
        {
            get => _settings.Scale;
            set
            {
                _settings.Scale = value;
                ScaleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // ReSharper disable once InconsistentNaming
        protected readonly TSettings _settings;

        protected ModelBase(TSettings settings)
        {
            _settings = settings;
        }

        public void Destroy() => Destroyed?.Invoke(this, EventArgs.Empty);
    }
}