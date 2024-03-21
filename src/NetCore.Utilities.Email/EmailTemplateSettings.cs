using System.Collections.Generic;

namespace ICG.NetCore.Utilities.Email;

/// <summary>
///     Setting object for email templates
/// </summary>
public class EmailTemplateSettings
{
    /// <summary>
    ///     The application relative file path to the default template file
    /// </summary>
    public string DefaultTemplatePath { get; set; }

    /// <summary>
    ///     A collection of additional templates with a "name" and "path" for each one
    /// </summary>
    public Dictionary<string, string> AdditionalTemplates { get; set; }
}