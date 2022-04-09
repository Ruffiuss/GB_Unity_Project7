using System;

namespace Utils
{
    public abstract class AwaiterBase<TAwaited> : IAwaiter<TAwaited>
    {
        #region Fields

        private Action _continuation;
        private TAwaited _result;
        private bool _isCompleted;

        #endregion

        #region Properties

        public bool IsCompleted => _isCompleted;

        #endregion

        #region Methods

        public TAwaited GetResult() => _result;

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
                continuation?.Invoke();
            else
                _continuation = continuation;
        }

        protected void OnWaitFinish(TAwaited result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }

        #endregion
    }
}
