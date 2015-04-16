using System;
using System.Collections;
using System.Linq;
using BizTalkComponents.Utils;

namespace BizTalkComponents.PipelineComponents.CopyContextProperty
{
    public partial class CopyContextProperty
    {
        public string Name { get { return "CopyContextProperty"; } }
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

    }
}