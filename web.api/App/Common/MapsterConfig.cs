using Mapster;

namespace web.api.App.Common
{
    public static class MapsterConfig
    {
        public static TypeAdapterConfig Ignore(string fieldName)
        {
            var config = TypeAdapterConfig.GlobalSettings.Clone();
            config.Default.Ignore("TeamId");
            return config;
        }

        public static TypeAdapterConfig Ignore(string[] fieldNames)
        {
            var config = TypeAdapterConfig.GlobalSettings.Clone();
            foreach (var field in fieldNames)
            {
                config.Default.Ignore(field);
            }

            return config;
        }
    }
}