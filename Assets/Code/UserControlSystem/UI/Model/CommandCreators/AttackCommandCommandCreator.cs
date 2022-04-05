using Abstractions.Commands;
using System;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model.CommandCreators;
using Utils;
using Zenject;

namespace UI.Model.CommandCreators
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        #region Fields

        [Inject] private AssetsContext _context;

        #endregion

        #region Methods

        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new
            AttackCommand()));
        }

        #endregion
    }
}
