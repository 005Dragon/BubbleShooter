using System;
using System.Linq;
using Code.Models;
using UnityEngine;

namespace Code.Views
{
    [RequireComponent(typeof(LineRenderer))]
    public class BubbleShooterAimView : MonoBehaviour, IView
    {
        public IModel Model { get; set; }

        private BubbleShooterAim _model;
        private LineRenderer _lineRenderer;
        
        public void Initialize()
        {
            _model = (BubbleShooterAim)Model;
            _lineRenderer = GetComponent<LineRenderer>();
            
            _model.Updated += ModelOnUpdated;
        }

        private void ModelOnUpdated(object sender, EventArgs eventArgs)
        {
            _lineRenderer.enabled = _model.Aiming;
            _lineRenderer.positionCount = _model.Points.Count;
            _lineRenderer.SetPositions(_model.Points.Select(x => (Vector3)x).ToArray());
        }
    }
}