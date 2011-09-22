using System;
using System.Collections.Generic;
using FinishLine.Library.Infrastructure.Domain;

namespace FinishLine.Library.Model.Rules
{
    public class IterationBusinessRules
    {
        public static BusinessRule RequiresName = new BusinessRule("Name", "Name is required.");
        public static BusinessRule RequiresStartDate = new BusinessRule("StartDate", "Start date is required.");
        public static BusinessRule RequiresEndDate = new BusinessRule("EndDate", "End date is required.");
        public static BusinessRule EndDateGreaterThanStartDate = new BusinessRule("EndDate", "End date must be greater than start date.");

    }
}
