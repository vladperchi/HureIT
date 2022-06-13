FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /webapi
EXPOSE 5005
EXPOSE 5006

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /
COPY [".editorconfig", "/"]
COPY ["Directory.Build.props", "/"]
COPY ["src/server/Api/Bootstrapper/Bootstrapper.csproj", "src/server/Api/Bootstrapper/"]
COPY ["src/server/Shared/Shared.DTO/Shared.DTO.csproj", "src/server/Shared/Shared.DTO/"]
COPY ["src/server/Shared/Shared.Core/Shared.Core.csproj", "src/server/Shared/Shared.Core/"]
COPY ["src/server/Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/server/Shared/Shared.Infrastructure/"]
COPY ["src/server/Modules/Identity/Modules.Identity.Core/Modules.Identity.Core.csproj", "src/server/Modules/Identity/Modules.Identity.Core/"]
COPY ["src/server/Modules/Identity/Modules.Identity.Infrastructure/Modules.Identity.Infrastructure.csproj", "src/server/Modules/Identity/Modules.Identity.Infrastructure/"]
COPY ["src/server/Modules/Identity/Modules.Identity.Api/Modules.Identity.Api.csproj", "src/server/Modules/Identity/Modules.Identity.Api/"]
COPY ["src/server/Modules/Workflow/Modules.Workflow.Core/Modules.Workflow.Core.csproj", "src/server/Modules/Workflow/Modules.Workflow.Core/"]
COPY ["src/server/Modules/Workflow/Modules.Workflow.Infrastructure/Modules.Workflow.Infrastructure.csproj", "src/server/Modules/Workflow/Modules.Workflow.Infrastructure/"]
COPY ["src/server/Modules/Workflow/Modules.Workflow.Api/Modules.Workflow.Api.csproj", "src/server/Modules/Workflow/Modules.Workflow.Api/"]

COPY ["/hureit.ruleset", "src/server/Api/Bootstrapper/"]
COPY ["/hureit.ruleset", "src/server/Shared/Shared.DTO/"]
COPY ["/hureit.ruleset", "src/server/Shared/Shared.Core/"]
COPY ["/hureit.ruleset", "src/server/Shared/Shared.Infrastructure/"]
COPY ["/hureit.ruleset", "src/server/Modules/Identity/Modules.Identity.Core/"]
COPY ["/hureit.ruleset", "src/server/Modules/Identity/Modules.Identity.Infrastructure/"]
COPY ["/hureit.ruleset", "src/server/Modules/Identity/Modules.Identity.Api/"]
COPY ["/hureit.ruleset", "src/server/Modules/Workflow/Modules.Workflow.Core/"]
COPY ["/hureit.ruleset", "src/server/Modules/Workflow/Modules.Workflow.Infrastructure/"]
COPY ["/hureit.ruleset", "src/server/Modules/Workflow/Modules.Workflow.Api/"]

COPY ["/stylecop.json", "src/server/Api/Bootstrapper/"]
COPY ["/stylecop.json", "src/server/Shared/Shared.DTO/"]
COPY ["/stylecop.json", "src/server/Shared/Shared.Core/"]
COPY ["/stylecop.json", "src/server/Shared/Shared.Infrastructure/"]
COPY ["/stylecop.json", "src/server/Modules/Identity/Modules.Identity.Core/"]
COPY ["/stylecop.json", "src/server/Modules/Identity/Modules.Identity.Infrastructure/"]
COPY ["/stylecop.json", "src/server/Modules/Identity/Modules.Identity.Api/"]
COPY ["/stylecop.json", "src/server/Modules/Workflow/Modules.Workflow.Core/"]
COPY ["/stylecop.json", "src/server/Modules/Workflow/Modules.Workflow.Infrastructure/"]
COPY ["/stylecop.json", "src/server/Modules/Workflow/Modules.Workflow.Api/"]

RUN dotnet restore "src/server/Api/Bootstrapper/Bootstrapper.csproj"

COPY . .
WORKDIR "/src/server/Api/Bootstrapper"
RUN dotnet build "Bootstrapper.csproj" -c Release -o /webapi/build

FROM build AS publish
RUN dotnet publish "Bootstrapper.csproj" -c Release -o /webapi/publish

FROM base AS final
WORKDIR /webapi

COPY --from=publish /webapi/publish .
WORKDIR /webapi/Files
WORKDIR /webapi

ENTRYPOINT ["dotnet", "HureIT.Bootstrapper.dll"]