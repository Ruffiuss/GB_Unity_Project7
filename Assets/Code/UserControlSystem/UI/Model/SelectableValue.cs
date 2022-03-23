using Abstractions;
using System;
using UnityEngine;

namespace UserControlSystem.UI.Model
{
    [CreateAssetMenu(fileName =nameof(SelectableValue), menuName ="Strategy Game/" + nameof(SelectableValue), order =0)]
    public class SelectableValue : ScriptableObject
    {
        #region Properties

        public ISelectable CurrentValue { get; private set; }
        public Action<ISelectable> OnSelected;

        #endregion

        #region Methods

        public void SetValue(ISelectable value)
        {
            CurrentValue = value;
            OnSelected?.Invoke(value);
        }

        #endregion
    }
}
