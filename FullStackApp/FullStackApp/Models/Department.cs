using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullStackApp.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int NoOfEmployees { get; set; }
        public int DepartmentType { get; set; }
        public bool HasDisabledEmployees { get; set; }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DepartmentType
    {
        IT,Business,Logistics,Management
    }
}