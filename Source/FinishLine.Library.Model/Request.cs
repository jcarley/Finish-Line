
namespace FinishLine.Library.Model
{
    public class Request : WorkItemBase
    {
        public override WorkItemType Type
        {
            get { return WorkItemType.Request; }
        }
    }
}
