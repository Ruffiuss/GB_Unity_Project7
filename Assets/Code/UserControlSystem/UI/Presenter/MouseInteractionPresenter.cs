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
        [SerializeField] private Transform _groundTransform;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SelectablePresenter _selectablePresenter;
        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private AttackableValue _attackablesRMB;

        private Plane _groundPlane;

        #endregion

        #region Properties

        private Action<ISelectable> _onChageSelection;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _onChageSelection += _selectablePresenter.ChangeSelected;
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
                return;
            if (_eventSystem.IsPointerOverGameObject())
                return;

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (hits.Length == 0)
            {
                _onChageSelection.Invoke(null);
                return;
            }

            if (CompareHit<ISelectable>(hits, out var selectable))
            {
                _onChageSelection.Invoke(selectable);
            }

            if (CompareHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRMB.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
            }
        }

        private void OnDestroy()
        {
            _onChageSelection -= _selectablePresenter.ChangeSelected;
            _selectablePresenter = null;
        }

        #endregion

        #region Methods

        private bool CompareHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
            {
                return false;
            }
            result = hits
                .Select(hit => hit.collider.GetComponentInParent<T>())
                .Where(c => c != null)
                .FirstOrDefault();
            return result != default;
        }

        #endregion
    }
}
