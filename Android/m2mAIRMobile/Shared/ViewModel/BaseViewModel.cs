﻿using System;

using Shared.Model;

namespace Shared.ViewModel
{
	public abstract class BaseViewModel
	{

		public delegate void OnError(string title, string message, int code, string dismissCaption);
		public delegate void OnSuccess();

		public BaseViewModel ()
		{
		}

		protected Shared.Model.Constants.VM_States GetVMState(string vm_state)
		{
			return (Shared.Model.Constants.VM_States)Enum.Parse(typeof(Shared.Model.Constants.VM_States), vm_state);
		}

	}
}

