using Newtonsoft.Json;
using System;

namespace SettingsAPIClient
{

    public class Setting
    {
        private string valuTypeName;
        private ValueDataType valuType;

        [JsonIgnore]
        public string Id { get { return string.Format("{0}{1}", ObjectId, Key).ToLower(); } }

        public int ObjectId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Info { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        [JsonProperty(PropertyName = "TypeInfo")]
        public string ValueTypeName
        {
            get { return TypeNameResolver.Resolve(valuType); }

            set
            {
                valuTypeName = value;
                valuType = TypeNameResolver.Resolve(valuTypeName);
            }
        }
        public ValueDataType ValueType
        {
            get { return TypeNameResolver.Resolve(valuTypeName); } 

            set
            {
                valuType = value;
                valuTypeName = TypeNameResolver.Resolve(valuType);
            }            
        }
    }
}