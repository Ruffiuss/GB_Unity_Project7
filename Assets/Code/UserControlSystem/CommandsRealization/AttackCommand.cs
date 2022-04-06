using Abstractions;
using Abstractions.Commands;

namespace UserControlSystem.CommandsRealization
{
    public class AttackCommand : IAttackCommand
    {
        #region Properties

        public IAttackable Target { get; }

        #endregion

        #region Methods

        public AttackCommand(IAttackable target)
        {
            Target = target;
        }

        #endregion
    }
}
