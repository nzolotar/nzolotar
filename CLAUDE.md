# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Purpose

Personal portfolio repository for Natalia Zolotareva (Senior Full-Stack SDE). Contains two top-level categories:
- **`Interviews/`** — Technical assessment submissions for specific companies
- **`Practices/`** — Coding challenges, algorithm practice, and learning projects

## Commands

### C# / .NET Projects
All C# projects (under `Interviews/` and `Practices/PracticeC#/`) use the standard .NET CLI:

```bash
dotnet restore          # Restore NuGet packages
dotnet build            # Build solution or project
dotnet test             # Run all tests
dotnet test --filter "FullyQualifiedName~TestName"  # Run a single test
dotnet run              # Run a console/web project
dotnet publish -c Release  # Publish for deployment
```

### Synapse (Docker-based Web API)
```bash
cd Interviews/Synapse
docker build -t synapse .
docker-compose up -d    # Run with persistent logs volume
docker-compose down
```

### Next.js (`Practices/practice-next-js/`)
```bash
npm run dev     # Dev server at localhost:3000
npm run build   # Production build
npm run lint    # ESLint
```

### BooksAndReviews JS (`Practices/PracticeJS/BooksAndReviews/`)
```bash
npm test              # Karma + Jasmine test suite
npm run build         # Webpack bundle
```

### PracticeNode (`Practices/PracticeNode/`)
```bash
npm i
node ./e1/test.js     # Run an example
```

## Architecture & Structure

### `Interviews/Synapse/` — Most architecturally significant project
Production-grade .NET 8 Web API for medical equipment order processing. Key patterns:

- **Layered structure**: `Entities/` → `Interfaces/` → `Services/` → `Program.cs`
- **Three core services**: `OrderRepository` (data access), `AlertService` (notifications), `OrderProcessor` (orchestration)
- **`IHttpClientWrapper`** abstracts all HTTP calls to enable unit testing without live endpoints
- **Logging**: NLog (writes to `/app/logs` in Docker, configured via `nlog.config`) — not `Console.WriteLine`
- **Testing stack**: xUnit + Moq + AutoFixture + FluentAssertions in `Synapse.UnitTests/`
- **Config**: Environment-specific `appsettings.{env}.json` files; API URLs injected via `ApiSettings.cs`
- **Docker**: Multi-stage build, non-root user (`dotnetuser`), bridge network (`synapse-network`), log volume mount

### `Interviews/Origence-Tech/` — Algorithm challenges
Two standalone problems, each with a matching test project:
- `ConsoleApp_StackProblem` / `ConsoleApp_StackProblem_TESTS` — custom Stack (push/pop/peek)
- `ConsoleApp2` / `ConsoleApp_RemoveDuplicatesProblem_TESTS` — remove duplicate chars from string

### `Practices/practice-next-js/`
Next.js 14 App Router project with TypeScript and Tailwind CSS. Standard `app/` directory structure.

### `Practices/PracticeJS/BooksAndReviews/`
Webpack + Babel bundled vanilla JS app. Tests run in Chrome via Karma. Config split across `karma.conf.js` and `webpack.config.js`.

### `Practices/PracticeNode/`
Express + TypeScript Node backend. TypeScript config in `tsconfig.json`.

## .gitignore
Globally excludes `**/obj/`, `**/bin/`, `**/.vs/` — build artifacts should never be committed.
