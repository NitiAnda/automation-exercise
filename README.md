# Automation Exercise — QA Suite

Playwright + C# (NUnit) automated regression suite for [automationexercise.com](https://automationexercise.com/), built as a Quest Global QA internship deliverable.

- **10 core test cases** from the official list + **2 plus-value scenarios** (12 total)
- Page Object Model — all selectors isolated in `Pages/`
- Parallel execution (3 workers), fully isolated test data per test
- Playwright traces + screenshots captured automatically on failure
- Headless by default; headed with one env-var flag
- GitHub Actions CI on every push / pull request to `main`

---

## Prerequisites

| Tool | Version | Notes |
|------|---------|-------|
| [.NET SDK](https://dotnet.microsoft.com/download) | **10.0** | `dotnet --version` must show `10.x` |
| [PowerShell 7+](https://github.com/PowerShell/PowerShell/releases) | 7+ (`pwsh`) | Required for the Playwright browser install script |
| Git | any | — |

> Playwright manages its own browser binaries — no separate Chrome/Edge install needed.

---

## Setup (first time)

```bash
# 1. Clone and enter the project
git clone <repo-url>
cd qa

# 2. Restore NuGet packages
dotnet restore tests/AutomationExercise.Tests

# 3. Build
dotnet build tests/AutomationExercise.Tests

# 4. Install Playwright browsers (one-time, ~2 min download)
pwsh tests/AutomationExercise.Tests/bin/Debug/net10.0/playwright.ps1 install
```

After step 4 you can run tests immediately. Repeat step 4 whenever Playwright is upgraded.

---

## Running tests

All commands run from the repo root (`qa/`).

### Full suite

```bash
dotnet test tests/AutomationExercise.Tests
```

### By category

```bash
# Smoke tests only (fast, critical-path)
dotnet test tests/AutomationExercise.Tests --filter "Category=Smoke"

# Regression tests only
dotnet test tests/AutomationExercise.Tests --filter "Category=Regression"
```

### Single test by name

```bash
dotnet test tests/AutomationExercise.Tests --filter "FullyQualifiedName~TC1_RegisterUser"
dotnet test tests/AutomationExercise.Tests --filter "FullyQualifiedName~TC14"
```

### Headed mode (visible browser)

```bash
# Linux / macOS
HEADED=1 dotnet test tests/AutomationExercise.Tests

# PowerShell (Windows)
$env:HEADED = "1"; dotnet test tests/AutomationExercise.Tests
```

### Verbose output

```bash
dotnet test tests/AutomationExercise.Tests --logger "console;verbosity=detailed"
```

### With TRX report

```bash
dotnet test tests/AutomationExercise.Tests \
  --logger "trx;LogFileName=results.trx" \
  --results-directory TestResults
```

---

## Configuration

Edit `tests/AutomationExercise.Tests/appsettings.json` to change defaults:

```json
{
  "BaseUrl": "https://automationexercise.com",
  "Browser": "chromium",
  "Headless": true,
  "DefaultTimeoutMs": 30000
}
```

All keys can be overridden with environment variables at runtime:

| Environment variable | Effect |
|----------------------|--------|
| `BaseUrl` | Target base URL |
| `Browser` | `chromium` · `firefox` · `webkit` |
| `Headless` | `false` to run headed (or use `HEADED=1` shortcut) |
| `HEADED=1` | Shortcut to disable headless regardless of `Headless` setting |
| `DefaultTimeoutMs` | Global Playwright timeout in milliseconds |

---

## Traces and screenshots

On any test failure, Playwright automatically saves:

```
tests/AutomationExercise.Tests/bin/Debug/net10.0/playwright-traces/
  <TestName>.zip   ← full Playwright trace
  <TestName>.png   ← full-page screenshot
```

Open a trace interactively:

```bash
pwsh tests/AutomationExercise.Tests/bin/Debug/net10.0/playwright.ps1 show-trace <path-to-trace.zip>
```

The Trace Viewer shows every action, network request, console log, and DOM snapshot at each step.

---

## Project structure

```
qa/
├── docs/
│   └── test-plan.md                    # written test plan (scope, risks, improvements)
├── tests/
│   └── AutomationExercise.Tests/
│       ├── Fixtures/
│       │   └── BaseTest.cs             # extends PageTest; trace/screenshot hooks
│       ├── Helpers/
│       │   ├── ConfigLoader.cs         # reads appsettings.json + env vars
│       │   └── TestDataFactory.cs      # Bogus-generated users, addresses
│       ├── Pages/                      # Page Object Model — all selectors live here
│       │   ├── HomePage.cs
│       │   ├── LoginSignupPage.cs
│       │   ├── AccountCreatedPage.cs
│       │   ├── ProductsPage.cs
│       │   ├── ProductDetailsPage.cs
│       │   ├── CartPage.cs
│       │   ├── CheckoutPage.cs
│       │   ├── PaymentPage.cs
│       │   ├── ContactUsPage.cs
│       │   └── TestCasesPage.cs
│       ├── Tests/                      # one file per scenario
│       ├── TestData/
│       │   └── sample-upload.txt       # fixture file for the contact-us upload test
│       ├── appsettings.json
│       └── .runsettings
└── .github/
    └── workflows/
        └── playwright.yml              # CI pipeline
```

---

## Test coverage

### Core — 10 official test cases

| # | TC-ID | Scenario | Category | File |
|---|-------|----------|----------|------|
| 1 | TC1 | Register User | Smoke | `TC1_RegisterUser.cs` |
| 2 | TC2 | Login with correct credentials | Smoke | `TC2_LoginCorrect.cs` |
| 3 | TC3 | Login with incorrect credentials | Regression | `TC3_LoginIncorrect.cs` |
| 4 | TC5 | Register with existing email | Regression | `TC5_RegisterExistingEmail.cs` |
| 5 | TC7 | Verify Test Cases page | Smoke | `TC7_TestCasesPage.cs` |
| 6 | TC9 | Search product | Smoke | `TC9_SearchProduct.cs` |
| 7 | TC10 | Subscription on home page | Regression | `TC10_SubscriptionHome.cs` |
| 8 | TC14 | Place Order: register while checkout | Smoke | `TC14_PlaceOrderRegisterWhileCheckout.cs` |
| 9 | TC18 | View category products | Regression | `TC18_ViewCategoryProducts.cs` |
| 10 | TC21 | Add review on product | Regression | `TC21_AddReview.cs` |

### Plus-value — additional scenarios

| # | TC-ID | Scenario | Category | File |
|---|-------|----------|----------|------|
| 11 | TC-VA-05 | API contract — GET /api/productsList | Smoke | `TCVA05_ApiProductsList.cs` |
| 12 | TC-VA-06 | Contact-Us form with file upload | Regression | `TCVA06_ContactUsFileUpload.cs` |

---

## CI

GitHub Actions workflow at `.github/workflows/playwright.yml`:

- Triggers on every `push` and `pull_request` to `main`
- Runs on `ubuntu-latest`, headless Chromium
- Steps: checkout → setup .NET 10 → restore (NuGet cache) → build → Playwright install → `dotnet test`
- Artifacts retained 14 days:
  - `playwright-traces/` — traces and screenshots from failed tests
  - `TestResults/*.trx` — NUnit TRX report

---

## Troubleshooting

**`pwsh: command not found`**
Install PowerShell 7: https://github.com/PowerShell/PowerShell/releases

**`PlaywrightException: Executable doesn't exist`**
Run the browser install step again:
```bash
pwsh tests/AutomationExercise.Tests/bin/Debug/net10.0/playwright.ps1 install
```

**Tests time out against the live site**
The site can be slow under load. Increase `DefaultTimeoutMs` in `appsettings.json` or set it as an env var. Do not add `Thread.Sleep`.

**Failures in CI but not locally**
Download the `playwright-traces` artifact from the failed run and open it:
```bash
pwsh playwright.ps1 show-trace trace.zip
```
The Trace Viewer pinpoints which action timed out and shows the DOM at that moment.

**Headed mode shows a blank window on Linux**
Install missing system dependencies:
```bash
pwsh tests/AutomationExercise.Tests/bin/Debug/net10.0/playwright.ps1 install-deps
```
