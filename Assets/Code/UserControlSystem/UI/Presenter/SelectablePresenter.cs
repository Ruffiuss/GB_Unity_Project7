using Abstractions;
using UserControlSystem.UI.Model;

namespace UserControlSystem.UI.Presenter
{
    public sealed class SelectablePresenter
    {
        #region Fields

        private SelectableValue _selectedObject;

        private ISelectable _lastSelected;

        #endregion

        #region ClassLifeCycles

        public SelectablePresenter(SelectableValue selectableValue)
        {
            _selectedObject = selectableValue;
        }

        #endregion

        #region Methods

        public void ChangeSelected(ISelectable selected)
        {
            if (selected == null)
            {
                _lastSelected.SetSelected(false);
            }
            else
            {
                _lastSelected = selected;
                _lastSelected.SetSelected(true);
            }
            _selectedObject.SetValue(selected);
        }

        #endregion
    }
}
