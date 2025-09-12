using Core.Shared.Systems;

namespace Gameplay.Tray
{
    public class TraySystem : GameSystem<TrayRepository, TrayManager>
    {
        TraySkin _skin;


        public TraySystem(TraySkin skin)
        {
            _skin = skin;
        }


        protected override TrayManager CreateManager()
        {
            return new TrayManager(Repository, _skin);
        }
    }
}
