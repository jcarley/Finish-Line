using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinishLine.Library.Model;
using FinishLine.Library.Model.Rules;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;
using FinishLine.Library.Infrastructure.Domain;


namespace FinishLine.Tests.Model
{
    [TestFixture]
    public class when_adding_a_new_valid_iteration : SpecificationBase
    {
        private IRepository _repository = null;
        private Iteration _iteration = null;

        protected override void Given()
        {
            _repository = MockRepository.GenerateMock<IRepository>();

            _iteration = new Iteration()
            {
                Name = "Iteration 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14)
            };
        }

        protected override void When()
        {
            _repository.Insert(_iteration);
        }

        [Then]
        public void it_should_be_added_successfully()
        {
            _repository.ShouldHaveBeenCalled(repo => repo.Insert(_iteration));
        }

        [Then]
        public void it_should_require_a_name()
        {
            _iteration.GetBrokenRules().ShouldNotContain(IterationBusinessRules.RequiresName);
        }

        [Then]
        public void it_should_require_a_start_date()
        {
            _iteration.GetBrokenRules().ShouldNotContain(IterationBusinessRules.RequiresStartDate);
        }

        [Then]
        public void it_should_require_an_end_date()
        {
            _iteration.GetBrokenRules().ShouldNotContain(IterationBusinessRules.RequiresEndDate);
        }

        [Then]
        public void it_should_have_a_greater_end_date_than_start_date()
        {
            _iteration.GetBrokenRules().ShouldNotContain(IterationBusinessRules.EndDateGreaterThanStartDate);
        }
    }
}
