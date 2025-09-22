# NetCore.Utilities.Email
NetCore.Utilities.Email is a .NET 8.0 utility library for working with email templating and services. It provides email template processing functionality with token replacement and supports multiple template configurations. This is a foundational library used by other concrete email implementations like NetCore.Utilities.Email.Smtp.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively
- Bootstrap, build, and test the repository:
  - `cd src`
  - `dotnet restore "NetCore Utilities Email.sln"` -- takes ~9 seconds. NEVER CANCEL.
  - `dotnet build "NetCore Utilities Email.sln" --no-restore --configuration Debug` -- takes ~11 seconds. NEVER CANCEL.
  - `dotnet build "NetCore Utilities Email.sln" --no-restore --configuration Release` -- takes ~2 seconds for incremental builds. NEVER CANCEL.
- Run unit tests:
  - `dotnet test "NetCore Utilities Email.sln" --no-build --configuration Debug` -- takes ~3 seconds. NEVER CANCEL.
  - `dotnet test "NetCore Utilities Email.sln" --no-build --configuration Release` -- takes ~3 seconds. NEVER CANCEL.
  - **IMPORTANT**: Tests will fail on Linux/Mac due to Windows path separators in test code. This is expected - CI runs on Windows where tests pass.
- Code formatting and analysis:
  - `dotnet format "NetCore Utilities Email.sln"` -- apply automatic code formatting. Takes ~11 seconds. NEVER CANCEL.
  - `dotnet format "NetCore Utilities Email.sln" --verify-no-changes` -- verify code formatting compliance
  - **CRITICAL**: Always run `dotnet format` before committing or the CI build may fail

## Project Structure
- **NetCore.Utilities.Email**: Main library project containing email template functionality
- **NetCore.Utilities.Email.Tests**: Unit test project with xUnit tests
- **Solution file**: "NetCore Utilities Email.sln" (note the spaces in the name)

## Platform and CI Requirements
- **Target Framework**: .NET 8.0
- **CI Environment**: Windows (ci-build.yml runs on windows-latest)
- **Local Development**: Works on any platform but tests may fail on non-Windows due to path separator issues
- **NuGet**: Automatically generates packages on build (ICG.NetCore.Utilities.Email.*.nupkg)

## Validation
- ALWAYS run through basic template factory scenarios after making changes:
  - Build both Debug and Release configurations successfully
  - Run working tests: `dotnet test --filter "StartupExtensions" --no-build`
  - Run validation tests: `dotnet test --filter "BuildEmailContent_ShouldThrowArgumentNullException" --no-build`
  - Verify code formatting compliance: `dotnet format --verify-no-changes`
- **Manual Validation**: Test the core functionality by ensuring EmailTemplateFactory can load templates and replace tokens ([SUBJECT], [PREVIEW], [CONTENT])
- **Known Issue**: Template file path tests fail on Linux/Mac but pass on Windows CI - this is expected behavior

## Common Tasks
The following are outputs from frequently run commands. Reference them instead of viewing, searching, or running bash commands to save time.

### Repository Root
```
ls -la /
.git
.github  
.gitignore
CODE_OF_CONDUCT.md
CONTRIBUTING.md
GitVersion.yml
LICENSE
README.md
src
```

### Build Commands
```bash
# Full build process (run from src/ directory)
dotnet restore "NetCore Utilities Email.sln"
dotnet build "NetCore Utilities Email.sln" --no-restore --configuration Release
dotnet test "NetCore Utilities Email.sln" --no-build --configuration Release
dotnet format "NetCore Utilities Email.sln"
```

### CI Workflow Overview
- **ci-build.yml**: Runs on Windows, includes SonarQube analysis, requires passing tests
- **release-build.yml**: Publishes to NuGet when tags are pushed
- **Build time**: ~11 seconds for initial build, ~2 seconds for incremental
- **Test time**: ~3 seconds
- **Format time**: ~11 seconds

### Key Files to Know
- **EmailTemplateFactory.cs**: Main template processing class with IEmailTemplateFactory interface
- **EmailTemplateSettings.cs**: Configuration class for template paths
- **StartupExtensions.cs**: Dependency injection configuration
- **Templates/Default.html** and **Templates/Special.html**: Test template files (in test project)
- **appsettings.json**: Configuration examples for template settings

### Development Workflow
1. Make code changes in src/NetCore.Utilities.Email/
2. Build: `dotnet build "NetCore Utilities Email.sln" --configuration Debug`
3. Test: `dotnet test "NetCore Utilities Email.sln" --no-build` (expect some failures on non-Windows)
4. Format: `dotnet format "NetCore Utilities Email.sln"`
5. Final validation: `dotnet build "NetCore Utilities Email.sln" --configuration Release`

### Token Replacement System
The library processes email templates with these tokens:
- `[SUBJECT]` - Email subject line
- `[PREVIEW]` - Preview text
- `[CONTENT]` - Main email content

### Configuration Example
```json
{
  "EmailTemplateSettings": {
    "DefaultTemplatePath": "Templates\\default.html",
    "AdditionalTemplates": { 
      "Special": "Templates\\Special.html" 
    }
  }
}
```

## Critical Reminders
- **NEVER CANCEL**: Builds may take up to 11 seconds, tests take ~3 seconds - wait for completion
- **Format First**: Always run `dotnet format` before committing  
- **Windows-Specific**: Some tests only pass on Windows due to path separators - this is expected
- **Solution Name**: Remember the solution file has spaces: "NetCore Utilities Email.sln"
- **Working Directory**: Always run build commands from the `src/` directory
- **Automatic Packaging**: Every build generates NuGet packages automatically