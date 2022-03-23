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

        [SerializeField] private float _maxHealth = 1000.0f;

        private float _health = 1000.0f;

        #endregion

        #region Properties

        public Sprite Icon => _icon;
        public float MaxHealth => _maxHealth;
        public float Health => _health;

        #endregion

        #region Methods

        public void ProduceUnit()
        {
            Instantiate(_unitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
        }

        #endregion
    }
}
