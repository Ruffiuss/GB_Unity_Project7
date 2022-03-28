using Abstractions;
using UnityEngine;

namespace Core
{
    public sealed class MainBuilding : MonoBehaviour, IUnitProducer, ISelectable
    {
        #region Fields

        [SerializeField] private GameObject _unitPrefab;
        [SerializeField] private Transform _unitsParent;
        [SerializeField] private Sprite _icon;
        [SerializeField] private SpriteRenderer _selector;

        [SerializeField] private float _maxHealth = 1000.0f;

        private float _health = 1000.0f;
        private bool _isSelected = false;

        #endregion

        #region Properties

        public Sprite Icon => _icon;
        public float MaxHealth => _maxHealth;
        public float Health => _health;

        #endregion

        #region Methods

        public void ProduceUnit()
        {
            Instantiate(_unitPrefab, new Vector3(Random.Range(-5, 35), 0, Random.Range(-5, 25)), Quaternion.identity, _unitsParent);
        }

        public void SetSelected(bool isSelected)
        {
            if (_isSelected != isSelected)
            {
                _isSelected = isSelected;
                _selector.enabled = _isSelected;
            }
        }

        #endregion
    }
}
