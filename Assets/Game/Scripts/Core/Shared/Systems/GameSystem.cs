namespace Core.Shared.Systems
{
    public abstract class GameSystem<R, M> : ISystem 
        where R : Repository, new()
        where M : Manager
    {
        public R Repository { get; private set; }
        public M Manager { get; private set; }


        protected abstract M CreateManager();


        public virtual void Create()
        {
            Repository = new();
            Repository.OnCreated();

            Manager = CreateManager();
            Manager.OnCreated();
        }

        public virtual void Initialize()
        {
            Repository.Initialize();
            Manager.Initialize();
        }

        public virtual void Start()
        {
            Repository.OnStarted();
            Manager.OnStarted();
        }
    }
}
