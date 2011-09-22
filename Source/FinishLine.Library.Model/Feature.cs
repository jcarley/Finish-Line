using System.Collections.Generic;
using FinishLine.Library.Infrastructure.Domain;

namespace FinishLine.Library.Model
{
    public class Feature : WorkItemBase, IAggregateRoot, IIterationPlanItem
    {
        public IList<UserStory> Stories { get; set; }

        public override WorkItemType Type
        {
            get { return WorkItemType.Feature; }
        }
    }
}
