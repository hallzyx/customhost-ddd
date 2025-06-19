#!/bin/bash
# Script de despliegue para producción
# chmod +x deploy-production.sh

echo "🚀 INICIANDO DESPLIEGUE EN PRODUCCIÓN"

# Verificar que estamos en el entorno correcto
if [ "$ASPNETCORE_ENVIRONMENT" != "Production" ]; then
    echo "❌ ERROR: ASPNETCORE_ENVIRONMENT debe ser 'Production'"
    exit 1
fi

# Verificar variables críticas
if [ -z "$DATABASE_PASSWORD" ] || [ -z "$JWT_SECRET_KEY" ] || [ -z "$CORS_ALLOWED_ORIGINS" ]; then
    echo "❌ ERROR: Variables de entorno críticas no configuradas"
    echo "Requeridas: DATABASE_PASSWORD, JWT_SECRET_KEY, CORS_ALLOWED_ORIGINS"
    exit 1
fi

echo "✅ Variables de entorno verificadas"

# Crear backup de base de datos si existe
echo "📦 Creando backup de base de datos..."
docker-compose -f docker-compose.production.yml exec db mysqldump -u root -p$MYSQL_ROOT_PASSWORD customhost_production > ./backup/backup_$(date +%Y%m%d_%H%M%S).sql

# Construir imagen de producción
echo "🔨 Construyendo imagen de producción..."
docker-compose -f docker-compose.production.yml build --no-cache

# Ejecutar migraciones de base de datos
echo "🗄️ Ejecutando migraciones de base de datos..."
docker-compose -f docker-compose.production.yml run --rm customhost-api dotnet ef database update

# Desplegar aplicación
echo "🚀 Desplegando aplicación..."
docker-compose -f docker-compose.production.yml up -d

# Verificar que la aplicación está funcionando
echo "🔍 Verificando que la aplicación está funcionando..."
sleep 30

if curl -f http://localhost/health; then
    echo "✅ DESPLIEGUE EXITOSO - Aplicación funcionando correctamente"
else
    echo "❌ ERROR EN DESPLIEGUE - Verificar logs"
    docker-compose -f docker-compose.production.yml logs customhost-api
    exit 1
fi

echo "🎉 DESPLIEGUE COMPLETADO"
echo "📊 Ver logs: docker-compose -f docker-compose.production.yml logs -f"
echo "🔗 Health check: http://localhost/health"
