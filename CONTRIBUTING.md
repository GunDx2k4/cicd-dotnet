# Contributing Guide

We follow an **Issue-First** workflow with a simple 3-tier environment branching model (`dev` → `stag` → `main`).

---

## 📌 Core Rule: Issue First

> **No branch should be created without an existing GitHub Issue.**
> Always create or pick an Issue first before starting any work.

---

## 🌿 Branching Model

```text
<issue-number>-<short-title>  -->  dev  -->  stag  -->  main
```

| Branch | Environment | Purpose |
| :--- | :--- | :--- |
| **`main`** | **Production** | Stable releases. Merged only from `stag` (or critical hotfix). |
| **`stag`** | **Staging** | QA and pre-production verification. Merged from `dev`. |
| **`dev`** | **Development** | Active development branch. All issue branches merge here. |
| **`<issue-id>-<title>`** | Local | Working branch created from an Issue (branched off `dev`). |

---

## 🚀 Workflow Steps

### 1. Create a GitHub Issue
Create a new GitHub Issue (or pick an assigned one) describing what you plan to build or fix. Note the issue number (e.g., `#12`).

### 2. Create a Branch from `dev`
Create your branch from the latest **`dev`** using the issue number:

```bash
git checkout dev
git pull origin dev

# Format: <issue-number>-<short-description>
git checkout -b 12-user-authentication
```

*(Alternatively, click **"Create a branch"** directly inside the GitHub Issue page).*

### 3. Work & Test Locally
Ensure the solution builds and all tests pass before committing:

```bash
dotnet build CiCd.Net.slnx
dotnet test CiCd.Net.slnx
```

### 4. Open a Pull Request to `dev`
- Push your branch to GitHub.
- Create a Pull Request targeting **`dev`**.
- Link the issue in your PR description (e.g., `Closes #12`).
- After review and CI checks pass, merge into `dev`.

### 5. Promotion Flow
- **`dev` → `stag`**: When features are ready for QA testing, open a PR from `dev` to `stag`.
- **`stag` → `main`**: Once QA verification on staging is complete, open a PR from `stag` to `main` for production release.

---

## 📐 Quick Rules

- **Always link an Issue.** No loose branches without an associated Issue.
- **Never commit directly** to `main`, `stag`, or `dev`.
- **Run local tests** (`dotnet test CiCd.Net.slnx`) before pushing.
- **Manage package versions centrally** in `Directory.Packages.props`.
