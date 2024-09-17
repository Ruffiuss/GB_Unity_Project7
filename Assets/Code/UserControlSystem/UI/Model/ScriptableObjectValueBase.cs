using System;
using UnityEngine;
using Utils;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName = "AttackableValue", menuName = "Strategy Game/AttackableValue", order = 0)]
    public class ScriptableObjectValueBase<T> : ScriptableObject, IAwaitable<T>
    {
        #region Properties

        public T CurrentValue { get; private set; }
        public Action<T> OnNewValue;

        #endregion

        #region Methods

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }

        #endregion

        public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;

            public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
            {
                _scriptableObjectValueBase = scriptableObjectValueBase;
                _scriptableObjectValueBase.OnNewValue += OnNewValue;
            }

            private void OnNewValue(TAwaited value)
            {
                _scriptableObjectValueBase.OnNewValue -= OnNewValue;
                OnWaitFinish(value);
            }
        }
    }
}