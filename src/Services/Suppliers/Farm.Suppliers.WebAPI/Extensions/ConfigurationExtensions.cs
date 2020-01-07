using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Farm.Suppliers.WebAPI
{
    public class CorsConfig
    {
        public string[] Origins { get; set; }
        public string[] Methods { get; set; }
        public string[] Headers { get; set; }
    }

    public class IamConfig
    {
        public string AuthUrl { get; set; }

        public string OpenIdConnectAuthUrl { get; set; }
        public string OpenIdConnectTokenUrl { get; set; }
    }

    public static class ConfigurationExtensions
    {
        private static string POSTGRESQL_ENDPOINT = "POSTGRESQL_ENDPOINT";
        private static string POSTGRESQL_DATABASE = "POSTGRESQL_DATABASE";
        private static string POSTGRESQL_PORT = "POSTGRESQL_PORT";
        private static string POSTGRESQL_USER = "POSTGRESQL_USER";
        private static string POSTGRESQL_PASSWORD = "POSTGRESQL_PASSWORD";
        private static string CORS_ORIGIN = "CORS_ORIGIN";
        private static string CORS_METHOD = "CORS_METHOD";
        private static string CORS_HEADER = "CORS_HEADER";
        private static string IAM_AUTH_URL = "IAM_AUTH_URL";

        private static IDictionary<string, string> defaultValues = new Dictionary<string, string>
        {
            {POSTGRESQL_ENDPOINT, "192.168.56.1" },
            {POSTGRESQL_DATABASE, "mix" },
            {POSTGRESQL_PORT, "5432" },
            {POSTGRESQL_USER, "postgres" },
            {POSTGRESQL_PASSWORD, "postgres" },
            {CORS_ORIGIN, "*" },
            {CORS_METHOD, "*" },
            {CORS_HEADER, "*" },
            {IAM_AUTH_URL, "http://192.168.56.1:8080/auth/realms/master" }
        };

        public static string GetConnectionPostgreSQLString(this IConfiguration configuration)
        {
            return $"Host = {GetValue(configuration, POSTGRESQL_ENDPOINT, defaultValues[POSTGRESQL_ENDPOINT])};" +
                $" Port = {GetValue(configuration, POSTGRESQL_PORT, defaultValues[POSTGRESQL_PORT])};" +
                $" Database = {GetValue(configuration, POSTGRESQL_DATABASE, defaultValues[POSTGRESQL_DATABASE])};" +
                $" Username = {GetValue(configuration, POSTGRESQL_USER, defaultValues[POSTGRESQL_USER])};" +
                $" Password = {GetValue(configuration, POSTGRESQL_PASSWORD, defaultValues[POSTGRESQL_PASSWORD])}";
        }

        public static CorsConfig GetCorsConfig(this IConfiguration configuration)
        {
            CorsConfig conf = new CorsConfig();
            conf.Origins = GetValue(configuration, CORS_ORIGIN, defaultValues[CORS_ORIGIN]).Split(',');
            conf.Methods = GetValue(configuration, CORS_METHOD, defaultValues[CORS_METHOD]).Split(',');
            conf.Headers = GetValue(configuration, CORS_HEADER, defaultValues[CORS_HEADER]).Split(',');
            return conf;
        }

        public static IamConfig GetIamConfig(this IConfiguration configuration)
        {
            IamConfig conf = new IamConfig();
            conf.AuthUrl = GetValue(configuration, IAM_AUTH_URL, defaultValues[IAM_AUTH_URL]);
            conf.OpenIdConnectAuthUrl = Path.Combine(GetValue(configuration, IAM_AUTH_URL, defaultValues[IAM_AUTH_URL]), "protocol/openid-connect/auth");
            conf.OpenIdConnectTokenUrl = Path.Combine(GetValue(configuration, IAM_AUTH_URL, defaultValues[IAM_AUTH_URL]), "protocol/openid-connect/token");
            return conf;
        }

        private static string GetValue(IConfiguration configuration, string key, string defaultValue)
        {
            return configuration.GetValue<string>(key) ?? defaultValue;
        }
    }
}
