using Abstractions;
using UnityEngine;

namespace Core
{
    public class Unit : MonoBehaviour, ISelectable, IAttackable
    {
        #region Fields

        [SerializeField] private Transform _curentPosition;
        [SerializeField] private Sprite _icon;
        [SerializeField] private SpriteRenderer _selector;

        [SerializeField] private float _maxHealth = 1000.0f;

        private float _health = 1000.0f;
        private bool _isSelected;

        #endregion

        #region Properties

        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Transform CurrentPosition => _curentPosition;
        public Sprite Icon => _icon;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            SetSelected(false);
        }

        #endregion

        #region Methods

        public void SetSelected(bool isSelected)
        {
            if (_isSelected != isSelected)
                _isSelected = isSelected;
            _selector.enabled = _isSelected;
        }

        #endregion
    }
}
