using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace Trackerino.App.Converters
{
    public class DateTimeToStringConverter : BaseConverterOneWay<DateTime, string>
    {
        public override string ConvertFrom(DateTime value, CultureInfo? culture)
        {
            return value.ToString("yyyy-MM-dd, HH:mm", culture);
        }

        public override string DefaultConvertReturnValue { get; set; } = string.Empty;
    }
}
