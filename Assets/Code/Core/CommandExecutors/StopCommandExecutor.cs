using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log($"{this.name} stopped");
        }
    }
}
