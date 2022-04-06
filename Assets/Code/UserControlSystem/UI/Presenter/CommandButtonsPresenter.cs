using Abstractions;
using Abstractions.Commands;
using System.Collections.Generic;
using UnityEngine;
using UserControlSystem.UI.Model;
using UserControlSystem.UI.View;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;

        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;
            _selectable.OnNewValue += OnSelected;
            OnSelected(_selectable.CurrentValue);
        }

        #endregion

        #region Methods

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
            return;
            }
            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }
            _currentSelectable = selectable;
            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as
                Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }
        #endregion
    }
}
