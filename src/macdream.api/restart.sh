#!/bin/bash
docker kill $(docker ps -q)
docker build -t macdream-backend -f macdream.api/Dockerfile .
docker run -p 80:80 -d macdream-backend
