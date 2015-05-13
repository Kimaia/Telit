using System;
using SQLite;
using Newtonsoft.Json;
using Shared.DB;

namespace Shared.Model
{
	[Table("Thing")]
	public class Thing : EntityWithAssociations
	{

		[Unique]
		public string 	key				{ get; set; }

		public string 	id				{ get; set; }
		public string 	name			{ get; set; }
		public string 	type			{ get; set; }
		public bool  	connected		{ get; set; }
		public string 	connectedId		{ get; set; }
		public string 	connectedKey	{ get; set; }
		public string 	lastSeen		{ get; set; }
		public string 	defkey			{ get; set; }
		public string 	proto			{ get; set; }
		public string 	remoteAddr		{ get; set; }
		public string  	locUpdated		{ get; set; }

		[Ignore]
		public Array  	locWithin		{ get; set; }

		[Ignore]
		public Gateway 	gateway			{ get; set; }
		public string RawGateway
		{
			get
			{
				return gateway == null ? null : JsonConvert.SerializeObject (gateway);
			}
			set
			{
				gateway = value == null ? null : JsonConvert.DeserializeObject<Gateway> (value);
			}
		}

		[Ignore]
		public Location	  	loc				{ get; set; }
		public string RawLocation
		{
			get
			{
				return loc == null ? null : JsonConvert.SerializeObject (loc);
			}
			set
			{
				loc = value == null ? null : JsonConvert.DeserializeObject<Location> (value);
			}
		}

//		public string data			{ get; set; }

		public Thing ()
		{
		}

		public Thing (string id, string key, string name)
		{
			this.id = id;
			this.key = key;
			this.name = name;
		}
	}

	public class Location : Entity
	{
		public float 	lat 	 		{ get; set; }
		public float 	lng 	 		{ get; set; }
		public int 	 	acc		 		{ get; set; }
		public string 	fixType	 		{ get; set; }
		public Address	addr	 		{ get; set; }
	}

	public class Address : Entity
	{
		public string streetNumber 		{ get; set; }
		public string street 	 		{ get; set; }
		public string city		 		{ get; set; }
		public string state		 		{ get; set; }
		public string zipCode	 		{ get; set; }
		public string country	 		{ get; set; }
	}

	public class Gateway : Entity
	{
		public string make		 		{ get; set; }
		public string model 	 		{ get; set; }
		public string dwProduct	 		{ get; set; }
		public string dwPlatform 		{ get; set; }
		public string dwVersion 		{ get; set; }
		public string appVersion 		{ get; set; }
		public bool   remShell	 		{ get; set; }
	}
}
