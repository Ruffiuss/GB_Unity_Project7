using Abstractions.Commands;
using UI.Model.CommandCreators;
using UnityEngine;
using UserControlSystem.UI.Model.CommandCreators;
using Utils;
using Zenject;

namespace UserControlSystem.UI.Model
{
    public class UiModelInstaller : MonoInstaller
    {
        #region Fields

        [Inject] private AssetsContext _legacyContext;
        [Inject] private SelectableValue _selectableValue;
        [Inject] private Vector3Value _vector3Value;
        [Inject] private AttackableValue _attackableValue;

        #endregion

        #region Methods

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_legacyContext);
            Container.Bind<SelectableValue>().FromInstance(_selectableValue);
            Container.Bind<Vector3Value>().FromInstance(_vector3Value);
            Container.Bind<AttackableValue>().FromInstance(_attackableValue);


            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
            .To<ProduceUnitCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>()
            .To<AttackCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>()
            .To<MoveCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>()
            .To<PatrolCommandCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>()
            .To<StopCommandCommandCreator>().AsTransient();
            Container.Bind<CommandButtonsModel>().AsTransient();
        }

        #endregion
    }
}
