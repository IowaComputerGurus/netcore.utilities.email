using System;
using System.Collections.Generic;
using System.Text;
using ICG.NetCore.Utilities.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace NetCore.Utilities.Email.Tests
{
    public class StartupExtensionsTests
    {
        [Fact]
        public void Configuration_ShouldLoad_WhenNoAdditionalTemplatesIncluded()
        {
            //Arrange
            var collection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings-simple.json")
                .Build();
            collection.UseIcgNetCoreUtilitiesEmail(configuration);
            var services = collection.BuildServiceProvider();
            var expectedPath = "Templates\\simple.html";

            //Act
            var result = services.GetService<IOptions<EmailTemplateSettings>>();

            //Assert
            Assert.Equal(expectedPath, result.Value.DefaultTemplatePath);
            Assert.Null(result.Value.AdditionalTemplates);
        }

        [Fact]
        public void Configuration_ShouldLoad_WithAdditionalTemplates()
        {
            //Arrange
            var collection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            collection.UseIcgNetCoreUtilitiesEmail(configuration);
            var services = collection.BuildServiceProvider();
            var expectedPath = "Templates\\test.html";

            //Act
            var result = services.GetService<IOptions<EmailTemplateSettings>>();

            //Assert
            Assert.Equal(expectedPath, result.Value.DefaultTemplatePath);
            Assert.NotNull(result.Value.AdditionalTemplates);
            Assert.True(result.Value.AdditionalTemplates.ContainsKey("TestTemplate"));
            Assert.Equal("test.path", result.Value.AdditionalTemplates["TestTemplate"]);
        }

        [Fact]
        public void ServiceCollection_ShouldHaveRegisteredInstanceOfEmailFactory()
        {
            //Arrange
            var collection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            collection.AddSingleton<IHostingEnvironment>(new Mock<IHostingEnvironment>().Object);
            collection.UseIcgNetCoreUtilitiesEmail(configuration);
            var services = collection.BuildServiceProvider();

            //Act
            var result = services.GetService<IEmailTemplateFactory>();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<EmailTemplateFactory>(result);
        }
    }
}
