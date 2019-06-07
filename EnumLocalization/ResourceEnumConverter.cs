using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace EnumLocalization
{
    /// <summary>
    /// Defines a type converter for enum values that converts enum values to 
    /// and from string representations using resources
    /// </summary>
    /// <remarks>
    /// This class makes localization of display values for enums in a project easy.  Simply
    /// derive a class from this class and pass the ResourceManagerin the constructor.  
    /// 
    /// <code lang="C#" escaped="true" >
    /// class LocalizedEnumConverter : ResourceEnumConverter
    /// {
    ///    public LocalizedEnumConverter(Type type)
    ///        : base(type, Properties.Resources.ResourceManager)
    ///    {
    ///    }
    /// }    
    /// </code>
    /// 
    /// <code lang="Visual Basic" escaped="true" >
    /// Public Class LocalizedEnumConverter
    /// 
    ///    Inherits ResourceEnumConverter
    ///    Public Sub New(ByVal sType as Type)
    ///        MyBase.New(sType, My.Resources.ResourceManager)
    ///    End Sub
    /// End Class    
    /// </code>
    /// 
    /// Then define the enum values in the resource editor.   The names of
    /// the resources are simply the enum value prefixed by the enum type name with an
    /// underscore separator eg MyEnum_MyValue.  You can then use the TypeConverter attribute
    /// to make the LocalizedEnumConverter the default TypeConverter for the enums in your
    /// project.
    /// </remarks>
    public class ResourceEnumConverter : EnumConverter
    {
        private class LookupTable : Dictionary<string, object> { }
        private Dictionary<CultureInfo, LookupTable> _lookupTables = new Dictionary<CultureInfo, LookupTable>();
        private ResourceManager _resourceManager;
        private Array _flagValues;
        private bool IsFlagEnum => _flagValues != null;

        public ResourceEnumConverter(Type type, ResourceManager resourceManager) : base(type)
        {
            _resourceManager = resourceManager;
            _flagValues = type.GetCustomAttributes(typeof(FlagsAttribute), true).Length > 0 ? Enum.GetValues(type) : null;
        }

        /// <summary>
        /// Convert string values to enum values
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
                return base.ConvertFrom(context, culture, value);

            var result = (IsFlagEnum)
                ? GetFlagValue(culture, (string)value)
                : GetValue(culture, (string)value);

            return result ?? base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Convert the enum value to a string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType != typeof(string))
                return base.ConvertTo(context, culture, value, destinationType);

            object result = (IsFlagEnum)
                ? GetFlagValueText(culture, value)
                : GetValueText(culture, value);
            return result;

        }

        /// <summary>
        /// Return the Enum value for a simple (non-flagged enum)
        /// </summary>
        /// <param name="culture">The culture to convert using</param>
        /// <param name="text">The text to convert</param>
        /// <returns>The enum value</returns>
        private object GetValue(CultureInfo culture, string text)
        {
            var lookupTable = GetLookupTable(culture);
            lookupTable.TryGetValue(text, out object result);
            return result;
        }

        /// <summary>
        /// Return the Enum value for a flagged enum
        /// </summary>
        /// <param name="culture">The culture to convert using</param>
        /// <param name="text">The text to convert</param>
        /// <returns>The enum value</returns>
        private object GetFlagValue(CultureInfo culture, string text)
        {
            LookupTable lookupTable = GetLookupTable(culture);
            string[] textValues = text.Split(',');
            ulong result = 0;
            foreach (string textValue in textValues)
            {
                if (!lookupTable.TryGetValue(textValue.Trim(), out object value))
                    return null;
                result |= Convert.ToUInt32(value);
            }
            return Enum.ToObject(EnumType, result);
        }

        /// <summary>
        /// Get the lookup table for the given culture (creating if necessary)
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private LookupTable GetLookupTable(CultureInfo culture)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            if (!_lookupTables.TryGetValue(culture, out LookupTable result))
            {
                result = new LookupTable();
                foreach (var value in GetStandardValues())
                {
                    string text = GetValueText(culture, value);
                    if (text != null)
                    {
                        result.Add(text, value);
                    }
                }
                _lookupTables.Add(culture, result);
            }
            return result;
        }

        /// <summary>
        /// Return the text to display for a simple value in the given culture
        /// </summary>
        /// <param name="culture">The culture to get the text for</param>
        /// <param name="value">The enum value to get the text for</param>
        /// <returns>The localized text</returns>
        private string GetValueText(CultureInfo culture, object value)
        {
            string resourceName = $"{value.GetType().Name}_{value}";

            return _resourceManager.GetString(resourceName, culture) ?? resourceName;
        }

        /// <summary>
        /// Return the text to display for a flag value in the given culture
        /// </summary>
        /// <param name="culture">The culture to get the text for</param>
        /// <param name="value">The flag enum value to get the text for</param>
        /// <returns>The localized text</returns>
        private string GetFlagValueText(CultureInfo culture, object value)
        {
            // if there is a standard value then use it
            //
            if (Enum.IsDefined(value.GetType(), value))
                return GetValueText(culture, value);

            // otherwise find the combination of flag bit values
            // that makes up the value
            //
            ulong lValue = Convert.ToUInt32(value);
            string result = null;
            foreach (var flagValue in _flagValues)
            {
                ulong lFlagValue = Convert.ToUInt32(flagValue);
                if (IsSingleBitValue(lFlagValue) && (lFlagValue & lValue) == lFlagValue)
                {
                    string valueText = GetValueText(culture, flagValue);

                    result = result == null ? valueText : $"{result}, {valueText}";
                }
            }
            return result;
        }

        /// <summary>
        /// Return true if the given value is can be represented using a single bit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsSingleBitValue(ulong value)
        {
            switch (value)
            {
                case 0:
                    return false;
                case 1:
                    return true;
            }
            return (value & (value - 1)) == 0;
        }
    }
}
