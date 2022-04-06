using Abstractions.Commands;
using UnityEngine;

namespace UserControlSystem.CommandsRealization
{
    public class PatrolCommand : IPatrolCommand
    {
        #region Fields

        private Vector3 _startPosition;
        private Vector3 _finishPosition;

        #endregion

        #region Properties

        public Vector3 StartPosition => _startPosition;

        public Vector3 FinishPosition => _finishPosition;

        #endregion

        #region ClassLifeCycles

        public PatrolCommand(Vector3 start, Vector3 finish)
        {
            _startPosition = start;
            _finishPosition = finish;
        }

        #endregion
    }
}
