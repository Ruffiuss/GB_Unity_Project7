using Abstractions;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class TimeModel : ITimeModel, ITickable
    {
        #region Properties

        public IObservable<int> GameTime => _gameTime.Select(f => (int)f);

        private readonly ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        #endregion

        #region Methods

        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }

        #endregion
    }
}
