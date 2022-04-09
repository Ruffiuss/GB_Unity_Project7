using Abstractions.Commands;
using UnityEngine;
using UnityEngine.AI;

namespace Core.CommandExecutors
{
    public sealed class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        #region Fields

        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;

        private readonly int Walk = Animator.StringToHash("Walk");
        private readonly int Idle = Animator.StringToHash("Idle");

        #endregion

        #region Methods

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(Walk);
            await _stop;
            _animator.SetTrigger(Idle);
        }

        #endregion
    }
}
