using Abstractions.Commands;
using UnityEngine;

namespace Core.CommandExecutors
{
    public sealed class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{this.name} attacking");
        }
    }
}
