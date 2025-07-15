# --- Etapa 1: Compilación (Build Stage) ---
# Usamos la imagen oficial del SDK de .NET 8. Es grande porque tiene todas las herramientas para compilar.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos primero los archivos de proyecto (.csproj) y restauramos las dependencias.
# Esto aprovecha el cache de Docker: si no cambian las dependencias, no se vuelven a descargar.
COPY ["CuentasApi/CuentasApi.csproj", "CuentasApi/"]
RUN dotnet restore "CuentasApi/CuentasApi.csproj"

# Copiamos el resto del código fuente del proyecto.
COPY . .
WORKDIR "/src/CuentasApi"

# Compilamos la aplicación en modo "Release" y la publicamos en una carpeta llamada /app/publish.
# Esto crea una versión optimizada de la API.
RUN dotnet publish "CuentasApi.csproj" -c Release -o /app/publish

# --- Etapa 2: Imagen Final (Final Stage) ---
# Empezamos de cero con una imagen súper ligera que solo contiene el runtime de ASP.NET.
# No tiene el SDK, por lo que es mucho más pequeña y segura.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiamos ÚNICAMENTE los archivos compilados desde la etapa 'build'.
COPY --from=build /app/publish .

# Exponemos el puerto 8080. Dentro del contenedor, la app escuchará en este puerto.
EXPOSE 8080

# Este es el comando que se ejecutará cuando el contenedor se inicie.
# Inicia nuestra API.
ENTRYPOINT ["dotnet", "CuentasApi.dll"]
