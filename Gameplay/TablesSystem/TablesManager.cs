using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Tables;
using System.Collections.Generic;

namespace Gameplay.Tables
{
    public class TablesManager : Manager
    {
        TablesRepository _repository;

        public TablesManager(TablesRepository repository)
        {
            _repository = repository;
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


        public bool TryGetFreeTable(int clientsAmount, out Table freeTable)
        {
            List<Table> suitableTables = _repository.GetFreeTableOfSeatsAmount(clientsAmount);

            if (suitableTables.Count == 0)
            {
                freeTable = null;
                return false;
            }
            else
            {
                freeTable = suitableTables.GetRandom();
                return true;
            }
        }
    }
}
