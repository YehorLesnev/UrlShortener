# UrlShortener API
 This is my pet project. UrlShortener API receives full url and returns shorted url. When user go to shorted ulr UrlShortener redirects user to corresponding full url.

> [!NOTE]
> Before sending requests to running API you should run the following command to start the Redis docker container on port 6379:
> 
> `docker run -p 6379:6379 --name redis -d redis` - this command will create and start a docker container from the latest official Redis image with name _redis_
> 
> If you already have such container just run `docker start NAME_OF_CONTAINER` or `docker start CONTAINER_ID`
