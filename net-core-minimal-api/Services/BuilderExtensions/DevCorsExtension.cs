namespace net_core_minimal_api.Services.BuilderExtensions
{
    public static class DevCorsExtension
    {
        public static readonly string PolicyName = "AppCors";
        public static void AddDevCorsPolicy(this IHostApplicationBuilder builder)
        {
            var devOrigins = new[] { "http://localhost:3000", "https://localhost:3001" };
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, policy => policy.WithOrigins(devOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
         }
    }
}
