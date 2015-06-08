using System;

namespace Shared.ViewModel
{
	public abstract class BaseViewModel
	{

		public delegate void OnError(string title, string message);
		public delegate void OnSuccess();

		public BaseViewModel ()
		{
		}
	}
}

