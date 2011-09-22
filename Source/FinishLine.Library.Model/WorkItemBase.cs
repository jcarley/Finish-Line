using System;

namespace FinishLine.Library.Model
{
    public abstract class WorkItemBase
    {
        public Guid ID { get; set; }
        
        public string Name { get; set; }
        
        public int Rank { get; set; }
        
        public PriorityLevel Priority { get; set; }
        
        public int Effort { get; set; }
        
        public int TimeSpent { get; set; }
        
        public int TimeRemaining { get; set; }
        
        public string AssignedAs { get; set; }

        public WorkItemState Status { get; private set; }
        
        public abstract WorkItemType Type { get; }


        public void ChangeStatus(State status)
        {
            if (Status == null || Status.IsAllow(status))
            {
                Status = WorkItemStateFactory.GetState(status);
            }
        }
    }
}
