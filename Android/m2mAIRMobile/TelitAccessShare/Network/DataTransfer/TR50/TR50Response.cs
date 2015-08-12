using System;
using System.Collections.Generic;

using Shared.Model;

namespace Shared.Network.DataTransfer.TR50
{
    public class TR50NullDataException : Exception
    {
        public TR50NullDataException(string message)
            : base(message)
        {
        }
    }

    public interface ITR50HasPayload
    {
        bool HasPayload();
    }

    public class TR50Response<ParamsType> : ITR50HasPayload where ParamsType : ITR50HasPayload
    {
        public bool success;
        public ParamsType Params;

        public bool HasPayload()
        {
            return (success && Params.HasPayload());
        }
    }

    public class TR50ThingsListParams : ITR50HasPayload
    {
        public int count;
        public List<string> fields;
        public List<Thing> result;

        public bool HasPayload()
        {
            return (count > 0);
        }
    }

    public class TR50ThingDefParams : ITR50HasPayload
    {
        public string id;
        public string key;
        public string name;
        public int version;
        public string createdBy;
        public string createdOn;
        public string updatedBy;
        public string updatedOn;
        public bool autoDefAttrs;
        public bool autoDefProps;
        public object alarms;
        public object attributes;
        public object methods;
        public object tunnels;
        public Dictionary<string, Property>	properties;

        public bool HasPayload()
        {
            return (key != null);
        }
    }

    public class TR50PropertyHistoryParams : ITR50HasPayload
    {
        public List<TR50PropertyValue>	values;

        public bool HasPayload()
        {
            return ((values != null) && (values.Count > 0));
        }
    }

    public class TR50PropertyValue : ITR50HasPayload
    {
        public int value;
        public string ts;

        public bool HasPayload()
        {
            return (ts != null);
        }
    }

    public class TR50LocationHistoryParams : ITR50HasPayload
    {
        public int sampling;
        public List<TR50LocationHistoryValue> values;

        public bool HasPayload()
        {
            return (values.Count > 0);
        }
    }

    public class TR50LocationHistoryValue : ITR50HasPayload
    {
        public string ts;
        public string	thingId;
        public string	corrId;
        public float lat;
        public float lng;
        public float fixAcc;
        public string fixType;
        public float heading;
        public float altitude;
        public float speed;
        public float delta;
        public Address	addr;

        public bool HasPayload()
        {
            return (ts != null);
        }
    }

    public class TR50AlarmValue : ITR50HasPayload
    {
        public int state;
        public int duration;
        public string msg;
        public string ts;

        public bool HasPayload()
        {
            return (ts != null);
        }
    }
}