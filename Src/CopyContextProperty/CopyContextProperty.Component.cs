using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;

namespace BizTalkComponents.PipelineComponents.CopyContextProperty
{
    public partial class CopyContextProperty
    {
        [Description("Set to True to disable the component.")]
        public bool Disabled { get; set; }
        public string Name { get { return "CopyContextProperty."; } }
        public string Version { get { return "1.0"; } }
        public string Description { get { return "Copies one context property from another context property."; } }

        public void GetClassID(out Guid classID)
        {
            classID = new Guid("4C3DDF79-7FA6-4A2D-B750-8982F549FBE5");
        }

        public void InitNew()
        {

        }

        public IEnumerator Validate(object projectSystem)
        {
            return ValidationHelper.Validate(this, false).ToArray().GetEnumerator();
        }

        public bool Validate(out string errorMessage)
        {
            var errors = ValidationHelper.Validate(this, true).ToArray();

            if (errors.Any())
            {
                errorMessage = string.Join(",", errors);

                return false;
            }

            errorMessage = string.Empty;

            return true;
        }

        public IntPtr Icon { get { return IntPtr.Zero; } }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.CanRead & prop.CanWrite)
                {
                    object value = PropertyBagHelper.ReadPropertyBag(propertyBag, prop.Name) ?? prop.GetValue(this, null);
                    prop.SetValue(this, value, null);
                }
            }
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.CanRead & prop.CanWrite)
                {
                    PropertyBagHelper.WritePropertyBag(propertyBag, prop.Name, prop.GetValue(this, null));
                }
            }
        }
    }
}