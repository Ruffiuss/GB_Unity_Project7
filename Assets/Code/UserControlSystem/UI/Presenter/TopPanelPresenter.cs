using Abstractions;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserControlSystem.UI.Presenter
{
    public sealed class TopPanelPresenter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _menuGO;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_InputField _inputField;

        #endregion

        #region Methods

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _inputField.text = $"{t.Minutes:D2}:{t.Seconds:D2}";
            });

            _menuButton.OnClickAsObservable().Subscribe(_ => _menuGO.SetActive(true));
        }

        #endregion
    }
}
