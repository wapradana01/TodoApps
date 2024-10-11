using System.ComponentModel;

namespace Todo.Api.Shared.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                if (attribute != null) return attribute.Description;
            }

            return value.ToString();
        }
    }
}
