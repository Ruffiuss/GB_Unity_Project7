using Abstractions.Commands;
using System;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model.CommandCreators;
using Utils;
using Zenject;

namespace UI.Model.CommandCreators
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        #region Fields

        [Inject] private AssetsContext _context;

        #endregion

        #region Methods

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new
            PatrolCommand()));
        }

        #endregion
    }
}
