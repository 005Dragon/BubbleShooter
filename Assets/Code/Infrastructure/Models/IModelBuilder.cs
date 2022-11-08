using System;

namespace Code.Infrastructure.Models
{
    public interface IModelBuilder
    {
        Type ModelType { get; }

        IModel Build(ModelSettingsBase settings);
    }
}