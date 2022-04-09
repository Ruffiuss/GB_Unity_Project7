using Abstractions.Commands;
using System;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model;
using UserControlSystem.UI.Model.CommandCreators;
using Utils;
using Zenject;

namespace UI.Model.CommandCreators
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        #region Fields

        [Inject] private SelectableValue _selectable;

        #endregion

        #region Properties

        private Action<IPatrolCommand> _onCreated;

        #endregion

        #region Methods

        protected override IPatrolCommand CreateCommand(Vector3 argument) =>
            new PatrolCommand(_selectable.CurrentValue.CurrentPosition.position, argument);

        #endregion
    }
}
