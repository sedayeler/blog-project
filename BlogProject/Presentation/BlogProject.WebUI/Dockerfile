# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarını kopyala
COPY . .

# Bağımlılıkları indir
RUN dotnet restore "./BlogProject.WebUI.csproj"

# Yayınla
RUN dotnet publish "./BlogProject.WebUI.csproj" -c Release -o /out

# Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "BlogProject.WebUI.dll"]
