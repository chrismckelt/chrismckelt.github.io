---
title: "Value Tests --> Eliminate Toil"
date: "2015-07-10"
categories: 
  - "tdd-bdd"
---

Having drank the TDD cool aid over the years I went deep.  BDD with Fitnesse, SpecFlow. Maximum code coverage %. Days fixing up UI tests. Ensuring this calls that – all code was covered.

Today here is how I calculate a tests value.

1. Will it help me discover a design?
2. Is the functionality well known? (is this feature being tested to see if its used/needed)
3. Will it stop hard to find bugs in the future?
4. Is its value worth maintaining over time?
5. Will the next person be able to understand and appreciate this test?

If YES then write the test!

> From the Google SRE Book - dont 'Automate Everything', instead 'Eliminate Toil' - where toil has some/all of these attributes.

**Manual** This includes work such as manually running a script that automates some task. Running a script may be quicker than manually executing each step in the script, but the hands-on time a human spends running that script (not the elapsed time) is still toil time. **Repetitive** If you’re performing a task for the first time ever, or even the second time, this work is not toil. Toil is work you do over and over. If you’re solving a novel problem or inventing a new solution, this work is not toil. **Automatable** If a machine could accomplish the task just as well as a human, or the need for the task could be designed away, that task is toil. If human judgment is essential for the task, there’s a good chance it’s not toil. **Tactical** Toil is interrupt-driven and reactive, rather than strategy-driven and proactive. Handling pager alerts is toil.We may never be able to eliminate this type of work completely, but we have to continually work toward minimizing it. No enduring value If your service remains in the same state after you have finished a task, the task was probably toil. If the task produced a permanent improvement in your service, it probably wasn’t toil, even if some amount of grunt work—such as digging into legacy code and configurations and straightening them out—was involved. **O(n) with service growth** If the work involved in a task scales up linearly with service size, traffic volume, or user count, that task is probably toil. An ideally managed and designed service can grow by at least one order of magnitude with zero additional work, other than some one-time efforts to add resources.

ref: https://landing.google.com/sre/book/chapters/eliminating-toil.html
