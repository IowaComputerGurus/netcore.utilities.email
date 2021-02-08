using System.Collections.Generic;

namespace ICG.NetCore.Utilities.Email
{
    public class EmailTemplateSettings
    {
        public string DefaultTemplatePath { get; set; }
        public Dictionary<string, string> AdditionalTemplates { get; set; }
    }
}