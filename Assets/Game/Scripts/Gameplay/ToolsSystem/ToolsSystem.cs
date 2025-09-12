using Core.Shared;
using Core.Shared.Systems;
using System.Collections.Generic;

namespace Gameplay.Tools
{
    public class ToolsSystem : GameSystem<ToolsRepository, ToolsManager>
    {
        private List<Tool> _availableTools;


        public ToolsSystem(List<Tool> tools)
        {
            _availableTools = tools;
        }


        protected override ToolsManager CreateManager()
        {
            return new ToolsManager(Repository, _availableTools);
        }
    }
}
