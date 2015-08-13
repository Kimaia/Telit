using System;
using SQLite;
using Newtonsoft.Json;
using Shared.DB;

namespace Shared.Model
{
    [Table("Alarm")]
    public class AlarmResponse
    {
        public int     state   { get; set; }

        public int     duration{ get; set; }

        public string  msg     { get; set; }

        public string  ts      { get; set; }

        public AlarmResponse()
        {
        }
    }
}
