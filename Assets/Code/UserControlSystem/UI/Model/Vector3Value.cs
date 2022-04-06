using System;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy Game/"+ nameof(Vector3Value), order = 0)]
    public sealed class Vector3Value : ScriptableObject
    {
        #region Properties

        public Vector3 CurrentValue { get; private set; }
        public Action<Vector3> OnNewValue;

        #endregion

        #region Methods

        public void SetValue(Vector3 value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }

        #endregion
    }
}
