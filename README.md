# Docker Project
## This project was created to study docker, docker-compose and also kafka.

# Scenario  
  It will run these services:
- Zookeeper
- Kafka
- Kafdrop
- Kafka Consumer  
# Steps 
First of all we have to build the consumer image:
Get into the dockerfile path DockerProject/Producer
Execute the following command:
> docker build -t bordallog/kafka-producer .

After That,  you have to go to the root file of the project (where the docker-compose.yml file is) and run the docker compose command:
> docker-compose up -d
