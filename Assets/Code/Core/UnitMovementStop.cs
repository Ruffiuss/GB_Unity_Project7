using System;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        #region Fields

        [SerializeField] private NavMeshAgent _agent;

        #endregion

        #region Properties

        public event Action OnStop;

        #endregion

        #region UnityMethods

        void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        OnStop?.Invoke();
                    }
                }
            }
        }

        #endregion

        #region Methods

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);

        #endregion

        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            private readonly UnitMovementStop _unitMovementStop;

            public StopAwaiter(UnitMovementStop unitMovementStop)
            {
                _unitMovementStop = unitMovementStop;
                _unitMovementStop.OnStop += onStop;
            }
            private void onStop()
            {
                _unitMovementStop.OnStop -= onStop;
                OnWaitFinish(new AsyncExtensions.Void());
            }
        }
    }

}
