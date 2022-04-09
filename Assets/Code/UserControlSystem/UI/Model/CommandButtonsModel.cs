﻿using Abstractions.Commands;
using System;
using UserControlSystem.UI.Model.CommandCreators;
using Zenject;

namespace UserControlSystem.UI.Model
{
    public sealed class CommandButtonsModel
    {
        #region Fields

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;

        private bool _commandIsPending;

        #endregion

        #region Properties

        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        #endregion

        #region Methods

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
        {
            if (_commandIsPending)
            {
                processOnCancel();
            }
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);
            _unitProducer.ProcessCommandExecutor(commandExecutor, command =>
            executeCommandWrapper(commandExecutor, command));
            _attacker.ProcessCommandExecutor(commandExecutor, command =>
            executeCommandWrapper(commandExecutor, command));
            _stopper.ProcessCommandExecutor(commandExecutor, command =>
            executeCommandWrapper(commandExecutor, command));
            _mover.ProcessCommandExecutor(commandExecutor, command =>
            executeCommandWrapper(commandExecutor, command));
            _patroller.ProcessCommandExecutor(commandExecutor, command =>
            executeCommandWrapper(commandExecutor, command));
        }

        public void executeCommandWrapper(ICommandExecutor commandExecutor, object command)
        {
            commandExecutor.ExecuteCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            processOnCancel();
        }

        private void processOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _stopper.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();
            OnCommandCancel?.Invoke();
        }

        #endregion
    }
}