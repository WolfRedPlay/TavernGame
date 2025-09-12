using Core.Events;
using Core.Shared;

namespace Gameplay.Player
{
    public class PlayerInteractor : Interactor
    {
        public PlayerInteractor(MovementController movementController) : base(movementController)
        {
            EventManager.AddListener<MovePlayerEvent>(OnPlayerMove);
        }

        private void OnPlayerMove(MovePlayerEvent evt)
        {
            _interactable = evt.Interactable;
        }
    }
}