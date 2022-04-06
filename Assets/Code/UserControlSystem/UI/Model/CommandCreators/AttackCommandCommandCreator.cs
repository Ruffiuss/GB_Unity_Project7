using Abstractions;
using Abstractions.Commands;
using System;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model;
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

        #region Properties

        private Action<IAttackCommand> _onCreated;

        #endregion

        #region Methods

        [Inject]
        private void Init(AttackableValue groundCLicks)
        {
            groundCLicks.OnNewTarget += OnNewTarget;
        }

        private void OnNewTarget(IAttackable target)
        {
            _onCreated?.Invoke(_context.Inject(new AttackCommand(target)));
            _onCreated = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            _onCreated = creationCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _onCreated = null;
        }

        #endregion
    }
}
