using System;
using System.Collections.Generic;
using FinishLine.Library.Infrastructure;
using FinishLine.Library.Infrastructure.Domain;
using FinishLine.Library.Model.Rules;

namespace FinishLine.Library.Model
{
    public abstract class IterationBase : EntityBase<Guid>, IAggregateRoot
    {
        private IList<IIterationPlanItem> _workItems = new List<IIterationPlanItem>();

        public Guid ID { get; set; }

        public string Name { get; set; }

        public IEnumerable<IIterationPlanItem> WorkItems
        {
            get { return _workItems; }
        }

        public void AddWorkItem(IIterationPlanItem workItem)
        {
            //TODO:  Throw exception is invalid workItem

            _workItems.Add(workItem);
        }

        protected override void Validate()
        {
            if (Name.Empty())
            {
                AddBrokenRule(IterationBusinessRules.RequiresName);
            }
        }
    }
}
