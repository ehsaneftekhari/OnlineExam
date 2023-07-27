using System.Reflection;

namespace OnlineExam.Application
{
    internal class ReflectionHelper
    {
        internal IEnumerable<(Type Impelimention, Type ContractInterface)> GetImplementationContractInterfaces(Type ContractInterfaceMarker)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsAssignableTo(ContractInterfaceMarker) && !t.IsInterface)
                .Select(t => (t, t.GetInterfaces().FirstOrDefault(it => it.IsAssignableTo(ContractInterfaceMarker))!));
        }

        internal IEnumerable<MethodInfo> GetAttributeMarkedMethods<TClassMarker, TAttribute>()
        {
            return GetAttributeMarkedMethods(typeof(TClassMarker), typeof(TAttribute));
        }

        internal IEnumerable<MethodInfo> GetAttributeMarkedMethods(Type classMarkerType, Type attributeType)
        {
            return GetMarkedTypes(classMarkerType)
                  .SelectMany(t => t.GetMethods())
                  .Where(mi => MethodHasAttribute(mi, attributeType));
        }

        internal IEnumerable<Type> GetMarkedTypes<TClassMarker>()
        {
            return GetMarkedTypes(typeof(TClassMarker));
        }

        internal IEnumerable<Type> GetMarkedTypes(Type classMarkerType)
        {
            return Assembly.GetExecutingAssembly()
                   .GetTypes()
                   .Where(t => t.IsAssignableTo(classMarkerType) && !t.IsInterface);
        }

        internal bool ClassHasAttribute(Type targetClassType, Type attributeType)
        {
            if (attributeType == null && attributeType == null)
                throw new ArgumentNullException();

            if (targetClassType.IsInterface)
                throw new ArgumentException($"{nameof(targetClassType)} should not be Interface");

            return targetClassType.GetMethods().Any(m => MethodHasAttribute(m, attributeType));
        }

        internal bool MethodHasAttribute(MethodInfo targetMethod, Type attributeType) 
        {
            if (!attributeType.IsAssignableTo(typeof(Attribute)))
                throw new ArgumentException($"{nameof(attributeType)} should be assignable to {typeof(Attribute)}");

            return targetMethod.GetCustomAttribute(attributeType) != null;
        }
    }
}
