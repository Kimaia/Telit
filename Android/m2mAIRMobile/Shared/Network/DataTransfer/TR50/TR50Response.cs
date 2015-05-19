using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network.DataTransfer.TR50
{
	public class TR50Response<ParamsType>
	{
		public bool			success;
		public ParamsType 	Params;
	}

	public class TR50ThingsListParams
	{
		public int 				count;
		public List<string> 	fields;
		public List<Thing> 		result;
	}

	public class TR50ThingDefParams
	{
		public string 			id;
		public string 			key;
		public string 			name;
		public string 			version;
		public string 			createdBy;
		public string 			createdOn;
		public string 			updatedBy;
		public string 			updatedOn;
		public bool 			autoDefAttrs;
		public bool 			autoDefProps;
		public object 			properties;
		public object 			alarms;
		public object 			attributes;
		public object 			methods;
		public object 			tunnels;
	}
}

