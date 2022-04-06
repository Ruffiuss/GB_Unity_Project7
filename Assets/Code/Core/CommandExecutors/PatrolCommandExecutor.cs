using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log($"{this.name} patroling from {command.StartPosition} to {command.FinishPosition}");
        }
    }
}
