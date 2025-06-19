# 🚀 Customhost Platform API - Despliegue en Producción

## ⚠️ CONFIGURACIONES CRÍTICAS ANTES DEL DESPLIEGUE

### 1. Variables de Entorno Obligatorias

```bash
# Copiar .env.production y configurar con valores reales
cp .env.production .env.local

# CRÍTICO: Configurar estas variables con valores seguros
export ASPNETCORE_ENVIRONMENT=Production
export DATABASE_PASSWORD="tu-password-super-seguro-min-16-chars"
export JWT_SECRET_KEY="tu-clave-jwt-super-segura-minimo-32-caracteres"
export CORS_ALLOWED_ORIGINS="https://tu-dominio-frontend.com"
export ALLOWED_HOSTS="tu-dominio-backend.com"
export MYSQL_ROOT_PASSWORD="password-root-mysql-seguro"
```

### 2. Certificados SSL (OBLIGATORIO para HTTPS)

```bash
# Crear directorio para certificados
mkdir -p certs

# Copiar tus certificados SSL
cp tu-certificado.pfx certs/
cp tu-certificado.crt certs/
cp tu-certificado.key certs/
```

### 3. Verificación de Seguridad

✅ **ANTES DE DESPLEGAR VERIFICA:**

- [ ] Variables de entorno configuradas
- [ ] Certificados SSL instalados
- [ ] Base de datos de producción configurada
- [ ] CORS configurado solo para dominios de confianza
- [ ] Swagger deshabilitado en producción
- [ ] Passwords seguros (mínimo 16 caracteres)
- [ ] JWT secret key seguro (mínimo 32 caracteres)

## 🚀 Despliegue

### Opción 1: Docker Compose (Recomendado)

```bash
# 1. Configurar variables de entorno
source .env.local

# 2. Ejecutar script de despliegue
chmod +x deploy-production.sh
./deploy-production.sh
```

### Opción 2: Manual

```bash
# 1. Construir aplicación
dotnet build -c Release

# 2. Ejecutar migraciones
dotnet ef database update --project customhost.platform.API

# 3. Publicar aplicación
dotnet publish -c Release -o ./publish

# 4. Ejecutar en producción
cd publish
dotnet customhost.platform.API.dll
```

## 🔍 Verificación Post-Despliegue

```bash
# Health check
curl -f https://tu-dominio.com/health

# Verificar logs
docker-compose -f docker-compose.production.yml logs -f customhost-api

# Verificar métricas
docker stats
```

## 🔐 Seguridad en Producción

### Características Implementadas:

- ✅ HTTPS obligatorio con HSTS
- ✅ Headers de seguridad (XSS, CSRF, etc.)
- ✅ CORS restrictivo
- ✅ Swagger deshabilitado
- ✅ Logging sin datos sensibles
- ✅ Variables de entorno para secretos
- ✅ Health checks para monitoreo

### URLs Importantes:

- **API**: `https://tu-dominio.com/api/v1/`
- **Health Check**: `https://tu-dominio.com/health`
- **Logs**: `docker-compose logs -f customhost-api`

## 🚨 Troubleshooting

### Error de Docker Registry (Microsoft Container Registry)

Si ves este error:
```
failed to copy: httpReadSeeker: failed open: failed to do request: Get "https://eastus.data.mcr.microsoft.com/..."
```

**Soluciones (en orden de prioridad):**

#### 1. Reiniciar Docker Desktop
```bash
# Reiniciar Docker completamente
docker system prune -a
# Reiniciar Docker Desktop desde la aplicación
```

#### 2. Configurar DNS alternativo
```bash
# Cambiar DNS a Google o Cloudflare
# En Windows: Panel de Control > Conexiones de Red
# DNS Primario: 8.8.8.8
# DNS Secundario: 8.8.4.4
```

#### 3. Usar imagen alternativa en Dockerfile
```dockerfile
# En lugar de:
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Usar:
FROM docker.io/microsoft/dotnet:8.0-aspnet AS base
```

#### 4. Configurar proxy/timeout en Docker
```bash
# Crear/editar ~/.docker/config.json
{
  "proxies": {
    "default": {
      "httpProxy": "http://proxy.company.com:8080",
      "httpsProxy": "http://proxy.company.com:8080"
    }
  }
}
```

#### 5. Dockerfile alternativo para problemas de conectividad
Usar este Dockerfile si persisten problemas:

```dockerfile
# Dockerfile alternativo con imágenes de Docker Hub
FROM docker.io/microsoft/dotnet:8.0-aspnet AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM docker.io/microsoft/dotnet:8.0-sdk AS build
WORKDIR /src

# Resto del Dockerfile igual...
```

### Si la aplicación no inicia:

```bash
# Ver logs detallados
docker-compose -f docker-compose.production.yml logs customhost-api

# Verificar variables de entorno
docker-compose -f docker-compose.production.yml exec customhost-api env

# Verificar conectividad de base de datos
docker-compose -f docker-compose.production.yml exec customhost-api ping db
```

### Si hay problemas de CORS:

1. Verificar `CORS_ALLOWED_ORIGINS` en variables de entorno
2. Verificar que el frontend use HTTPS
3. Verificar logs para errores de CORS

## 📞 Contacto

Para problemas de despliegue contactar al equipo de DevOps.
