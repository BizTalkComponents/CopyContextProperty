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
    public partial class CopyContextPropertyWithNullCoalescing : IComponent, IBaseComponent,
                                        IPersistPropertyBag, IComponentUI
    {
        [RequiredRuntime]
        [DisplayName("First Property Path")]
        [Description("The property path of the property to copy from.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        public string FirstProperty { get; set; }

        [DisplayName("Second Property Path")]
        [Description("The property path of the property to copy from.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        public string SecondProperty { get; set; }


        [RequiredRuntime]
        [DisplayName("Destination Property Path")]
        [Description("The property path of the property to copy to.")]
        [RegularExpression(@"^.*#.*$",
        ErrorMessage = "A property path should be formatted as namespace#property.")]
        public string DestinationProperty { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;

            if (!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            var firstContextProperty = new ContextProperty(FirstProperty);
            var secondContextProperty = new ContextProperty(SecondProperty);
            var destinationContextProperty = new ContextProperty(DestinationProperty);
            object value1, value2;
            pInMsg.Context.TryRead(firstContextProperty, out value1);
            pInMsg.Context.TryRead(secondContextProperty, out value2);
            if (value1 == null & value2 == null)
                throw new InvalidOperationException("Could not find the specified properties in BizTalk context.");
            pInMsg.Context.Promote(destinationContextProperty, value1 ?? value2);
            return pInMsg;
        }
    }
}
