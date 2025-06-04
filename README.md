# CloudQA Automation Test

A robust Selenium WebDriver automation framework built in C# for testing web forms on the CloudQA practice platform.

## 🎯 Overview

This project demonstrates automated testing capabilities for web form interactions, including text input validation, email field testing, and dropdown selection. The framework uses multiple locator strategies to ensure reliable element detection across different web page structures.

## ✨ Features

- **Multi-Strategy Element Location**: Uses fallback locators (Name, ID, XPath, CSS Selector) for robust element detection
- **Cross-Browser Support**: Configured for Chrome with anti-detection measures
- **Smart Input Handling**: Implements both standard Selenium and JavaScript-based text input methods
- **Comprehensive Validation**: Validates form field values after input
- **Error Handling**: Graceful error handling with detailed console logging
- **Visual Feedback**: Emoji-enhanced console output for better test result visualization

## 🛠️ Technologies Used

- **C# (.NET)**
- **Selenium WebDriver**
- **ChromeDriver**
- **WebDriverWait** (Explicit waits)
- **SelectElement** (Dropdown handling)

## 📋 Prerequisites

Before running the tests, ensure you have:

- **.NET Framework** or **.NET Core** installed
- **Google Chrome** browser
- **ChromeDriver** (compatible with your Chrome version)
- **Visual Studio** or **Visual Studio Code** (recommended)

## 📦 Required NuGet Packages

```xml
<PackageReference Include="Selenium.WebDriver" Version="4.x.x" />
<PackageReference Include="Selenium.WebDriver.ChromeDriver" Version="x.x.x" />
<PackageReference Include="Selenium.Support" Version="4.x.x" />
<PackageReference Include="DotNetSeleniumExtras.WaitHelpers" Version="3.x.x" />
```

## 🚀 Installation & Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd CloudQAAutomationTest
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the tests**
   ```bash
   dotnet run
   ```

## 🧪 Test Coverage

The automation suite covers the following test scenarios:

### Form Field Testing
- **First Name Field**
  - Locates input field using multiple strategies
  - Enters test data ("John")
  - Validates successful input

- **Email Field**
  - Identifies email input field
  - Enters valid email address
  - Verifies email format acceptance

- **Country Dropdown**
  - Locates dropdown element
  - Selects "India" from options
  - Confirms selection accuracy

## 🏗️ Project Structure

```
CloudQAAutomationTest/
├── FormTester.cs          # Main test class with form testing logic
├── Program.cs             # Entry point and test execution
└── README.md              # Project documentation
```

## 🔧 Configuration

### Chrome Browser Options
The framework includes several Chrome options for optimal testing:

```csharp
var options = new ChromeOptions();
options.AddArgument("--disable-blink-features=AutomationControlled");
options.AddArgument("--no-sandbox");
options.AddArgument("--disable-dev-shm-usage");
```

### Target URL
Currently configured to test: `https://app.cloudqa.io/home/AutomationPracticeForm`

## 📊 Test Results

The framework provides detailed console output including:
- ✅ Successful operations
- ❌ Failed operations
- ⚠️ Warnings and missing elements
- 🔍 Discovery and navigation actions

## 🔄 Extending the Framework

To add new test cases:

1. Create a new test method following the naming convention `Test[FieldName]()`
2. Implement element location using the `FindElement()` helper method
3. Add appropriate validation using `AssertValue()`
4. Call your new test method from the `Run()` method

### Example:
```csharp
private void TestPhoneNumber()
{
    Console.WriteLine("➡️ Testing 'Phone Number' field...");
    
    var phoneField = FindElement(new List<By>
    {
        By.Name("phone"),
        By.Id("phone"),
        By.XPath("//input[@type='tel']")
    });
    
    if (phoneField != null)
    {
        EnterText(phoneField, "1234567890");
        AssertValue(phoneField, "1234567890", "Phone Number");
    }
}
```

## 🐛 Troubleshooting

**Common Issues:**

1. **ChromeDriver Version Mismatch**
   - Ensure ChromeDriver version matches your Chrome browser version
   - Update ChromeDriver from NuGet packages

2. **Element Not Found**
   - The framework uses multiple locator strategies
   - Check console output for which locators failed
   - Website structure may have changed

3. **Timeout Issues**
   - Default timeout is 10 seconds
   - Increase timeout in WebDriverWait constructor if needed

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-test`)
3. Commit your changes (`git commit -am 'Add new test case'`)
4. Push to the branch (`git push origin feature/new-test`)
5. Create a Pull Request

 

**Happy Testing! 🎉**
