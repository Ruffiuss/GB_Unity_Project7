using System;
using UniRx;

namespace UserControlSystem.UI.Model
{
    public abstract class StatlessSOValueBase<T> : ScriptableObjectValueBase<T>, IObservable<T>
    {
        #region Fields

        private Subject<T> _property = new Subject<T>();

        #endregion

        #region Methods

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _property.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _property.Subscribe(observer);
        }

        #endregion
    }
}