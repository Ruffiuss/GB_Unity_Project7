using Abstractions;
using Abstractions.Commands;
using UnityEngine;

namespace Core
{
    public sealed class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable
    {
        #region Fields

        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Sprite _icon;
        [SerializeField] private SpriteRenderer _selector;

        [SerializeField] private float _maxHealth = 1000.0f;

        private float _health = 1000.0f;
        private bool _isSelected;

        #endregion

        #region Properties

        public Sprite Icon => _icon;
        public float MaxHealth => _maxHealth;
        public float Health => _health;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            SetSelected(false);
        }

        #endregion

        #region Methods

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
            => Instantiate(command.UnitPrefab,
                new Vector3(Random.Range(-5, 35), 1, Random.Range(-5, 25)),
                Quaternion.identity,
                _unitsParent);

        public void SetSelected(bool isSelected)
        {
            if (_isSelected != isSelected)
                _isSelected = isSelected;
           _selector.enabled = _isSelected;
        }

        #endregion
    }
}
