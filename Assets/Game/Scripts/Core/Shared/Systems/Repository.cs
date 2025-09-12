namespace Core.Shared.Systems
{
    public abstract class Repository
    {
        public abstract void OnCreated();
        public abstract void Initialize();
        public abstract void OnStarted();
    }
}
