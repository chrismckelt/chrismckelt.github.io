---
layout: post
category: posts
title: "Software Development Best Practice (minimum)"
date: "2014-07-21"
categories: 
  - "net"
  - "agile"
  - "software"
---

# Source Control

To track versions and manage an environment from deployment to recovery, source control is essential.  Tracking changes, performing peer reviews before merging to the main master branch & the ability to rebuild/recover/revert to a previously known good state ensure quality throughout the process.

Example source control : [GitHub](https://github.com/)

# Continue Integration

Continuous Integration (CI) is a development practice that requires developers to integrate code into a shared repository several times a day. Each check-in is then verified by an automated build, allowing teams to detect problems early.

By integrating regularly, you can detect errors quickly, and locate them more easily.

Running new code through a pipeline of automated tests & peer review before reaching the test/uat environment ensures less regression errors & frustration for QA team.

[https://www.thoughtworks.com/continuous-integration](https://www.thoughtworks.com/continuous-integration "https://www.thoughtworks.com/continuous-integration")

Example CI : [AppVeyor](https://www.appveyor.com/)

# [Automated Tests (unit, integration, functional, acceptance, UI, subcutaneous)](https://www.atlassian.com/software-testing)

[Test Driven Development (TDD)](https://en.wikipedia.org/wiki/Test-driven_development)  involves coding test cases checking the newly implemented scenarios function as required (and ensure no regression when changes occur)

[http://www.bas36.com/2012/07/benefits-of-test-driven-development/](http://www.bas36.com/2012/07/benefits-of-test-driven-development/ "http://www.base36.com/2012/07/benefits-of-test-driven-development/")

TDD is an essential tool in a programmers toolkit & is essential is required for modern dev [positions](https://www.seek.com.au/jobs?keywords=tdd).
