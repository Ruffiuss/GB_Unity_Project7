using Abstractions;
using System;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = nameof(AttackableValue), menuName = "Strategy Game/" + nameof(AttackableValue), order = 0)]
    public sealed class AttackableValue : ScriptableObject
    {
        #region Properties

        public IAttackable CurrentTarget { get; private set; }
        public Action<IAttackable> OnNewTarget;

        #endregion

        #region Methods

        public void SetValue(IAttackable target)
        {
            CurrentTarget = target;
            OnNewTarget?.Invoke(target);
        }

        #endregion
    }
}
