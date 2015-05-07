﻿using System;
using SQLite;

namespace Shared.Model
{
	[Table("thing")]
	public class Thing
	{

		public string 	id				{ get; set; }
		public string 	key				{ get; set; }
		public string 	name			{ get; set; }
		public string 	type			{ get; set; }
		public bool  	connected		{ get; set; }
		public string 	connectedId		{ get; set; }
		public string 	connectedKey	{ get; set; }
		public string 	lastSeen		{ get; set; }
		public string 	defkey			{ get; set; }
		public object 	gateway			{ get; set; }
		public string 	proto			{ get; set; }
		public string 	remoteAddr		{ get; set; }
		public object  	locWithin		{ get; set; }
		public string  	locUpdated		{ get; set; }
		public object  	loc				{ get; set; }
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
}
