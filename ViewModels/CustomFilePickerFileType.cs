using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.ViewModels
{
    public static class CustomFilePickerFileType
    {
        public static FilePickerFileType Csv { get; } = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
            { DevicePlatform.iOS, new[] { "public.comma-separated-values-text" } },
            { DevicePlatform.Android, new[] { "text/csv" } },
            { DevicePlatform.WinUI, new[] { ".csv" } },
            { DevicePlatform.Tizen, new[] { "text/csv" } },
            { DevicePlatform.macOS, new[] { "public.comma-separated-values-text" } },
            });
    }

}
