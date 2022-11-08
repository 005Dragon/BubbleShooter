using System;
using System.Collections.Generic;
using Code.Common;
using Code.GameScene.Models.Builders;
using Code.Infrastructure.Models;
using Code.Services;
using UnityEngine;

namespace Code.GameScene.Models
{
    public class BubbleShooterAim : ModelBase<BubbleShooterAim.Settings>, IUpdatable
    {
        public class Settings : ModelSettingsBase
        {
            public int MaxIntersections { get; set; }
            public Map Map { get; set; }
            public GameUserInputService UserInputService { get; set; }
        }
        
        public event EventHandler Updated;
        
        public List<Vector2> Points { get; private set; }

        public bool Aiming => _settings.UserInputService.Aiming;
        
        private readonly BubbleWayBuilder _bubbleWayBuilder;

        public BubbleShooterAim(Settings settings) : base(settings)
        {
            _bubbleWayBuilder = new BubbleWayBuilder(settings.Map, settings.MaxIntersections);
        }

        public bool Update()
        {
            Points = new List<Vector2>() { _settings.Position };
            Vector2 direction = (_settings.UserInputService.GetTargetPosition() - _settings.Position).normalized;
            Points.AddRange(_bubbleWayBuilder.Build(_settings.Position, direction));
            Updated?.Invoke(this, EventArgs.Empty);
            return true;
        }
    }
}