#!/bin/bash
docker kill $(docker ps -q)
docker build -t macdream-backend .
docker run -p 80:80 -d macdream-backend
