using Core.Shared;
using Core.Shared.Systems;
using Gameplay.Camera;
using Gameplay.Clients;
using Gameplay.Interactables;
using Gameplay.Orders;
using Gameplay.Player;
using Gameplay.Tables;
using Gameplay.Tools;
using Gameplay.Tray;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core 
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ClientsSpawner _clientsSpawner;
        [SerializeField] private TraySkin _traySkin;


        private void Awake()
        {
            List<Tool> availableTools = FindObjectsOfType<Tool>().ToList();
            List<Door> doors = FindObjectsOfType<Door>(true).ToList();

            SystemsLocator.RegisterSystem(new ClientsSystem(_clientsSpawner));
            SystemsLocator.RegisterSystem(new TablesSystem());
            SystemsLocator.RegisterSystem(new ToolsSystem(availableTools));
            SystemsLocator.RegisterSystem(new OrdersSystem());
            SystemsLocator.RegisterSystem(new TraySystem(_traySkin));


            SystemsLocator.CreateAllSystems();
            SystemsLocator.InitializeAllSystems();
            SystemsLocator.StartAllSystems();


            _player.Initialize();
            _cameraController.Initialize();

            foreach (MoveTarget target in FindObjectsByType<MoveTarget>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                target.Initialize();

            foreach (Door door in doors)
                door.Initialize();
        }

        private void Start()
        {
            
        }
    }
}