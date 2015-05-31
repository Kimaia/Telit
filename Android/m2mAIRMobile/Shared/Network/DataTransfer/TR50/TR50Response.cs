using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network.DataTransfer.TR50
{

	public interface ITR50IsPayloadEmpty
	{
		bool IsPayloadEmpty ();
	}

	public class TR50Response<ParamsType> : ITR50IsPayloadEmpty where ParamsType : ITR50IsPayloadEmpty
	{
		public bool			success;
		public ParamsType 	Params;

		public bool IsPayloadEmpty ()
		{
			return (success && !Params.IsPayloadEmpty ());
		}
	}
		
	public class TR50ThingsListParams : ITR50IsPayloadEmpty
	{
		public int 				count;
		public List<string> 	fields;
		public List<Thing> 		result;

		public bool IsPayloadEmpty ()
		{
			return (count > 0);
		}
	}

	public class TR50ThingDefParams : ITR50IsPayloadEmpty
	{
		public string 			id;
		public string 			key;
		public string 			name;
		public int	 			version;
		public string 			createdBy;
		public string 			createdOn;
		public string 			updatedBy;
		public string 			updatedOn;
		public bool 			autoDefAttrs;
		public bool 			autoDefProps;
		public object 			alarms;
		public object 			attributes;
		public object 			methods;
		public object 			tunnels;
		public Dictionary<string, Property>	properties;

		public bool IsPayloadEmpty ()
		{
			return true;
		}
	}
}

