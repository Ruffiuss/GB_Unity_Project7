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
                return;
            }
            var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault(c => c != null);
            _selectedObject.SetValue(selectable);
        }

        #endregion
    }
}
