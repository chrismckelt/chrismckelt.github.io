---
layout: post
category: posts
title: "Using Azure Pipelines to restore a production database to another environment"
date: "2019-08-28"
tags: azure sql 
---

Often we need a fresh copy of the production database in another environment (eg DEV/TEST/UAT). 

Previously this was a tedious task involving getting a backup file, copying it to another location, restoring the database.   Here is a solution to automate this process using Azure Pipelines.

### User Story

    Given a production database exists in subscription 1  
    When we do a release of the Azure Pipeline named ‘_Refresh Database – DEV’_  
    Then an copy of production is available in the DEV environment in subscription 2  
    And permissions are correct for the DEV environment

## Pipeline Overview

For each environment that you wish to restore into create an Azure Pipeline with 3 stages.

![image](https://user-images.githubusercontent.com/662868/120557358-5c3e6780-c430-11eb-981e-62d5bb843cbf.png)

Create variable groups that are **scoped to specific stages**

Each variable group contains deployment credentials that the specific stage will require to perform operations within the specific Azure Subscription.

![image](https://user-images.githubusercontent.com/662868/120557271-3fa22f80-c430-11eb-906c-2475ac31de34.png)


### Task 1 - Export

Create a PowerShell task to run a script and pass it the information for the production environment.

This runs against the production environment and creates a blog storage container that holds the exported BACPAC

![image](https://user-images.githubusercontent.com/662868/120557372-62ccdf00-c430-11eb-8ab3-8ee0d5566c69.png)

###### [View code for script production-export.ps1](https://gist.github.com/chrismckelt/cc3c2ea53d8500b7c02e3da43513cbae)

<script src="https://gist.github.com/chrismckelt/cc3c2ea53d8500b7c02e3da43513cbae.js"></script>

### Results in production once this script run should show the database BACPAC export

![image](https://user-images.githubusercontent.com/662868/120558018-67de5e00-c431-11eb-8368-fcd759e10a99.png)

### Task 2 – Import

Under the ‘import’ stage create a task that will import the BACPAC from the storage container in the production subscription.  This uses both production and the environment credentials.

![image](https://user-images.githubusercontent.com/662868/120557389-695b5680-c430-11eb-9c55-6f074e5a42d8.png)

###### [View code for script production-import.ps1](https://gist.github.com/chrismckelt/629f992935f9a6aa6701e2c69ae49358)


<script src="https://gist.github.com/chrismckelt/629f992935f9a6aa6701e2c69ae49358.js"></script>

### Task 3 – Sanitise

Create a 3rd task in the ‘Sanitise’ stage. 

This will scramble any  information you do not want in that environment (eg emails).

Also remove any Production SQL user account and replace them with environment specific

###### [View code for script sanitise](https://gist.github.com/chrismckelt/f1dcefb52db6e79b8e5514853067e774)

<script src="https://gist.github.com/chrismckelt/f1dcefb52db6e79b8e5514853067e774.js"></script>

* * *

## Tada!

Running the pipeline now copies the database to the DEV environment. Typically after this will run a software build which will automatically apply schema changes currently in DEV in the database. Happy restoring!

![image](https://user-images.githubusercontent.com/662868/120557416-76784580-c430-11eb-87af-44da7c102fd1.png)
