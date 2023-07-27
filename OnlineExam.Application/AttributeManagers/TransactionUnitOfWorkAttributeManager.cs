using OnlineExam.Application.Attributes;
using OnlineExam.Application.Contract.Markers;

namespace OnlineExam.Application.AttributeManagers
{
    internal class TransactionUnitOfWorkAttributeManager : MethodAttributeManager<TransactionUnitOfWorkAttribute>
    {
        public TransactionUnitOfWorkAttributeManager(ReflectionHelper registrationReflectionHelper) : base(typeof(IApplicationContractMarker), registrationReflectionHelper) { }
    }
}
