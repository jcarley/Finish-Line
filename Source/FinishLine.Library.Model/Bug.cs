using FinishLine.Library.Infrastructure.Domain;

namespace FinishLine.Library.Model
{
    public class Bug : WorkItemBase, IAggregateRoot, IIterationPlanItem
    {
        public override WorkItemType Type
        {
            get { return WorkItemType.Bug; }
        }
    }
}
