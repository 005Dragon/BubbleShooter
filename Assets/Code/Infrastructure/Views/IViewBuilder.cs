using System;
using Code.Infrastructure.Models;

namespace Code.Infrastructure.Views
{
    public interface IViewBuilder
    {
        Type ModelType { get; }
        
        void Build(IModel model);
    }
}