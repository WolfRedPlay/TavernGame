namespace Core.Shared.Systems
{
    public abstract class Manager
    {
        public abstract void OnCreated();
        public abstract void Initialize();
        public abstract void OnStarted();
    }
}
