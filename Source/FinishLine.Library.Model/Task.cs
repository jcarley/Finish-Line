using FinishLine.Library.Infrastructure.Domain;

namespace FinishLine.Library.Model
{
    public class Task : WorkItemBase, IAggregateRoot
    {
        public Task()
        {
            ChangeStatus(State.New);
        }

        public override WorkItemType Type
        {
            get { return WorkItemType.Task; }
        }
    }
}