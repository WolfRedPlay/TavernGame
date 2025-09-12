using Core.Shared.Systems;

namespace Gameplay.Tables
{
    public class TablesSystem : GameSystem<TablesRepository, TablesManager>
    {
        protected override TablesManager CreateManager()
        {
            return new TablesManager(Repository);
        }
    }
}
