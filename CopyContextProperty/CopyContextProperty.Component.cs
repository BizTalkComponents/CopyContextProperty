using System;
using System.Collections;

namespace BizTalkComponents.CopyContextProperty
{
    public partial class CopyContextProperty
    {
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
            return null;
        }

        public IntPtr Icon { get { return IntPtr.Zero; } }

    }
}