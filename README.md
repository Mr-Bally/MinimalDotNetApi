# MinimalDotNetApi
A minimal web API written in .NET 6 using Dapper for the DAL. 
Includes SQL Server database migrations for quick set up

## Useful Docker Commands

### Containers and Images

Remove all Docker images. Make sure there are no running containers which depend on these images

```docker
	docker rmi -f $(docker images -aq)
```

Remove all Docker containers

```docker
	docker rm -vf $(docker ps -aq)
```

Remove stopped Docker containers

```docker
	docker rm $(docker ps --filter status=exited -q)
```

### Running Docker containers

Build a Docker image with a tag

```docker
	docker build . -t example-docker-tag
```

Build a Docker container from an existing image with a given name and port mapping

```docker
	docker run -p 8080:80 --name exampleapi example-web-api
```

Bring up a Docker compose file with a non-default name

```docker
	docker-compose -f docker-compose-db.yml up
```

### Docker Networks

Remove all Docker networks not used by at least one container

```docker
	docker network prune
```