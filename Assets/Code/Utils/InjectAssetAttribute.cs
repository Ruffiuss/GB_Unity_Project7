using System;

namespace Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAssetAttribute : Attribute
    {
        #region Fields

        public readonly string AssetName;

        #endregion

        #region ClassLifeCycles

        public InjectAssetAttribute(string assetName = null)
        {
            AssetName = assetName;
        }

        #endregion
    }
}
