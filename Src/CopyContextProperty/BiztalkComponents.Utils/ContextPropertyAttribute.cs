using System;

namespace BizTalkComponents.Utils
{
    public class ContextPropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}