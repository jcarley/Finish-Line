using NUnit.Framework;

namespace FinishLine.Tests
{
    [TestFixture]
    public class SpecificationBase
    {
        [SetUp]
        public void Setup()
        {
            Given();
            When();
        }
        
        protected virtual void Given()
        {
            
        }

        protected virtual void When()
        {
            
        }
    }
}