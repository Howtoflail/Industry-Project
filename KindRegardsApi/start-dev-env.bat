@echo off

docker-compose -f docker-compose-dev.yml down --remove-orphans
docker-compose -f docker-compose-dev.yml build
docker-compose -f docker-compose-dev.yml up
docker-compose -f docker-compose-dev.yml down --remove-orphans

exit
