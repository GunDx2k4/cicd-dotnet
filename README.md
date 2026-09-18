# CiCd.Net

A standardized .NET 10 solution boilerplate and reference CI/CD architecture designed for modern development teams.

---

## 🚀 Overview

This repository serves as a starter template and CI/CD blueprint for .NET projects. It demonstrates production-ready workflows for building and deploying Web APIs, shared libraries, and reusable project templates.

### Key Highlights

- **Standard Solution Layout**: Clean `src/` and `tests/` architecture using the modern `.slnx` solution format.
- **Central Package Management (CPM)**: Manage all dependency versions in a single [Directory.Packages.props](Directory.Packages.props) file.
- **Automated CI/CD Workflows**:
  - **Web API**: Automated testing, Docker image building, publishing to GitHub Container Registry (GHCR), and VPS deployment via SSH.
  - **Shared Library**: Semantic versioning, testing, and NuGet package publishing to GitHub Packages.
  - **CLI Template**: Standalone solution template packable and installable via `dotnet new`.

---

## 📁 Repository Structure

```text
cicd-dotnet/
├── .github/workflows/             # GitHub Actions CI/CD pipelines
│   ├── cicd-api.yml              # Deploy Web API (Docker + GHCR + VPS)
│   ├── cicd-common.yml           # Publish Shared Library (NuGet)
│   └── cicd-template.yml         # Publish dotnet new Template
│
├── src/                          # Application source code
│   ├── CiCd.API/                 # Sample Web API with Dockerfile
│   └── CiCd.Common/              # Shared utility library
│
├── tests/                        # Test projects
│   ├── CiCd.API.Tests/           # API integration and unit tests
│   └── CiCd.Common.Tests/        # Library unit tests
│
├── template/                     # Reusable solution template
│   ├── .template.config/         # Template definition (identity: CiCd.Template)
│   ├── CiCd.Template.slnx        # Solution file for generated projects
│   ├── src/CiCd.Template/        # Web application template
│   └── tests/CiCd.Template.Tests/# Test project template
│
├── CiCd.Net.slnx                 # Root solution file
├── Directory.Build.props         # Global build and compiler settings
├── Directory.Packages.props      # Central Package Management (CPM)
├── docker-compose.yml            # Docker compose configuration
└── nuget.config                  # NuGet package feed configuration
```

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/) (optional, for container runs)

### Build and Test

Clone the repository and run:

```bash
# Restore and build the entire solution
dotnet build CiCd.Net.slnx

# Run all tests across all projects
dotnet test CiCd.Net.slnx
```

### Run the Web API

```bash
# Run locally with .NET CLI
dotnet run --project src/CiCd.API/CiCd.API.csproj

# Or run using Docker Compose
docker compose up -d
```

### Using the Template

You can install and use the solution template locally:

```bash
# 1. Install the template from the local folder
dotnet new install ./template

# 2. Create a new project with the template
dotnet new cicd-template -n MyNewProject -o ./MyNewProject

# 3. Test your new solution
dotnet test ./MyNewProject/MyNewProject.slnx

# 4. (Optional) Uninstall the template
dotnet new uninstall ./template
```

---

## ⚙️ CI/CD Pipelines

| Pipeline | Trigger | Description |
| :--- | :--- | :--- |
| **[cicd-api.yml](.github/workflows/cicd-api.yml)** | Push to `main` | Tests API, builds Docker image, pushes to `ghcr.io`, and deploys to VPS via SSH. |
| **[cicd-common.yml](.github/workflows/cicd-common.yml)** | Push to `main` / Tag `common-v*` | Tests library, calculates semantic version, packs NuGet, and publishes to GitHub Packages. |
| **[cicd-template.yml](.github/workflows/cicd-template.yml)** | Push to `main` / Tag `template-v*` | Tests template solution, packs template package, and publishes to GitHub Packages. |

### Required GitHub Secrets

To enable automated deployments, configure the following secrets in **Settings → Secrets and variables → Actions**:

- `SERVER_HOST`: Remote VPS IP address or hostname.
- `SERVER_PORT`: SSH port (typically `22`).
- `SERVER_USERNAME`: SSH username.
- `SERVER_SSH_KEY`: Private SSH key for server access.
- `GHCR_PAT`: Personal Access Token with package read/write permissions (or use default `GITHUB_TOKEN`).

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Please check out the [Contributing Guide](CONTRIBUTING.md) for details on our workflow, coding standards, and pull request process.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
