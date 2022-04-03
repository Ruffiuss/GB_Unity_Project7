using Abstractions;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem.UI.Model;

namespace UserControlSystem.UI.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SelectablePresenter _selectablePresenter;

        #endregion

        #region Properties

        private Action<ISelectable> _onChageSelection;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _onChageSelection += _selectablePresenter.ChangeSelected;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }
            if (_eventSystem.IsPointerOverGameObject())
                return;

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                _onChageSelection.Invoke(null);
                return;
            }
            var lastHit = hits.Select(
                    hit => hit.collider.GetComponentInParent<ISelectable>())
                    .FirstOrDefault(c => c != null);
            _onChageSelection.Invoke(lastHit);
        }

        private void OnDestroy()
        {
            _onChageSelection -= _selectablePresenter.ChangeSelected;
            _selectablePresenter = null;
        }

        #endregion
    }
}
