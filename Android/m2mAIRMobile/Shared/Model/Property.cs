using System;
using SQLite;
using Newtonsoft.Json;
using Shared.DB;

namespace Shared.Model
{
	[Table("Property")]
	public class Property : EntityWithAssociations
	{

		[Unique]
		public string 	name		{ get; set; }
		public string 	unit		{ get; set; }

		public Property ()
		{
		}
	}
}

