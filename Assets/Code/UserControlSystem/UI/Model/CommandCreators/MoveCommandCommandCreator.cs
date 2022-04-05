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
    public class MoveCommandCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        #region Fields

        [Inject] private AssetsContext _context;

        private Action<IMoveCommand> _creationCallback;

        #endregion

        #region Methods

        [Inject]
        private void Init(Vector3Value groundClicks)
        {
            groundClicks.OnNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new
            MoveCommand(groundClick)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IMoveCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _creationCallback = null;
        }

        #endregion
    }
}
