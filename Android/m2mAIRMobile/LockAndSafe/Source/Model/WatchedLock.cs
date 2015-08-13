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
        public Location	  	loc				{ get; set; }

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

        
        
        public string alarms_json
        {
            get
            {
                return alarms == null ? null : JsonConvert.SerializeObject(alarms);
            }
            set
            {
                alarms = value == null ? null : JsonConvert.DeserializeObject<LockAlarms>(value);
            }
        }

        [Ignore]
        public LockAlarms alarms { get; set; }

        
        
        
        
        
        
        
        
        
        public string properties_json
        {
            get
            {
                return properties == null ? null : JsonConvert.SerializeObject(properties);
            }
            set
            {
                properties = value == null ? null : JsonConvert.DeserializeObject<LockProperties>(value);
            }
        }

        [Ignore]
        public LockProperties properties { get; set; }

        
        
        public WatchedLock()
        {
        }

        public WatchedLock(string id, string key, string name)
        {
            this.id = id;
            this.key = key;
            this.name = name;
        }

        public static string stateReason(int index)
        {
            switch (index)
            {
                case 0:
                    return   "No reason recorded";
                case 1:
                    return   "Response";
                case 2:
                    return   "Tracking";
                case 4:
                    return   "Event";
                case 6:
                    return   "Emergency";
                case 7:
                    return   "Low battery";
                case 8:
                    return   "Static pin IN";
                case 9:
                    return   "Static pin OUT";
                case 10:
                    return   "Strong Impact";
                case 11:
                    return   "Mobile pin IN";
                case 12:
                    return   "Mobile pin OUT";
                case 13:
                    return   "Weak impact";
                case 14:
                    return   "Location update";
                case 22:
                    return   "Closed";
                case 23:
                    return   "Opened";
                case 24:
                    return   "Maintenance";
                case 25:
                    return   "Break-in";
                case 29:
                    return   "Power on";
                case 30:
                    return   "Light off";
                case 31:
                    return   "Light on";
                case 32:
                    return   "Temperature low";
                case 33:
                    return   "Temperature high";
                case 38:
                    return   "Logging";
                default:
                    return   "-";
            }
        }

        
        
        
        public class LockAlarms
        {
            public AlarmResponse     reason   { get; set; }

            public AlarmResponse     state    { get; set; }
        }

        
        
        public class LockProperties
        {
            public PropertyResponse  voltage   { get; set; }
        }
    
    
    }
}
