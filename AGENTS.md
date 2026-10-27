# Working agreements

## Git workflow

- Create commits only when the user explicitly requests them.
- Before committing, wait for the user to review the changes locally and
  explicitly confirm that the review is complete and the changes may be
  committed. A prior request to commit does not bypass this review gate.
- Push only when the user explicitly requests a push. Permission to commit does
  not imply permission to push.
- Split work into atomic, coherent commits, ordered in dependency layers. Each
  commit must build on the preceding commits without relying on later commits.
- Include directly related documentation and public API snapshots with the
  implementation they describe.
- Follow the existing commit subject format:
  `#<GitHub issue number>: <Project name>: <Description>`.
  Use English descriptions, consistent with the repository history.
- Confirm the GitHub issue number and affected project when they are unclear;
  never invent an issue number.
- Inspect both staged and unstaged changes before committing. Stage explicit
  paths or hunks rather than including unrelated changes.
- Preserve unrelated user changes. Ask before including changes whose scope is
  unclear.
- Do not amend commits or rewrite history without explicit user authorization.

## Implementation and communication

- Ask when requirements, scope, or consequential design decisions are unclear.
- Follow existing project conventions and keep changes focused on the task.
- When a code change affects public APIs, observable behavior, configuration,
  supported frameworks, or user workflows, update the relevant user-facing
  documentation in the same change. For generated documentation, edit its
  source template and regenerate the checked-in output; keep package README
  files and public API snapshots in sync. Documentation updates are not needed
  for changes with no user-facing impact.
- Keep directly related documentation, tests, and public API snapshots in sync.
- Use the smallest relevant existing build and test commands for validation.
  Distinguish a successful build from successful test execution; zero discovered
  tests is not a passing test run.
- Communicate concisely in Polish unless the user requests another language.
