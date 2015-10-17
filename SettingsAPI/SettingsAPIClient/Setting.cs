using Newtonsoft.Json;

namespace SettingsAPIClient
{

    public class Setting
    {
        private string valuTypeName;
        private ValueDataType valuType;

        public int ObjectId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string Info { get; set; }

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