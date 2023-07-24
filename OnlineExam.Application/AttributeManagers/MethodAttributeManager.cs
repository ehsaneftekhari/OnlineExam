using System.Reflection;
using System.Xml.Linq;

namespace OnlineExam.Application.AttributeManagers
{
    internal class MethodAttributeManager<TAttribute> where TAttribute : Attribute
    {
        readonly HashSet<string> methodsNames;
        internal MethodAttributeManager(Type classMarkerType, ReflectionHelper reflectionHelper)
        {
            methodsNames = new();

            this.classMarkerType = classMarkerType;

            reflectionHelper.GetAttributeMarkedMethods(classMarkerType, AttributeType)
                    .Select(mi => GetMethodNameByMethodInfo(mi))
                    .ToList()
                    .ForEach(mn => methodsNames.Add(mn));
        }

        public Type classMarkerType { get; private set; }

        public Type AttributeType => typeof(TAttribute);

        public bool HasMethod(MethodInfo methodInfo) => HasMethod(GetMethodNameByMethodInfo(methodInfo));

        public bool HasMethod(string name) => methodsNames.TryGetValue(name, out _);

        private string GetMethodNameByMethodInfo(MethodInfo methodInfo) => string.Format("{0}.{1}", methodInfo.DeclaringType?.Name, methodInfo.Name);
    }
}
