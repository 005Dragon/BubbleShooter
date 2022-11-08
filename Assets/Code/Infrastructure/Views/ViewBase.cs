using System;
using Code.Infrastructure.Models;
using UnityEngine;

namespace Code.Infrastructure.Views
{
    public class ViewBase<TModel> : MonoBehaviour, IView
        where TModel : class, IModel
    {
        public bool Initialized => Model != null;
        
        public TModel Model { get; private set; }

        protected GameObject CachedGameObject { get; private set; }
        protected Transform CachedTransform { get; private set; }


        public virtual void Initialize(IModel model)
        {
            CachedGameObject = gameObject;
            CachedTransform = transform;
            
            Model = (TModel)model;
            
            UpdatePosition();
            UpdateAngle();
            UpdateScale();
            
            Model.PositionChanged += ModelOnPositionChanged;
            Model.AngleChanged += ModelOnAngleChanged;
            Model.ScaleChanged += ModelOnScaleChanged;
            Model.Destroyed += ModelOnDestroyed;
        }

        protected virtual void UpdatePosition()
        {
            CachedTransform.localPosition = Model.Position;
        }

        protected virtual void UpdateAngle()
        {
            CachedTransform.localRotation = Quaternion.AngleAxis(Model.Angle, Vector3.forward);
        }

        protected virtual void UpdateScale()
        {
            CachedTransform.localScale = Model.Scale;
        }

        protected virtual void FreeView()
        {
            Model.Destroyed -= ModelOnDestroyed;
            Model.PositionChanged -= ModelOnPositionChanged;
            Model.AngleChanged -= ModelOnAngleChanged;
            Model.ScaleChanged -= ModelOnScaleChanged;

            Model = null;
            
            gameObject.SetActive(false);
        }

        private void ModelOnPositionChanged(object sender, EventArgs eventArgs)
        {
            UpdatePosition();
        }

        private void ModelOnAngleChanged(object sender, EventArgs eventArgs)
        {
            UpdateAngle();
        }

        private void ModelOnScaleChanged(object sender, EventArgs eventArgs)
        {
            UpdateScale();
        }

        private void ModelOnDestroyed(object sender, EventArgs eventArgs)
        {
            FreeView();
        }
    }
}