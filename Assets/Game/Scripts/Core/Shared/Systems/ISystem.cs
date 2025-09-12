namespace Core.Shared.Systems
{
    public interface ISystem
    {
        public void Create();
        public void Initialize();
        public void Start();
    }
}