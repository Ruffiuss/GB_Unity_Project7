using Abstractions;
using System;
using System.Linq;
using UnityEngine;
using UserControlSystem.UI.Model;

namespace UserControlSystem.UI.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableValue _selectedObject;

        private SelectablePresenter _selectablePresenter;
        private IUnitProducer _unitProducer;

        #endregion

        #region Properties

        private Action<ISelectable> _onChageSelection;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _selectablePresenter = new SelectablePresenter(_selectedObject);
            _onChageSelection += _selectablePresenter.ChangeSelected;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                _onChageSelection.Invoke(null);
                _unitProducer = null;
                return;
            }
            var lastHit = hits.Select(
                    hit => hit.collider.GetComponentInParent<ISelectable>())
                    .FirstOrDefault(c => c != null);
            if (lastHit != null)
            {
                _onChageSelection.Invoke(lastHit);
            }
            else
                _onChageSelection.Invoke(null);


            _unitProducer = hits.Select(
                hit => hit.collider.GetComponentInParent<IUnitProducer>())
                .FirstOrDefault(c => c != null);
            _unitProducer?.ProduceUnit();
        }

        private void OnDestroy()
        {
            _onChageSelection -= _selectablePresenter.ChangeSelected;
            _selectablePresenter = null;
        }

        #endregion
    }
}
