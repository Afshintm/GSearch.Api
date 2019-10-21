# README #

### What is this repository for? ###
A simple .net core 3 restful api to search some keywords in google search and returns the positions of a target url in search result 

### Get up and running with Web Api app ###

To build web api just clone it and open GSearch.Api.sln using visual studio 2019 then Build and run it.

Webapi has got 3 projects.


1- Business Services Layer

2- Web APi layer

3- Unit testing -  for Business Services unit testing


### Api Endpoints: ###

Get request to search some keywords in google and count the target url position in the result: `http://localhost:53132/search?keywords=online%20title%20search&url=www.infotrack.com.au`
Get request and specify the maximum 50 result in search and keywords as well as target url: `http://localhost:53132/search?keywords=online%20title%20search&url=www.infotrack.com.au&num=50`

### Using Generic classes to add Bing search as a bonus point ###
The generic class GenericSearchServices<T> give us an option to use different search engine like bing. 
This means that the route ~/bing/search will do the search using Bing website. The full route is `http://localhost:53132/bing/search?keywords=online%20title%20search&url=www.infotrack.com.au`

### Technical spec ###

In this project, microsoft recommended approach of using HttpClient and HttpClientFactory has been followed.
Autofac is used for DI and for Unit tests Mock and a Fake implementation of HttpMessageHandler. 



### See it in action ###
I have deployed the api as a docker container to aws elastic containers and you can use the following link to use that 

`http://g-search-api-1245595445.ap-southeast-2.elb.amazonaws.com/search?keywords=online%20title%20search&url=www.infotrack.com.au`
