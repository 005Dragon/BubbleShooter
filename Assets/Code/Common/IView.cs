namespace Code.Common
{
    public interface IView
    {
        IModel Model { get; set; }

        void Initialize();
    }
}