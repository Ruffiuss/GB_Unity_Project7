using System;
using UniRx;

namespace UserControlSystem.UI.Model
{
    public abstract class StatefulSOValueBase<T> : ScriptableObjectValueBase<T>, IObservable<T>
    {
        #region Fields

        private ReactiveProperty<T> _property = new ReactiveProperty<T>();

        #endregion

        #region Methods

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _property.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _property.Subscribe(observer);
        }

        #endregion
    }
}