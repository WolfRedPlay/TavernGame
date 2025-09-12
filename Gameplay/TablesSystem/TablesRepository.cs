using Core.Shared.Systems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Tables
{
    public class TablesRepository : Repository
    {
        List<Table> _tables;

        public override void OnCreated()
        {
            _tables = new List<Table>();

            _tables = GameObject.FindObjectsOfType<Table>().ToList();
        }

        public override void Initialize()
        {
            uint id = 0;
            foreach (var table in _tables)
            {
                table.Initialize(id);
                id++;
            }
        }


        public override void OnStarted()
        {
        }


        public List<Table> GetFreeTableOfSeatsAmount(int seatsAmount)
        {
            return _tables.FindAll(x => (x.Seats.Count >= seatsAmount && x.IsBusy == false));
        }


        public void AddTable(Table newTable)
        {
            _tables.Add(newTable);
        }
    }
}
