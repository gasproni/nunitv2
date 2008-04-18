using System;
using NUnit.Framework.Constraints;
using NUnit.Framework.Syntax.CSharp;

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class AssertThrowsTests : MessageChecker
	{
		[Test]
		public void CorrectExceptionThrown()
		{
#if NET_2_0
            Assert.Throws(typeof(ArgumentException), ThrowsArgumentException);
            Assert.Throws(typeof(ArgumentException),
                delegate { throw new ArgumentException(); });

            Assert.That(ThrowsArgumentException,
                Throws.Exception<ArgumentException>());

            Assert.Throws<ArgumentException>(
                delegate { throw new ArgumentException(); });
			Assert.Throws<ArgumentException>( ThrowsArgumentException );
		    Assert.That(ThrowsArgumentException,
		                Throws.Exception(typeof (ArgumentException),
		                    Has.Property("Parameter").EqualTo("myParam")));

#else
			Assert.Throws(typeof(ArgumentException),
				new TestDelegate( ThrowsArgumentException ) );

            Assert.That( new TestDelegate( ThrowsArgumentException ),  
                Throws.Exception(typeof(ArgumentException)));
#endif
        }

		[Test]
		public void CorrectExceptionIsReturnedToMethod()
		{
			Exception ex = Assert.Throws(typeof(ArgumentException),
				new TestDelegate( ThrowsArgumentException ) );

			Assert.IsNotNull( ex );
			Assert.AreEqual( typeof(ArgumentException), ex.GetType() );
			Assert.AreEqual( "myMessage" + Environment.NewLine + "Parameter name: myParam", ex.Message );

#if NET_2_0
            ex = Assert.Throws<ArgumentException>(
                delegate { throw new ArgumentException("myMessage", "myParam"); });

			Assert.IsNotNull( ex );
			Assert.AreEqual( typeof(ArgumentException), ex.GetType() );
			Assert.AreEqual( "myMessage" + Environment.NewLine + "Parameter name: myParam", ex.Message );

			ex = Assert.Throws(typeof(ArgumentException), 
                delegate { throw new ArgumentException("myMessage", "myParam"); } );

			Assert.IsNotNull( ex );
			Assert.AreEqual( typeof(ArgumentException), ex.GetType() );
			Assert.AreEqual( "myMessage" + Environment.NewLine + "Parameter name: myParam", ex.Message );

			ex = Assert.Throws<ArgumentException>( ThrowsArgumentException );

			Assert.IsNotNull( ex );
			Assert.AreEqual( typeof(ArgumentException), ex.GetType() );
			Assert.AreEqual( "myMessage" + Environment.NewLine + "Parameter name: myParam", ex.Message );
#endif
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void NoExceptionThrown()
		{
			expectedMessage =
				"  Expected: <System.ArgumentException>" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
#if NET_2_0
			Assert.Throws<ArgumentException>( ThrowsNothing );
#else
			Assert.Throws( typeof(ArgumentException),
				new TestDelegate( ThrowsNothing ) );
#endif
		}

        [Test, ExpectedException(typeof(AssertionException))]
        public void UnrelatedExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.ArgumentException>" + Environment.NewLine +
                "  But was:  <System.ApplicationException>" + Environment.NewLine;
#if NET_2_0
			Assert.Throws<ArgumentException>(ThrowsApplicationException);
#else
			Assert.Throws( typeof(ArgumentException),
				new TestDelegate(ThrowsApplicationException) );
#endif
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void BaseExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.ArgumentException>" + Environment.NewLine +
                "  But was:  <System.Exception>" + Environment.NewLine;
#if NET_2_0
			Assert.Throws<ArgumentException>(ThrowsException);
#else
            Assert.Throws( typeof(ArgumentException),
                new TestDelegate( ThrowsException) );
#endif
        }

        [Test,ExpectedException(typeof(AssertionException))]
        public void DerivedExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.Exception>" + Environment.NewLine +
                "  But was:  <System.ArgumentException>" + Environment.NewLine;
#if NET_2_0
			Assert.Throws<Exception>(ThrowsArgumentException);
#else
            Assert.Throws( typeof(Exception),
				new TestDelegate( ThrowsArgumentException) );
#endif
        }

        [Test]
        public void DoesNotThrowSuceeds()
        {
#if NET_2_0
            Assert.DoesNotThrow(ThrowsNothing);
#else
            Assert.DoesNotThrow( new TestDelegate( ThrowsNothing ) );

			Assert.That( new TestDelegate( ThrowsNothing ), Throws.Nothing );
#endif
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void DoesNotThrowFails()
        {
#if NET_2_0
            Assert.DoesNotThrow(ThrowsArgumentException);
#else
            Assert.DoesNotThrow( new TestDelegate( ThrowsArgumentException ) );
#endif
        }

        #region Methods Called by Tests
        static void ThrowsArgumentException()
		{
			throw new ArgumentException("myMessage", "myParam");
		}

        static void ThrowsApplicationException()
		{
			throw new ApplicationException();
		}

        static void ThrowsException()
		{
			throw new Exception();
		}

        static void ThrowsNothing()
		{
        }
        #endregion
    }
}
