using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace UserControlSystem.UI.View
{
    public sealed class CommandButtonsView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _produceUnitButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        #endregion

        #region Properties

        public Action<ICommandExecutor> OnClick;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>();
            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
            _buttonsByExecutorType
                .Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _produceUnitButton);
        }

        #endregion

        #region Methods

        public void MakeLayout(IEnumerable<ICommandExecutor> commandExecutors)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                var buttonGameObject = _buttonsByExecutorType
                    .First(type => type
                        .Key
                        .IsInstanceOfType(currentExecutor))
                    .Value;
                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor));
            }
        }

        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                kvp.Value.SetActive(false);
            }
        }

        #endregion
    }
}
