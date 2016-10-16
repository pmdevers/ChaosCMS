using System;

namespace ChaosCMS.Razor
{
	public class ChaosRazorException : Exception
	{
		public ChaosRazorException()
		{
		}

		public ChaosRazorException(string message) : base(message) { }

		public ChaosRazorException(string message, Exception exception) : base(message, exception) { }
	}
}
