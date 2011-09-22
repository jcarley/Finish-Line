using System.Collections.Generic;
using FinishLine.Library.Infrastructure.Domain;

namespace FinishLine.Library.Model
{
    public class UserStory : WorkItemBase, IAggregateRoot, IIterationPlanItem
    {
        public IList<Task> Tasks { get; set; }

        public override WorkItemType Type
        {
            get { return WorkItemType.UserStory; }
        }
    }
}
