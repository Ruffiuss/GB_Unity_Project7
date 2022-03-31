using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class AsetsInjector
    {
        #region Fields

        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

        #endregion

        #region Methods

        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();
            var allFields = targetType.GetFields(BindingFlags.NonPublic
                | BindingFlags.Public
                | BindingFlags.Instance);
            for (int i = 0; i < allFields.Length; i++)
            {
                var fieldInfo = allFields[i];
                var injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                if (injectAssetAttribute == null)
                {
                    continue;
                }
                var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                fieldInfo.SetValue(target, objectToInject);
            }

            return target;
        }

        #endregion
    }
}
