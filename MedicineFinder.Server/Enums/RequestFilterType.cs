using System.ComponentModel;

namespace MedicineFinder.Server.Enums
{
    public enum RequestFilterType
    {
        [Description("name")]
        Name,

        [Description("barCode")]
        Barcode
    }
}
