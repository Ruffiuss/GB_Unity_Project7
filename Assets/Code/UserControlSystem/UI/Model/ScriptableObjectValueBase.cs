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

        public void SetValue(T target)
        {
            CurrentValue = target;
            OnNewValue?.Invoke(target);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }

        #endregion

        public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
        {
            private readonly ScriptableObjectValueBase<TAwaited> _scriptableObjectValueBase;
            private TAwaited _result;
            private Action _continuation;
            private bool _isCompleted;

            public NewValueNotifier(ScriptableObjectValueBase<TAwaited> scriptableObjectValueBase)
            {
                _scriptableObjectValueBase = scriptableObjectValueBase;
                _scriptableObjectValueBase.OnNewValue += OnNewValue;
            }

            private void OnNewValue(TAwaited value)
            {
                _scriptableObjectValueBase.OnNewValue -= OnNewValue;
                _result = value;
                _isCompleted = true;
                _continuation?.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                if (_isCompleted)
                {
                    continuation?.Invoke();
                }
                else
                {
                    _continuation = continuation;
                }
            }

            public bool IsCompleted => _isCompleted;

            public TAwaited GetResult() => _result;
        }
    }
}