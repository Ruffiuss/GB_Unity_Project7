using Abstractions.Commands;
using System.Threading;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}
