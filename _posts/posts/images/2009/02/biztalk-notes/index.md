---
title: "Biztalk notes"
date: "2009-02-12"
categories: 
  - "notes"
---

**Databases**

- SSODB -- Enterprise Single Sign-On database
- BizTalkRuleEngineDB -- Repository for your business rules
- BizTalkMsgBoxDb -- Storage for a multitude of BizTalk activities, notably messages
- BizTalkMgmtDb -- Database for server meta data
- BizTalkHwsDb -- Human Workflow Services storage database
- BizTalkEdiDb -- State data for the EDI adapter
- BizTalkDTADb -- BizTalk tracking engine storage
- BizTalkStarSchema -- Staging, dimension, and measure tables
- BAMPrimaryImport -- Raw tracking data for BAM
- BAMArchive -- Archive for older business activity
- BAMAlertsNSMain -- Notification services for BAM monitoring
- BAMAlertsApplication -- Alert information for BAM notifications

**Orchestration Tools**

Group

Allows you to collect various shapes together into a collapsible region, in much the same way that code regions in Visual Studio allow you to collapse code. If your orchestration becomes large and unwieldy, consider organizing items with groups.

Send

Provides a mechanism for sending out a message.

Receive

Provides a mechanism for receiving a message.

Port

Provides the liaison between the BizTalk messages and the orchestration.

Role Link

Provides an abstract method of dynamically selecting which of your trading partners you would like to send or receive a message.

Transform

Allows you to map a message.

Message Assignment

Nested within a Construct Message shape, allows you to create a message and assign values to it.

Construct

Message Creates a new instance of a message.

Call Orchestration

Synchronously calls another BizTalk orchestration. Start Orchestration

Asynchronously calls another BizTalk orchestration.

Call Rules

Makes a call to a business policy.

Expression

Allows you to create an “in-line” C#-like language (XLang) coding block that can execute against the message.

Decide

Allows you to implement conditional logic in your orchestration flow.

Delay

Instructs the orchestration to pause for a set amount of time.

Listen

Provides a conditional branching mechanism that “listens” for the end of a Delay shape or the input of a message and turns flow control over to the branch that arrives first.

Parallel Actions

Gives you the opportunity to execute shapes in parallel to each other.

Loop

Provides a while loop within the orchestration flow.

Scope

Similar to coding scope, restricts transactions and error handling to a specified region.

Throw Exception

Throws an exception for bubbled-up error handling.

Compensate

Allows you to “undo” the effects of a transaction that has run its course by returning or resetting any resources that have been modified.

Suspend

Freezes an orchestration and bubbles up an error. While captured by a Suspend shape, the message will become resumable, as needed.

Terminate

Stops the orchestration and bubbles up an error. The message will be subsequently suspended; however, unlike with the Suspend shape, this message will be not be resumable.
