using Abstractions.Commands;
using UnityEngine;
using Utils;

namespace UserControlSystem.CommandsRealization
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        #region Fields

        [InjectAsset("Unit")] private GameObject _unitPrefab;

        #endregion

        #region Properties

        public GameObject UnitPrefab => _unitPrefab;

        #endregion
    }
}
