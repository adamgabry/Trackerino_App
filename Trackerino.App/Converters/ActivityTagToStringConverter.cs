using CommunityToolkit.Maui.Converters;
using Trackerino.DAL.Common;
using System.Globalization;

namespace Trackerino.App.Converters;

public class ActivityTagToStringConverter : BaseConverterOneWay<ActivityTag, string>
{
    public override string ConvertFrom(ActivityTag value, CultureInfo? culture)
        => value.ToString();

    public override string DefaultConvertReturnValue { get; set; } = "None";
}