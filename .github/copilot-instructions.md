# MovieTracker Repository Instructions

## Project Structure

- The .NET solution is in `src/01/MovieTracker.sln` and targets .NET 10. Shared compiler settings are in `src/01/Directory.Build.props`; package versions are centrally managed in `src/01/Directory.Packages.props`.
- Keep responsibilities in the existing projects: Domain owns domain concepts, Application owns use cases and repository abstractions, Infrastructure implements external concerns, API exposes HTTP endpoints, and Web is the Blazor client.
- Preserve project-reference boundaries. In particular, Web must not reference server-side projects; communicate with the API through its existing client/service layer.
- Follow the existing request path and patterns: endpoint, application handler and validation, repository abstraction, infrastructure implementation. Avoid bypassing these layers for new behavior.

## Scope and Conventions

- Keep changes focused on the requested exercise. The starter exercise asks for presentation-only changes to the Movies page and explicitly excludes new CRUD operations, a database, authentication, Docker, and caching unless the user asks to expand that scope.
- Preserve existing API contracts and behavior unless a request specifically requires changing them.
- Follow the existing C# style: nullable reference types and implicit usings are enabled, and package versions belong in `Directory.Packages.props`.
- Tests use NUnit. Add or update focused unit tests for application/domain behavior and integration tests for HTTP behavior, following neighboring fixtures and helpers.
- Do not commit generated output such as `bin/`, `obj/`, or `.vs/`.

## Validation

Run solution commands from `src/01`:

```bash
dotnet build MovieTracker.sln
dotnet test MovieTracker.sln
```

For local application checks, run the API and Web projects with their existing HTTPS launch profiles. The exercise documents the expected URLs and startup commands.