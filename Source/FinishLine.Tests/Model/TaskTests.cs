using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinishLine.Library.Model;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;
using FinishLine.Library.Infrastructure.Domain;

namespace FinishLine.Tests.Model
{
    [TestFixture]
    public class when_adding_a_new_task : SpecificationBase
    {
        private IRepository _repository = null;
        private Task _task = null;

        protected override void Given()
        {
            _repository = MockRepository.GenerateMock<IRepository>();

            _task = new Task()
            {
                ID = Guid.NewGuid(),
                Name = "New Task",
                Priority = PriorityLevel.Low,
                Rank = 0
            };
        }

        protected override void When()
        {
            _repository.Insert(_task);
        }

        [Then]
        public void it_should_be_added_successfully()
        {
            _repository.ShouldHaveBeenCalled(repo => repo.Insert(_task));
        }

        [Then]
        public void it_should_have_a_new_status()
        {
            _task.Status.Value.ShouldBe(State.New);
        }
    }

    [TestFixture]
    public class when_retrieving_a_new_task : SpecificationBase
    {
        protected override void Given()
        {
            
        }

        protected override void When()
        {
            
        }


    }
}
