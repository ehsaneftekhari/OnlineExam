namespace OnlineExam.Application.AttributeManagers
{
    internal class MethodAttributeManager<TAttribute> where TAttribute : Attribute
    {
        readonly HashSet<string> methodsNames;
        internal MethodAttributeManager(Type classMarkerType, ReflectionHelper reflectionHelper)
        {
            methodsNames = new();

            this.classMarkerType = classMarkerType;

            reflectionHelper.GetAttributeMarkedMethodsNames(classMarkerType, typeof(TAttribute))
                .ForEach(m => methodsNames.Add(m));
        }

        public Type classMarkerType { get; private set; }

        public Type AttributeType => typeof(TAttribute);

        public bool HasMethod(string name)
        {
            return methodsNames.TryGetValue(name, out _);
        }
    }
}
