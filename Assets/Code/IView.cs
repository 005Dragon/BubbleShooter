namespace Code
{
    public interface IView
    {
        IModel Model { get; set; }

        void Initialize();
    }
}