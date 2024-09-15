#!/bin/bash
set -e

# Variables (modifica según tus necesidades)
COMPOSE_FILE="compose.yml"
SERVICE_NAME="product"

# Ejecutar Docker Compose
docker-compose -f $COMPOSE_FILE up --build -d $SERVICE_NAME