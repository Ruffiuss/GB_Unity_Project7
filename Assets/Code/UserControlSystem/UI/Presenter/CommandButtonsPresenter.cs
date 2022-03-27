using Abstractions;
using Abstractions.Commands;
using System;
using System.Collections.Generic;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.Model;
using UserControlSystem.UI.View;
using Utils;

namespace UserControlSystem.UI.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }

        #endregion

        #region Methods

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }
            _currentSelectable = selectable;

            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null)
            {
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommand()));
                return;
            }
            throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)}: "
                + $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
        }

        #endregion
    }
}
