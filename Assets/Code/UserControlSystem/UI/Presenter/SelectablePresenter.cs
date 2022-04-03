using Abstractions;
using UnityEngine;
using UserControlSystem.UI.Model;

namespace UserControlSystem.UI.Presenter
{
    public sealed class SelectablePresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SelectableValue _selectedObject;

        private ISelectable _lastSelected;

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
                _lastSelected?.SetSelected(false);
                _lastSelected = selected;
                _lastSelected.SetSelected(true);
            }
            _selectedObject.SetValue(selected);
        }

        #endregion
    }
}
