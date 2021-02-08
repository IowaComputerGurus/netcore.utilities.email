using ICG.NetCore.Utilities.Email;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        /// <summary>
        ///     Registers the items included in the ICG AspNetCore Utilities project for Dependency Injection
        /// </summary>
        /// <param name="services">Your existing services collection</param>
        /// <param name="configuration">The configuration instance to load settings</param>
        public static void UseIcgNetCoreUtilitiesEmail(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailTemplateSettings>(configuration.GetSection(nameof(EmailTemplateSettings)));
            services.AddTransient<IEmailTemplateFactory, EmailTemplateFactory>();
        }
    }
}