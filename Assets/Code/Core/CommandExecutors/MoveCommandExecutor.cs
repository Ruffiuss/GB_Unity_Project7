using Abstractions.Commands;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core.CommandExecutors
{
    public sealed class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        #region Fields

        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        private readonly int Walk = Animator.StringToHash("Walk");
        private readonly int Idle = Animator.StringToHash("Idle");

        #endregion

        #region Methods

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            GetComponent<NavMeshAgent>().destination = command.Target;
            _animator.SetTrigger(Walk);

            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
            }
            catch
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
            }
            _stopCommandExecutor.CancellationTokenSource = null;

            _animator.SetTrigger(Idle);
        }

        #endregion
    }
}
