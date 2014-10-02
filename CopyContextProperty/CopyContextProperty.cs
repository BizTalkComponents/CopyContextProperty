using BizTalkComponents.Utils.ContextPropertyHelpers;
using BizTalkComponents.Utils.PropertyBagHelpers;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;

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

        public string SourceProperty { get; set; }
        public string DestinationProperty { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            var sourceContextProperty = new ContextProperty(SourceProperty);
            var destinationContextProperty = new ContextProperty(DestinationProperty);

            ContextPropertyHelper.CopyContextProperty(pInMsg, sourceContextProperty, destinationContextProperty);

            return pInMsg;
        }

        
        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            if (string.IsNullOrEmpty(SourceProperty))
            {
                SourceProperty = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, SourcePropertyName), string.Empty);
            }

            if (string.IsNullOrEmpty(DestinationProperty))
            {
                DestinationProperty = PropertyBagHelper.ToStringOrDefault(PropertyBagHelper.ReadPropertyBag(propertyBag, DestinationPropertyName), string.Empty);
            }
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            PropertyBagHelper.WritePropertyBag(propertyBag, SourcePropertyName, SourceProperty);
            PropertyBagHelper.WritePropertyBag(propertyBag, DestinationPropertyName, DestinationProperty);
        }
        
    }
}
