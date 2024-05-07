using System.ComponentModel;

namespace MedicineFinder.Server.Enums.Extensions
{
    public static class RequestFilterTypeExtension
    {
        public static string GetDescription(this RequestFilterType parameter)
        {
            var description = parameter.ToString();
            var field = parameter.GetType().GetField(parameter.ToString());

            if (field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    is DescriptionAttribute[] attributes && attributes.Length != 0)
            {
                description = attributes.First().Description;
            }

            return description;
        }
    }
}
