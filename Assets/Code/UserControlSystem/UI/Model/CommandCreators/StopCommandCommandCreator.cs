using Abstractions.Commands;
using System;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model.CommandCreators;
using Utils;
using Zenject;

namespace UI.Model.CommandCreators
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        #region Fields

        [Inject] private AssetsContext _context;

        #endregion

        #region Methods

        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new
            StopCommand()));
        }

        #endregion
    }
}
