using CommunityToolkit.Maui.Converters;
using System.Globalization;

namespace Trackerino.App.Converters
{
    public class TimeSpanToStringConverter : BaseConverterOneWay<TimeSpan, string>
    {
        public override string ConvertFrom(TimeSpan value, CultureInfo? culture)
        {
            return value.ToString(@"hh\:mm\:ss", culture);
        }

        public override string DefaultConvertReturnValue { get; set; } = string.Empty;
    }
}