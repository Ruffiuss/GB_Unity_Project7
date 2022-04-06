using UnityEngine;

namespace Abstractions
{
    public interface  ISelectable : IHealthable
    {
        Transform CurrentPosition { get; }
        Sprite Icon { get; }
        void SetSelected(bool isSelected);
    }
}
