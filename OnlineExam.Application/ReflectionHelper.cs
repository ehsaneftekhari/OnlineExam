using System.Reflection;

namespace OnlineExam.Application
{
    internal class ReflectionHelper
    {
        public IEnumerable<(Type Impelimention, Type ContractInterface)> GetImplementationContractInterfaces(Type ContractInterfaceMarker)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsAssignableTo(ContractInterfaceMarker) && !t.IsInterface)
                .Select(t => (t, t.GetInterfaces().FirstOrDefault(it => it.IsAssignableTo(ContractInterfaceMarker))!));
        }

        public List<string> GetAttributeAssignedMethodsNames<TClassMarker, TAttribute>()
        {
            return GetAttributeAssignedMethodsNames(typeof(TClassMarker), typeof(TAttribute));
        }

        public List<string> GetAttributeAssignedMethodsNames(Type classMarkerType, Type attributeType)
        {
            return GetAttributeAssignedMethods(classMarkerType, attributeType)
                    .Select(m => string.Format("{0}.{1}", m.DeclaringType?.Name, m.Name))
                    .ToList();
        }

        public IEnumerable<MethodInfo> GetAttributeAssignedMethods<TClassMarker, TAttribute>()
        {
            return GetAttributeAssignedMethods(typeof(TClassMarker), typeof(TAttribute));
        }

        public IEnumerable<MethodInfo> GetAttributeAssignedMethods(Type classMarkerType, Type attributeType)
        {
            return GetAssignedTypes(classMarkerType)
                  .SelectMany(t => t.GetMethods())
                  .Where(mi => mi.GetCustomAttribute(attributeType) != null);
        }

        public IEnumerable<Type> GetAssignedTypes<TClassMarker>()
        {
            return GetAssignedTypes(typeof(TClassMarker));
        }

        public IEnumerable<Type> GetAssignedTypes(Type classMarkerType)
        {
            return Assembly.GetExecutingAssembly()
                   .GetTypes()
                   .Where(t => t.IsAssignableTo(classMarkerType) && !t.IsInterface);
        }
    }
}
