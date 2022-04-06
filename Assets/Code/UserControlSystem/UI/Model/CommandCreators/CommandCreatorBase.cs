using Abstractions.Commands;
using System;

namespace UserControlSystem.UI.Model.CommandCreators
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        #region Methods

        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor
        commandExecutor, Action<T> callback)
        {
            var classSpecificExecutor = commandExecutor as
            CommandExecutorBase<T>;
            if (classSpecificExecutor != null)
            {
                ClassSpecificCommandCreation(callback);
            }
            return commandExecutor;
        }
        
        protected abstract void ClassSpecificCommandCreation(Action<T> creationCallback);
        
        public virtual void ProcessCancel() { }

        #endregion
    }
}
