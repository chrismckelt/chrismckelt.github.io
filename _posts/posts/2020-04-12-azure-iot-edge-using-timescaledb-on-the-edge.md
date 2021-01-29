---
layout: post
category: posts
title: "Azure IoT Edge – using TimescaleDB on the Edge"
date: "2020-04-12"
categories: 
  - "net"
  - "azure"
  - "cloud"
  - "database"
  - "docker"
  - "iot"
  - "node"
  - "timescale"
---

# Series

[Part 1 - dotnet vs python vs node - temperature emission - who is cooler?](https://dev.to/chris_mckelt/azure-iot-edge-who-is-cooler-dotnet-node-or-python-369m)  
[Part 2 - Developing modules](https://dev.to/chris_mckelt/azure-iot-edge-developing-custom-modules-df3)  
[Part 3 - Custom Containers using Apache Nifi](https://dev.to/chris_mckelt/azure-iot-edge-3rd-party-containers-3mi3)  
[Part 4 - Custom Module using TimescaleDB](https://dev.to/chris_mckelt/azure-iot-edge-using-timescaledb-on-the-edge-2ec1)  
[Part 5 - Custom Module using Grafana](https://dev.to/chris_mckelt/azure-iot-edge-using-grafana-on-the-edge-26na)

# Intro

This is part 4 in a series starting [here](https://dev.to/chris_mckelt/azure-iot-edge-who-is-cooler-dotnet-node-or-python-369m) that runs through building an [Azure IOT Edge](https://docs.microsoft.com/en-us/azure/iot-edge/about-iot-edge) solution. This post will run through setting up [TimescaleDB](https://www.timescale.com/) to store data published from the dotnet, python and node temperature modules.

The code is located at: [https://github.com/chrismckelt/edgy](https://github.com/chrismckelt/edgy)

> [TimescaleDB](https://www.timescale.com/): An open-source database built for analysing
> 
> time-series data with the power and convenience of
> 
> SQL — on premise, at the edge or in the cloud.

## Steps to add the database

### 1\. add the [custom module](https://github.com/chrismckelt/edgy/tree/master/modules/TimescaleDb) 

![](images/79062247-060de280-7ccb-11ea-901d-7faa07663fd6.png)

### 2\. add the section to the [deployment file](https://github.com/chrismckelt/edgy/blob/master/deployment.debug.template.json)

Expose the internal port 5432 that TimescaleDB uses to 8081 for external container use

<script src="https://gist.github.com/chrismckelt/3e3da727c762c8bc038551a8ef683943.js"></script>

### 3\. create the [docker file](https://github.com/chrismckelt/edgy/blob/master/modules/TimescaleDb/Dockerfile.amd64.debug)

<script src="https://gist.github.com/chrismckelt/efe8e3ed3ae9a61a07a67b9d3454b2dd.js"></script>

### 4\. create the [database, login and schema](https://github.com/chrismckelt/edgy/blob/master/modules/TimescaleDb/init.sql)

<script src="https://gist.github.com/chrismckelt/f4e73f67a6903a1f4a0446065fdc6e78.js"></script>

### 5\. run the container and insert data from another module

<script src="https://gist.github.com/chrismckelt/bfe5ece31db7cd6db21d0eb5efdee339.js"></script>

######  select \* from "table\_001" where Isairconditioneron = 0 ORDER BY "Timestamp" DESC LIMIT 100;

![](images/79062131-078adb00-7cca-11ea-975e-6885c0ba70ce.png)

# Outro

Now we have data being saved into the database we can move onto displaying it visually via [Grafana](https://grafana.com/) in the next post.
