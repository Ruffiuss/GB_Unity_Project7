using Abstractions;
using System;
using UnityEngine;
using UserControlSystem.UI.Model;
using Utils;
using Zenject;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    #region Fields

    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private AttackableValue _attackableClicksRMB;
    [SerializeField] private SelectableValue _selectables;


    #endregion

    #region Methods

    public override void InstallBindings()
    {
        Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableClicksRMB);
        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectables);
        Container.Bind<IAwaitable<Vector3>>().FromInstance(_groundClicksRMB);
        Container.BindInstances(_legacyContext, _groundClicksRMB, _attackableClicksRMB, _selectables);
    }

    #endregion
}