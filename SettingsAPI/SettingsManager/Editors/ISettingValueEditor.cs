using SettingsAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsManager.Editors
{
    public interface ISettingValueEditor
    {
        string Value { get; set; }
        ValueDataType ValueType { get; }
        bool ValidateValue();
        bool ValidateValue(string value);
        string ValidationMessage { get;}
    }
}
