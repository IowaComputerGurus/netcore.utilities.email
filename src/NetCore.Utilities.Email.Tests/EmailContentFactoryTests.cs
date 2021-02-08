using System;
using System.Collections.Generic;
using System.IO;
using ICG.NetCore.Utilities.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace NetCore.Utilities.Email.Tests
{
    public class EmailContentFactoryTests
    {
        private const string RelativePathToDefault = "..\\..\\..\\Templates\\Default.html";
        private const string RelativePathToSpecial = "..\\..\\..\\Templates\\Special.html";
        private Mock<IHostingEnvironment> _hostingEnvironmentMock;
        private Mock<IOptions<EmailTemplateSettings>> _emailTemplateSettingsMock;
        private readonly IEmailTemplateFactory _factory;

        public EmailContentFactoryTests()
        {
            _hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            _hostingEnvironmentMock.Setup(h => h.ContentRootPath).Returns("..\\..\\..\\"); //Provides a path back to the root of the test project
            _emailTemplateSettingsMock = new Mock<IOptions<EmailTemplateSettings>>();
            _emailTemplateSettingsMock.Setup(s => s.Value).Returns(new EmailTemplateSettings
            {
                DefaultTemplatePath = "Templates\\default.html",
                AdditionalTemplates = new Dictionary<string, string>{{"Special", "Templates\\Special.html"}, {"Invalid", "invalid.html"}}
            });
            _factory = new EmailTemplateFactory(_emailTemplateSettingsMock.Object, _hostingEnvironmentMock.Object);
        }

        [Fact]
        public void BuildEmailContent_ShouldThrowArgumentNullException_WhenSubjectMissing()
        {
            //Arrange

            //Act
            Assert.Throws<ArgumentNullException>("subject", () => _factory.BuildEmailContent("", "Testing"));
        }

        [Fact]
        public void BuildEmailContent_ShouldThrowArgumentNullException_WhenContentMissing()
        {
            //Arrange
            var subject = "Testing";

            //Act
            Assert.Throws<ArgumentNullException>("content", () => _factory.BuildEmailContent(subject, ""));
        }

        [Fact]
        public void BuildEmailContent_ShouldThrowArgumentException_WhenTemplateReqeustedNotDefined()
        {
            //Arrange
            var subject = "Testing";
            var content = "My Content";
            var requestedTemplate = "NotHere";

            //Act
            Assert.Throws<ArgumentException>(() =>
                _factory.BuildEmailContent(subject, content, templateName: requestedTemplate));
        }

        [Fact]
        public void BuildEmailContent_ShouldThrowFileNotFoundException_WhenTemplateFileDoesNotExist()
        {
            //Arrange
            var subject = "Testing";
            var content = "My Content";
            var requestedTemplate = "Invalid";

            //Act
            Assert.Throws<FileNotFoundException>(() =>
                _factory.BuildEmailContent(subject, content, templateName: requestedTemplate));
        }

        [Fact]
        public void BuildEmailContent_ShouldReturnProperlyFormattedText_WithSubjectAndContentOnly_DefaultTemplate()
        {
            //Arrange
            var subject = "TestSubject";
            var content = "<p>Testing</p>";
            var expectedBody = File.ReadAllText(RelativePathToDefault)
                .Replace("[SUBJECT]", subject).Replace("[PREVIEW]", string.Empty).Replace("[CONTENT]", content);

            //Act
            var actualResult = _factory.BuildEmailContent(subject, content);

            //Assert
            Assert.Equal(expectedBody, actualResult);
        }

        [Fact]
        public void BuildEmailContent_ShouldReturnProperlyFormattedText_WithSubjectAndContentOnly_AdditionalTemplate()
        {
            //Arrange
            var subject = "TestSubject";
            var content = "<p>Testing</p>";
            var requestedTemplate = "Special";
            var expectedBody = File.ReadAllText(RelativePathToSpecial)
                .Replace("[SUBJECT]", subject).Replace("[PREVIEW]", string.Empty).Replace("[CONTENT]", content);

            //Act
            var actualResult = _factory.BuildEmailContent(subject, content, templateName: requestedTemplate);

            //Assert
            Assert.Equal(expectedBody, actualResult);
        }

        [Fact]
        public void BuildEmailContent_ShouldReturnProperlyFormattedText_WithSubjectPreviewAndContent_DefaultTemplate()
        {
            //Arrange
            var subject = "TestSubject";
            var content = "<p>Testing</p>";
            var preview = "This is a preview";
            var expectedBody = File.ReadAllText(RelativePathToDefault)
                .Replace("[SUBJECT]", subject).Replace("[PREVIEW]", preview).Replace("[CONTENT]", content);

            //Act
            var actualResult = _factory.BuildEmailContent(subject, content, preview);

            //Assert
            Assert.Equal(expectedBody, actualResult);
        }
        
        [Fact]
        public void BuildEmailContent_ShouldReturnProperlyFormattedText_WithSubjectPreviewAndContent_AdditionalTemplate()
        {
            //Arrange
            var subject = "TestSubject";
            var content = "<p>Testing</p>";
            var preview = "This is a preview";
            var requestedTemplate = "Special";
            var expectedBody = File.ReadAllText(RelativePathToSpecial)
                .Replace("[SUBJECT]", subject).Replace("[PREVIEW]", preview).Replace("[CONTENT]", content);

            //Act
            var actualResult = _factory.BuildEmailContent(subject, content, preview, requestedTemplate);

            //Assert
            Assert.Equal(expectedBody, actualResult);
        }
    }
}
