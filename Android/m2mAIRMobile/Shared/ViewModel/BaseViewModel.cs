using System;

using Shared.Model;

namespace Shared.ViewModel
{
	public abstract class BaseViewModel
	{

		public BaseViewModel ()
		{
		}

		protected Shared.Model.Constants.VM_States GetVMState(string vm_state)
		{
			return (Shared.Model.Constants.VM_States)Enum.Parse(typeof(Shared.Model.Constants.VM_States), vm_state);
		}

	}
}

