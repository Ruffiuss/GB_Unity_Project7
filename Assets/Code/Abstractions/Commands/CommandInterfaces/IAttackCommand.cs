namespace Abstractions.Commands
{
    public interface IAttackCommand : ICommand
    {
        public IAttackable Target { get; }
    }
}