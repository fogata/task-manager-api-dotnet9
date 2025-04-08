#!/bin/sh

host="$1"
shift
cmd="$@"

echo "🔄 Aguardando conexão com PostgreSQL ($host:5432)..."

until nc -z "$host" 5432 > /dev/null 2>&1; do
  echo "⏳ Esperando PostgreSQL ficar pronto..."
  sleep 1
done

echo "✅ PostgreSQL disponível. Iniciando aplicação..."
exec $cmd