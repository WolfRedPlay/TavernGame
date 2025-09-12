namespace Core.Shared.States
{
    public interface IState
    {
        public void OnStart();
        public void OnUpdate();
        public void OnStop();
    }
}
