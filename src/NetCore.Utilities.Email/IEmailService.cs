using System.Collections.Generic;

namespace ICG.NetCore.Utilities.Email
{
    /// <summary>
    ///     Represents a service that can deliver email messages to the recipients.  This is utilized by all downstream
    ///     implementations of ICG.NetCore.Utilities.Email applications allowing easy switching between providers & options.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        ///     Returns the configured administrator email for the service
        /// </summary>
        string AdminEmail { get; }

        /// <summary>
        ///     Returns the configured administrator name for outbound emails
        /// </summary>
        string AdminName { get; }

        /// <summary>
        ///     Shortcut for sending an email to the administrator, only requiring the subject and body.
        /// </summary>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        bool SendMessageToAdministrator(string subject, string bodyHtml);

        /// <summary>
        ///     Sends a message to the administrator as well as the additional contacts provided.
        /// </summary>
        /// <param name="ccAddressList">Additional email addresses to add to the CC line</param>
        /// <param name="subject">The email subject</param>
        /// <param name="bodyHtml">The HTML content of the email</param>
        bool SendMessageToAdministrator(IEnumerable<string> ccAddressList, string subject, string bodyHtml);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        bool SendMessage(string toAddress, string subject, string bodyHtml);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        bool SendMessage(string toAddress, string subject, string bodyHtml, List<KeyValuePair<string, string>> tokens);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="ccAddressList">Additional CC'ed emails</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        bool SendMessage(string toAddress, IEnumerable<string> ccAddressList, string subject, string bodyHtml);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="ccAddressList">Additional CC'ed emails</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        bool SendMessage(string toAddress, IEnumerable<string> ccAddressList, string subject, string bodyHtml, List<KeyValuePair<string, string>> tokens);

        /// <summary>
        ///     Sends a message to the specified recipient, and CC's with the supplied subject and body
        /// </summary>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="ccAddressList">Additional CC'ed emails</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        /// <param name="templateName">The optional custom template to override with</param>
        /// <param name="senderKeyName">The a custom key for identifying a sender</param>
        bool SendMessage(string toAddress, IEnumerable<string> ccAddressList, string subject, string bodyHtml, List<KeyValuePair<string, string>> tokens,
            string templateName, string senderKeyName = "");

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="replyToAddress">The address to be used as a reply to</param>
        /// <param name="replyToName">The address to be used as a reply to</param>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        bool SendWithReplyTo(string replyToAddress, string replyToName, string toAddress, string subject, string bodyHtml);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="replyToAddress">The address to be used as a reply to</param>
        /// <param name="replyToName">The address to be used as a reply to</param>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        bool SendWithReplyTo(string replyToAddress, string replyToName, string toAddress, string subject, string bodyHtml, List<KeyValuePair<string, string>> tokens);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="replyToAddress">The address to be used as a reply to</param>
        /// <param name="replyToName">The address to be used as a reply to</param>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="ccAddressList">Additional CC'ed emails</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        bool SendWithReplyTo(string replyToAddress, string replyToName, string toAddress, IEnumerable<string> ccAddressList, string subject, string bodyHtml);

        /// <summary>
        ///     Sends a message to the specified recipient, with the supplied subject and body
        /// </summary>
        /// <param name="replyToAddress">The address to be used as a reply to</param>
        /// <param name="replyToName">The address to be used as a reply to</param>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="ccAddressList">Additional CC'ed emails</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        bool SendWithReplyTo(string replyToAddress, string replyToName, string toAddress, IEnumerable<string> ccAddressList, string subject, string bodyHtml, List<KeyValuePair<string, string>> tokens);

        /// <summary>
        ///     Sends a message to the specified recipient, and CC's with the supplied subject and body
        /// </summary>
        /// <param name="replyToAddress">The address to be used as a reply to</param>
        /// <param name="replyToName">The address to be used as a reply to</param>
        /// <param name="toAddress">Who is receiving the email</param>
        /// <param name="ccAddressList">Additional CC'ed emails</param>
        /// <param name="subject">The message subject</param>
        /// <param name="bodyHtml">The message body</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        /// <param name="templateName">The optional custom template to override with</param>
        /// <param name="senderKeyName">The a custom key for identifying a sender</param>
        bool SendWithReplyTo(string replyToAddress, string replyToName, string toAddress, IEnumerable<string> ccAddressList, string subject, string bodyHtml, List<KeyValuePair<string, string>> tokens,
            string templateName, string senderKeyName = "");

        /// <summary>
        ///     Creates a message with an attachment
        /// </summary>
        /// <param name="toAddress">The to address for the message</param>
        /// <param name="ccAddressList">The address(ses) to add a CC's</param>
        /// <param name="subject">The subject of the message</param>
        /// <param name="fileContent">Attachment Content</param>
        /// <param name="fileName">Attachment file name</param>
        /// <param name="bodyHtml">The HTML body contents</param>
        /// <param name="tokens">A list of tokens that should be replaced within the email message</param>
        /// <param name="templateName">The optional custom template to override with</param>
        /// <param name="senderKeyName">The a custom key for identifying a sender</param>
        /// <returns></returns>
        bool SendMessageWithAttachment(string toAddress, IEnumerable<string> ccAddressList, string subject,
            byte[] fileContent, string fileName, string bodyHtml, List<KeyValuePair<string, string>> tokens, string templateName = "", string senderKeyName = "");
    }
}