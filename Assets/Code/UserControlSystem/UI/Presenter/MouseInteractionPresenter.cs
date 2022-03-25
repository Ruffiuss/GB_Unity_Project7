using Abstractions;
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

        private IUnitProducer _unitProducer;
        private ISelectable _lastSelected;

        #endregion

        #region UnityMethods

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                _lastSelected.SetSelected(false);
                _unitProducer = null;
                return;
            }
            var lastHit = hits.Select(
                    hit => hit.collider.GetComponentInParent<ISelectable>())
                    .FirstOrDefault(c => c != null);
            if (lastHit != null)
            {
                _lastSelected = lastHit;
                _lastSelected.SetSelected(true);
            }
            else
                _lastSelected.SetSelected(false);

            _selectedObject.SetValue(lastHit);
                

            _unitProducer = hits.Select(
                hit => hit.collider.GetComponentInParent<IUnitProducer>())
                .FirstOrDefault(c => c != null);
            _unitProducer?.ProduceUnit();
        }

        #endregion
    }
}
