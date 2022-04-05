using UnityEngine;

namespace Abstractions.Commands
{
    public interface IMoveCommand : ICommand
    {
        public Vector3 Target { get; }
    }
}