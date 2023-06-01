using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace Trackerino.App.Converters
{
    public class DateTimeToStringConverter : BaseConverterOneWay<DateTime, string>
    {
        public override string ConvertFrom(DateTime value, CultureInfo? culture)
        {
            if (value != DateTime.UnixEpoch)
            {
                return value.ToString("yyyy-MM-dd, HH:mm:ss", culture);
            }
            else
            {
                return "";
            }
        }

        public override string DefaultConvertReturnValue { get; set; } = string.Empty;
    }
}
