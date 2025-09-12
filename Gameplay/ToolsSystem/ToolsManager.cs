using Core.Shared;
using Core.Shared.Systems;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Tools
{
    public class ToolsManager : Manager
    {
        private ToolsRepository _repository;

        
        public ToolsManager(ToolsRepository repository, List<Tool> tools)
        {
            _repository = repository;
            _repository.SetAvailableTools(tools);
        }


        public override void OnCreated()
        {
        }

        public override void Initialize()
        {
        }

        public override void OnStarted()
        {
        }

        
        public List<Item_Data> GetAllAvailableItems()
        {
            List<Item_Data> availableItems = new List<Item_Data>();

            foreach (Tool tool in _repository.AvailableTools)
                availableItems = availableItems.Union(tool.ItemsData).ToList();

            return availableItems;
        }
    }
}