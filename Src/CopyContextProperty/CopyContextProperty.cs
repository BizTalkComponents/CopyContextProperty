using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [RequiredRuntime]
        [DisplayName("Source Property Path")]
        [Description("The property path of the property to copy from.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        public string SourceProperty { get; set; }

        [RequiredRuntime]
        [DisplayName("Destination Property Path")]
        [Description("The property path of the property to copy to.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        public string DestinationPropertyName { get; set; }

        [DisplayName("No Promotion")]
        [Description("If it is set to true, the component will not promote the destination property")]
        public bool NoPromotion { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;

            if (!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            var sourceContextProperty = new ContextProperty(SourceProperty);
            var destinationContextProperty = new ContextProperty(DestinationPropertyName);

            object sourceValue;

            if (!pInMsg.Context.TryRead(sourceContextProperty, out sourceValue))
            {
                throw new InvalidOperationException("Could not find the specified source property in BizTalk context.");
            }

            if (NoPromotion)
            {
                pInMsg.Context.Write(destinationContextProperty, sourceValue);
            }
            else
            {
                pInMsg.Context.Promote(destinationContextProperty, sourceValue);
            }
            return pInMsg;
        }
    }
}
