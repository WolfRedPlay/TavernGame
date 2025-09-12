using Core.Shared;
using Core.Shared.Systems;
using System.Collections.Generic;

namespace Gameplay.Tools
{
    public class ToolsRepository : Repository
    {
        private List<Tool> _availableTools;


        public List<Tool> AvailableTools => _availableTools;


        public override void OnCreated()
        {
        }

        public override void Initialize()
        {
        }

        public override void OnStarted()
        {
        }


        public void SetAvailableTools(List<Tool> tools)
        {
            _availableTools = tools;
        }

        public void AddNewTool(Tool newTool)
        {
            _availableTools.Add(newTool);
        }
    }
}