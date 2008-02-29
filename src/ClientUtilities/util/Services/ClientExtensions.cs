using System;
using System.Collections;
using NUnit.Core;
using NUnit.Core.Extensibility;
using NUnit.Util.Extensibility;
using NUnit.Util.ProjectConverters;

namespace NUnit.Util
{
	/// <summary>
	/// ClientExtensions is a service that groups together all 
	/// the extension points that are supported in the client domain.
	/// </summary>
	public class ClientExtensions : ExtensionHost, IService
	{
		#region Instance Fields
		VisualStudioConverter vsConverter;

		#endregion

		#region Constructor
		public ClientExtensions()
		{
			vsConverter = new VisualStudioConverter();

			extensions = new IExtensionPoint[] { new ProjectConverterCollection() };
		}
		#endregion

		#region Public Methods
		public void InstallBuiltins()
		{
			// Define NUnit Framework
			FrameworkRegistry.Register( "NUnit", "nunit.framework" );

			// Install builtin SuiteBuilders - Note that the
			// NUnitTestCaseBuilder is installed whenever
			// an NUnitTestFixture is being populated and
			// removed afterward.
			//Install( new VisualStudioConverter() );
		}
		#endregion

		#region IService Members

		public void UnloadService()
		{
			// TODO:  Add ClientExtensions.UnloadService implementation
		}

		public void InitializeService()
		{
			InstallBuiltins();
		}

		#endregion
	}
}
