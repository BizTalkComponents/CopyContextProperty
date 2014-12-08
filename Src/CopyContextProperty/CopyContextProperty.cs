using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;

namespace BizTalkComponents.PipelineComponents.CopyContextProperty
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("98EB6BA0-4FBC-44C5-8A53-A7D37C46A396")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public partial class CopyContextProperty : IComponent, IBaseComponent,
                                        IPersistPropertyBag, IComponentUI
    {
        private const string SourcePropertyName = "SourceProperty";
        private const string DestinationPropertyName = "DestinationPropertyName";

        [RequiredRuntime]
        [DisplayName("Source Property Path")]
        [Description("The property path of the property to copy from.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        [ContextPropertyAttribute(PropertyName = "SourceProperty")]
        public string SourceProperty { get; set; }

        [RequiredRuntime]
        [DisplayName("Destination Property Path")]
        [Description("The property path of the property to copy to.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        [ContextPropertyAttribute(PropertyName = "DestinationPropertyName")]
        public string DestinationProperty { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;

            if (!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            var sourceContextProperty = new ContextProperty(SourceProperty);
            var destinationContextProperty = new ContextProperty(DestinationProperty);

            pInMsg.Context.Copy(sourceContextProperty, destinationContextProperty);

            return pInMsg;
        }


        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            List<PropertyInfo> result =
    typeof(CopyContextProperty)
    .GetProperties()
    .Where(
        p =>
            p.GetCustomAttributes(typeof(ContextPropertyAttribute), true)
            .Any()
        )
    .ToList();

            foreach (var p in result)
            {
                
                
                var attr = p.GetCustomAttributes(typeof (ContextPropertyAttribute), true);
                var val = attr.GetValue(0);

                p.SetValue(PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, SourcePropertyName),string.Empty,);
            }
            SourceProperty = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, SourcePropertyName), string.Empty);
            DestinationProperty = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, DestinationPropertyName), string.Empty);
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            PropertyBagHelper.WritePropertyBag(propertyBag, SourcePropertyName, SourceProperty);
            PropertyBagHelper.WritePropertyBag(propertyBag, DestinationPropertyName, DestinationProperty);
        }

    }
}
