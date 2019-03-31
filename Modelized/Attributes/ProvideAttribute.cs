using System;

namespace Modelized.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ProvideAttribute : Attribute
    {
        public Type ProviderType { get; }

        public ProvideAttribute(Type providerType)
        {
            ProviderType = providerType;
        }
    }
}
