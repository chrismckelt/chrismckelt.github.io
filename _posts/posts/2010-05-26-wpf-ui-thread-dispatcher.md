---
title: "WPF UI Thread Dispatcher"
date: "2010-05-26"
categories: 
  - "net"
  - "code"
---

#### A simple implemention for calling asych methods from the UI

 

## Examples

 

1.

dispatcher.ExecuteOnMainUIThread(CommandManager.InvalidateRequerySuggested);

 

2.

dispatcher.Execute(() =>
                                        {
                                           SomeLongRunningMethodHere();
                                        });

 

## The interface

using System;

namespace Mvvm { public interface IDispatcher { void Execute(Action action);

void ExecuteOnMainUIThread(Action action); } }

## Synchronous for use in Testing

using System;

namespace Mvvm { public class SynchronousDispatcher : IDispatcher { public void Execute(Action action) { action(); }

public void ExecuteOnMainUIThread(Action action) { action(); } }

}

## Asynchronous for use by the application at run time

using System;

namespace Mvvm { using System.Windows; using System.Windows.Threading;

public class AsynchronousDispatcher : IDispatcher { public void Execute(Action action) { action.BeginInvoke(CallBack, action); }

public void ExecuteOnMainUIThread(Action action) { Dispatcher dispatcher;

if (Application.Current != null) { dispatcher = Application.Current.Dispatcher; } else { dispatcher = Dispatcher.CurrentDispatcher; }

dispatcher.Invoke(action); }

private void CallBack(IAsyncResult result) { try { ((Action)result.AsyncState).EndInvoke(result); } catch (Exception ex) { // Need to raise the exception on the main thread ExecuteOnMainUIThread(() => { throw ex; } ); } finally { result.AsyncWaitHandle.Close(); }

} } }
