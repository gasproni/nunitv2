#region Copyright (c) 2002-2004, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig
/************************************************************************************
'
' Copyright � 2002-2004 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' Copyright � 2000-2002 Philip A. Craig
'
' This software is provided 'as-is', without any express or implied warranty. In no 
' event will the authors be held liable for any damages arising from the use of this 
' software.
' 
' Permission is granted to anyone to use this software for any purpose, including 
' commercial applications, and to alter it and redistribute it freely, subject to the 
' following restrictions:
'
' 1. The origin of this software must not be misrepresented; you must not claim that 
' you wrote the original software. If you use this software in a product, an 
' acknowledgment (see the following) in the product documentation is required.
'
' Portions Copyright � 2002-2004 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' or Copyright � 2000-2003 Philip A. Craig
'
' 2. Altered source versions must be plainly marked as such, and must not be 
' misrepresented as being the original software.
'
' 3. This notice may not be removed or altered from any source distribution.
'
'***********************************************************************************/
#endregion

using System;
using System.Collections;

namespace NUnit.Core
{
	/// <summary>
	/// TestFixtureBuilder contains static methods for building
	/// TestFixtures from types. It uses the AddinManager and
	/// any installed ISuiteBuilder extensions to do it.
	/// </summary>
	public class TestFixtureBuilder
	{
		/// <summary>
		/// Build a test fixture from a given type.
		/// </summary>
		/// <param name="type">The type to be used for the fixture</param>
		/// <param name="assemblyKey">Integer indicating the assembly</param>
		/// <returns>A TestSuite if the fixture can be built, null if not</returns>
		public static TestSuite Make( Type type, int assemblyKey )
		{
			TestSuite suite = Addins.BuildFrom( type, assemblyKey );

			if ( suite == null )
				suite = Builtins.BuildFrom( type, assemblyKey );

			return suite;
		}

		/// <summary>
		/// Build a test fixture from a given type, using an assemblyKey of 0.
		/// </summary>
		/// <param name="type">The type to be used for the fixture</param>
		/// <returns>A TestSuite if the fixture can be built, null if not</returns>
		public static TestSuite Make( Type type )
		{
			return Make( type, 0 );
		}

		/// <summary>
		/// Build a fixture from an object. This method is used only
		/// for testing purposes.
		/// </summary>
		/// <param name="fixture">The object to be used for the fixture</param>
		/// <returns>A TestSuite if fixture type can be built, null if not</returns>
		public static TestSuite Make( object fixture )
		{
			TestSuite suite = Make( fixture.GetType() );
			suite.Fixture = fixture;
			return suite;
		}

		/// <summary>
		/// Private constructor to prevent instantiation
		/// </summary>
		private TestFixtureBuilder() { }
	}
}
