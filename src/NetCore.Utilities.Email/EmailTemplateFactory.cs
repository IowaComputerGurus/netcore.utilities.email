using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ICG.NetCore.Utilities.Email;

/// <summary>
///     Factory class for creating email content, using templates.
/// </summary>
public interface IEmailTemplateFactory
{
    /// <summary>
    ///     Creates HTML email content utilizing a template
    /// </summary>
    /// <param name="subject">The desired subject of the email</param>
    /// <param name="content">The desired content of the email, HTML formatted.</param>
    /// <param name="preview">
    ///     An optional preview textual email content element, replaced in the template for more advanced
    ///     control
    /// </param>
    /// <param name="templateName">The name of the template to use, rather than the initial template.</param>
    /// <returns>The updated content wrapped in the template</returns>
    /// <exception cref="ArgumentNullException">Thrown for missing subject or content</exception>
    /// <exception cref="ArgumentException">Thrown if requesting a template that is not defined</exception>
    /// <exception cred="FileNotFoundException">Thrown if the defined template file does not exist</exception>
    string BuildEmailContent(string subject, string content, string preview = "", string templateName = "");
}

/// <inheritdoc cref="IEmailTemplateFactory" />
public class EmailTemplateFactory : IEmailTemplateFactory
{
    private readonly IHostEnvironment _hostingEnvironment;
    private readonly IOptions<EmailTemplateSettings> _templateSettings;

    /// <summary>
    ///     Default constructor with injected dependencies
    /// </summary>
    /// <param name="templateSettings"></param>
    /// <param name="hostingEnvironment"></param>
    public EmailTemplateFactory(IOptions<EmailTemplateSettings> templateSettings, IHostEnvironment hostingEnvironment)
    {
        _templateSettings = templateSettings;
        _hostingEnvironment = hostingEnvironment;
    }

    /// <inheritdoc cref="IEmailTemplateFactory" />
    public string BuildEmailContent(string subject, string content, string preview = "", string templateName = "")
    {
        //Validate inputs
        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentNullException(nameof(subject));
        }

        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentNullException(nameof(content));
        }

        if (!string.IsNullOrEmpty(templateName) &&
            !_templateSettings.Value.AdditionalTemplates.ContainsKey(templateName))
        {
            throw new ArgumentException($"Requested template {templateName} was not found in configuration",
                nameof(templateName));
        }

        //Get the template
        var templatePath = string.IsNullOrEmpty(templateName)
            ? _templateSettings.Value.DefaultTemplatePath
            : _templateSettings.Value.AdditionalTemplates[templateName];
        var fullTemplatePath = Path.Combine(_hostingEnvironment.ContentRootPath, templatePath);
        if (!File.Exists(fullTemplatePath))
        {
            throw new FileNotFoundException("Unable to find template file", fullTemplatePath);
        }

        //Replace the content
        var templateBuilder =
            new StringBuilder(File.ReadAllText(fullTemplatePath));
        templateBuilder.Replace("[SUBJECT]", subject);
        templateBuilder.Replace("[PREVIEW]", preview);
        templateBuilder.Replace("[CONTENT]", content);

        //Return message content
        return templateBuilder.ToString();
    }
}