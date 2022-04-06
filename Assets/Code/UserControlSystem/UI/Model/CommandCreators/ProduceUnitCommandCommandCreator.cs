using Abstractions.Commands;
using System;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model.CommandCreators;
using Utils;
using Zenject;

namespace UI.Model.CommandCreators
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        #region Fields

        [Inject] private AssetsContext _context;

        #endregion

        #region Methods

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new
            ProduceUnitCommandHeir()));
        }

        #endregion
    }
}
