using Abstractions;
using System;
using System.Linq;
using UniRx;
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

        private void Start()
        {
            var LMBclickStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButton(0) && !_eventSystem.IsPointerOverGameObject());
            var RMBclickStream = Observable.EveryUpdate().Where(_ => Input.GetMouseButton(1) && !_eventSystem.IsPointerOverGameObject());

            var LMBray = LMBclickStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var RMBray = RMBclickStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

            var LMBallHits = LMBray.Select(ray => Physics.RaycastAll(ray));
            var RMBallHits = RMBray.Select(ray => (ray, Physics.RaycastAll(ray)));

            LMBallHits.Subscribe(hits =>
            {
                if (CompareHit<ISelectable>(hits, out var selectable))
                {
                    _onChageSelection?.Invoke(selectable);
                }
                else
                    _onChageSelection?.Invoke(null);
            });

            RMBallHits.Subscribe(data =>
            {

                if (CompareHit<IAttackable>(data.Item2, out var attackable))
                {
                    _attackablesRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(data.Item1, out var enter))
                {
                    _groundClicksRMB.SetValue((data.Item1.origin + (data.Item1.direction * enter)));
                }
            });
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
