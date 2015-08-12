using System;
using System.Collections.Generic;
using SQLite;
using Newtonsoft.Json;
using Shared.DB;
using Shared.Model;

namespace com.telit.lock_and_safe
{
    [Table("WatchedLock")]
    public class WatchedLock
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
                return gateway == null ? null : JsonConvert.SerializeObject(gateway);
            }
            set
            {
                gateway = value == null ? null : JsonConvert.DeserializeObject<Gateway>(value);
            }
        }

        [Ignore]
        public Location	  	loc				{ get; set; }

        [Ignore]
        public LockAlarms alarms { get; set; }

        
        public string RawLocation
        {
            get
            {
                return loc == null ? null : JsonConvert.SerializeObject(loc);
            }
            set
            {
                loc = value == null ? null : JsonConvert.DeserializeObject<Location>(value);
            }
        }

        //		public string data			{ get; set; }

        public WatchedLock()
        {
        }

        public WatchedLock(string id, string key, string name)
        {
            this.id = id;
            this.key = key;
            this.name = name;
        }
    }

    public class LockAlarms : Entity
    {
        public Alarm     reason   { get; set; }

        public Alarm     state    { get; set; }
    }
}
