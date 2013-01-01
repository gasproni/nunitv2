// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class AssignableToConstraintTests : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new AssignableToConstraint(typeof(D1));
            expectedDescription = string.Format("assignable to <{0}>", typeof(D1));
            stringRepresentation = string.Format("<assignableto {0}>", typeof(D1));
        }
        
        internal object[] SuccessData = new object[] { new D1(), new D2() };
            
        internal object[] FailureData = new object[] { new B() };

        internal string[] ActualValues = new string[]
            {
                "<NUnit.Framework.Constraints.AssignableToConstraintTests+B>"
            };

        class B { }

        class D1 : B { }

        class D2 : D1 { }
    }
}