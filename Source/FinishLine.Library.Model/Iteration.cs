using System;
using FinishLine.Library.Infrastructure;
using FinishLine.Library.Model.Rules;

namespace FinishLine.Library.Model
{
    public class Iteration : IterationBase
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        protected override void Validate()
        {
            base.Validate();

            if (StartDate.Empty())
            {
                AddBrokenRule(IterationBusinessRules.RequiresStartDate); 
            }

            if (EndDate.Empty())
            {
                AddBrokenRule(IterationBusinessRules.RequiresEndDate);
            }

            if (StartDate > EndDate)
            {
                AddBrokenRule(IterationBusinessRules.EndDateGreaterThanStartDate);
            }
        }
    }
}
