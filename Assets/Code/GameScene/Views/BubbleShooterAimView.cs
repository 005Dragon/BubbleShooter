using System;
using System.Linq;
using Code.GameScene.Models;
using Code.Infrastructure.Models;
using Code.Infrastructure.Views;
using UnityEngine;

namespace Code.GameScene.Views
{
    [RequireComponent(typeof(LineRenderer))]
    public class BubbleShooterAimView : ViewBase<BubbleShooterAim>
    {
        private LineRenderer _lineRenderer;
        
        public override void Initialize(IModel model)
        {
            base.Initialize(model);

            _lineRenderer = GetComponent<LineRenderer>();
            
            Model.Updated += ModelOnUpdated;
        }

        private void ModelOnUpdated(object sender, EventArgs eventArgs)
        {
            _lineRenderer.enabled = Model.Aiming;
            _lineRenderer.positionCount = Model.Points.Count;
            _lineRenderer.SetPositions(Model.Points.Select(x => (Vector3)x).ToArray());
        }

        protected override void FreeView()
        {
            Model.Updated -= ModelOnUpdated;
            
            base.FreeView();
        }
    }
}