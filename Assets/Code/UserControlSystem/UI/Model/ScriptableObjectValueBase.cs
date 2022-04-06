using System;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = "AttackableValue", menuName = "Strategy Game/AttackableValue", order = 0)]
    public class ScriptableObjectValueBase<T> : ScriptableObject
    {
        #region Properties

        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        #endregion

        #region Methods

        public void SetValue(T target)
        {
            CurrentValue = target;
            OnNewValue?.Invoke(target);
        }

        #endregion
    }
}