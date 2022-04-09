using Abstractions;
using Abstractions.Commands;
using System;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model.CommandCreators;

namespace UI.Model.CommandCreators
{
    public sealed class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        #region Methods

        protected override IAttackCommand CreateCommand(IAttackable argument) => new AttackCommand(argument);

        #endregion
    }
}
