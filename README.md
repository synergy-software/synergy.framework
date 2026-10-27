# synergy.framework

[![Build status](https://ci.appveyor.com/api/projects/status/l8wlmu8i0cjfcgo8?svg=true)](https://ci.appveyor.com/project/MarcinCelej/synergy-framework)

[//]: # (TODO Actualize this file)

<!--
```plantuml
@startmindmap
+ Synergy Framework
++ Synergy.Contracts
+++_ Design by Contract programming support
++ Synergy.Web.Api.Testing
+++_ Web API Testing support
++ Synergy.Behaviours.Testing
+++_ Behaviour Driven Development support
++ Synergy.Architecture.Diagrams
+++_ Automatic Sequence Diagram from Code support
++ Synergy.Documentation
+++_ support for various docs generation (Docs as Code support)
@endmindmap
```
-->
![Synergy Framework Structure Mind Map](images/generated/Synergy%20Framework%20Structure%20Mind%20Map.png) <!-- ← Generated image link. Do NOT modify it manually. -->

[Design by contract programming](https://github.com/synergy-software/synergy.framework/wiki/Contracts)

[Architecture Diagrams  as Code](Architecture/README.md)

[Docs As Code](Documentation/README.md)

## Supported frameworks and CI

.NET 5, 6 and 7 are no longer targeted. Modern tests, test helpers and the sample
Web API run on .NET 10. Libraries that previously targeted .NET 8 retain that
target alongside .NET 10. Existing .NET Standard 2.0 and .NET Framework 4.7.2/4.8
targets are unchanged.

Build with the .NET 10 SDK. On Windows, the legacy projects also require the
.NET Framework targeting packs; their .NET Framework tests still run separately.
No .NET 5, 6 or 7 runtime installation or major-version roll-forward is needed.

AppVeyor configuration is maintained in the project settings. Use a Windows image
with the .NET 10 SDK/runtime and the .NET Framework targeting packs. Remove the
legacy runtime installer from **Settings > Build > Before build script** and keep:

```powershell
dotnet restore
if ($LASTEXITCODE -ne 0) { throw 'dotnet restore failed.' }
nuget restore
if ($LASTEXITCODE -ne 0) { throw 'nuget restore failed.' }
```

Select **PowerShell** for this script. Keep the existing build, packaging and
test steps; modern test projects can still be run with `dotnet test --no-build`
after the build.
