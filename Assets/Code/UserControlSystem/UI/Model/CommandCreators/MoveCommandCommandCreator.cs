using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model.CommandCreators;

namespace UI.Model.CommandCreators
{
    public sealed class MoveCommandCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
    {
        #region Methods

        protected override IMoveCommand CreateCommand(Vector3 argument) => new MoveCommand(argument);

        #endregion
    }
}
