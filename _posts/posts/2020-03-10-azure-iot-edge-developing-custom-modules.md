---
layout: post
title: "Azure IoT Edge - Developing custom modules"
category: posts
date: "2020-03-10"
categories: 
  - "net"
  - "azure"
  - "cloud"
  - "code"
  - "docker"
  - "iot"
  - "node"
  - "powershell"
  - "python"
---

# Series

[Part 1 - dotnet vs python vs node - temperature emission - who is cooler?](https://dev.to/chris_mckelt/azure-iot-edge-who-is-cooler-dotnet-node-or-python-369m)  
[Part 2 - Developing modules](https://dev.to/chris_mckelt/azure-iot-edge-developing-custom-modules-df3)  
[Part 3 - Custom Containers using Apache Nifi](https://dev.to/chris_mckelt/azure-iot-edge-3rd-party-containers-3mi3)  
[Part 4 - Custom Module using TimescaleDB](https://dev.to/chris_mckelt/azure-iot-edge-using-timescaledb-on-the-edge-2ec1)  
[Part 5 - Custom Module using Grafana](https://dev.to/chris_mckelt/azure-iot-edge-using-grafana-on-the-edge-26na)

  

# Intro

This is part 2 in a series starting [here](http://blog.mckelt.com/2020/02/13/azure-iot-edge-creating-an-edge-reporting-solution/) running through an Azure IoT Edge demo solution located at: [https://github.com/chrismckelt/edgy](https://github.com/chrismckelt/edgy)

This part will cover developing and running custom modules written in C#, Python and NodeJS.

It will focus on commands available in the VS Code interface rather than command line arguments as seen at: [https://aka.ms/iotedgedev](https://aka.ms/iotedgedev)

## Azure IoT SDKs

Azure IoT Edge has a number of [SDKs](https://github.com/Azure/azure-iot-sdks) for module development in your favourite language.  The [SDK code](https://github.com/Azure/azure-iot-sdks) will handle setting up environment variables and provide the boiler plate code necessary to send and receive messages using multiple [protocols & channels  (e.g. MQTT, AMQP)](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-protocols)

## [Setting up VS Code to run the local simulator](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-protocols)

Given the solution is cloned locally and Azure is setup with our IOT Hub we can configure a device to act as a local simulator.

1\. Select IOT Hub & choose your IOT Hub

![](images/77226837-4390bb80-6bb6-11ea-8a88-e03f10cc02eb.png)

2\. Create IOT Edge Device - I have named my local device ‘_LocalSimulator_ ‘

![](images/77226840-4ab7c980-6bb6-11ea-8380-b0656965b968.png)

3\. Setup IOT Edge Simulator – this will create you edge certs

![](images/77226898-bd28a980-6bb6-11ea-99cb-3f5a35dce53a.png)

This will generate and install certificates for local development in the following folder & also install the for you _note://run VS Code as admin for this_

![](images/77226845-5905e580-6bb6-11ea-881e-46ca8640529b.png)

## Creating a module for our solution

Right click on the modules folder and select ‘Add IoT Edge Module’.

This will then ask a module name & language (C,C#,Java,Node.js, Python)

![](images/77226850-615e2080-6bb6-11ea-9775-f2a18775b707.png)

I have created numerous custom modules for this solution. For this post we focus on the below 6

![](images/77226855-67ec9800-6bb6-11ea-9af9-3bf33e1fbc44.png)

## Data Generators

The demo code shows 3 ‘data generator’ modules written in C#, Python & Node JS.

Each module publishes a message every second simulating temperature capture.  Properties of the sent message are:

<script src="https://gist.github.com/chrismckelt/0299fe4f6f81f7bebdb2792cec935508.js"></script>

In C# connecting to the Edge Hub and sending messages can be seen in the following code:

<script src="https://gist.github.com/chrismckelt/4a0769a626f433fb25903318b88c5311.js"></script>

## Data Recorders

3 modules in matching languages subscribe to their respective modules published messages

<script src="https://gist.github.com/chrismckelt/9b1a3923f31a657fa3a6ff9f1a9e417a.js"></script>

Messages received are deserialized from JSON format to a POCO and then saved in a database.

<script src="https://gist.github.com/chrismckelt/0fc37bda378ea94f694a2de8c1ca7a6e.js"></script>

## Routes

In order to route messages between modules we use the inbuilt route system in our [deployment template](https://github.com/chrismckelt/edgy/blob/master/deployment.debug.template.json) file.

![](images/77226859-75a21d80-6bb6-11ea-9723-f7f977688da2.png)

## Web App – Viewer module

Finally to view the messages from a web page I have modified an [existing demo to](https://github.com/Azure-Samples/iot-edge-hmi-module) that uses SignalR view all messages sent to it from the below routes:

![](images/77226864-7a66d180-6bb6-11ea-8e78-30165268a5d3.png)

When running the solution you can view all published messages on the web page below

![](images/77226868-7f2b8580-6bb6-11ea-991e-f88f499d70b7.png)

## Outro

Here was a basic overview of an demo solution to create and build your own custom IoT Edge modules.

Next we will introduce an existing docker container ([https://nifi.apache.org/)](https://nifi.apache.org/)) to act as a data orchestrator. This will subscribe to all 'Payload' messages and publish a message to ‘turn off’ the air conditioner when the temperature is too high.
