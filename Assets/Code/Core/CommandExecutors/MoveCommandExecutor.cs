using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log($"{this.name} moving");
        }
    }
}
