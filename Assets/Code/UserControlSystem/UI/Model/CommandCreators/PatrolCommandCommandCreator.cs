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
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        #region Fields

        [Inject] private AssetsContext _context;
        [Inject] private SelectableValue _selectable;

        #endregion

        #region Properties

        private Action<IPatrolCommand> _onCreated;

        #endregion

        #region Methods

        [Inject]
        private void Init(Vector3Value groundClicks)
        {
            groundClicks.OnNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _onCreated?.Invoke(_context.Inject(new PatrolCommand(_selectable.CurrentValue.CurrentPosition.position, groundClick)));
        }

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
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
