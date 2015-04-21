using System;

namespace Shared.Model
{
    public class UserDetails
	{
		public string username 	 		{ get; set; }
		public string password   		{ get; set; }
		public string sessionId			{ get; set; }


		public UserDetails(string username, string password, string sessionId)
		{
			this.username = username;
			this.password = password;
			this.sessionId = sessionId;
		}

	}
}