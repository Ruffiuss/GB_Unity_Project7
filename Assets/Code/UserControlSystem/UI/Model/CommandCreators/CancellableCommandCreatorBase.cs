using Abstractions.Commands;
using System;
using System.Threading;
using Utils;
using Zenject;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : ICommand
    {
        #region Fields

        [Inject] private AssetsContext _context;
        [Inject] private IAwaitable<TArgument> _awaitableArgument;

        private CancellationTokenSource _ctSource;

        #endregion

        #region Methods

        protected override async void ClassSpecificCommandCreation(Action<TCommand> creationCallback)
        {
            _ctSource = new CancellationTokenSource();
            try
            {
                var argument = await _awaitableArgument.WithCancellation(_ctSource.Token);
                creationCallback?.Invoke(_context.Inject(CreateCommand(argument)));
            }
            catch { }
        }

        protected abstract TCommand CreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            if (_ctSource !=null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }

        #endregion
    }
}
