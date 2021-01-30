---
layout: post
category: posts
title: "Impersonation in Microsoft Dot Net"
date: "2008-07-28"
categories: 
  - "net"
---

_**Usage**_

string domain = "ExampleDomain";
string userName = "ExampleUserName";
string password = "ExamplePassword"; 

using (Impersonation impersonation = new Impersonation(domain, userName, password))
{
 // impersonation occuring in here 
         Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
} 

_**Implementation**_

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace ExampleNameSpace

{
     public class Impersonation : IDisposable
        {
            private bool \_disposed = false;
            private WindowsImpersonationContext \_wc;

            #region Win32 Interop
            // Win32 declarations (constants, enumerations, functions etc.)
            public const int LOGON32\_LOGON\_INTERACTIVE = 2;
            public const int LOGON32\_PROVIDER\_DEFAULT = 0;

            \[DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi, SetLastError = true)\]
            public static extern int CloseHandle(
                IntPtr handle);

            \[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)\]
            public static extern int DuplicateToken(IntPtr hToken,
                int impersonationLevel,
                ref IntPtr hNewToken);

            \[DllImport("advapi32.dll")\]
            public static extern int LogonUserA(String lpszUserName,
                String lpszDomain,
                String lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                ref IntPtr phToken);

            \[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)\]
            public static extern bool RevertToSelf();
            #endregion

            /// <summary>
            /// Constructor begins impersonation based on user credentials passed in. 
            /// </summary>
            /// <param name="domain">Windows Domain</param>
            /// <param name="userName">Windows username</param>
            /// <param name="password">Windows password</param>
            public Impersonation(string domain, string userName, string password)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    \_wc = WindowsIdentity.GetCurrent().Impersonate();
                    ImpersonateValidUser(domain, userName, password);
                }
            }

            /// <summary>
            /// This destructor will run only if the Dispose method does not get called.
            /// </summary>
            ~Impersonation()
            {
                Dispose(false);
            }

            /// <summary>
            /// Dispose(bool disposing) executes in two distinct scenarios. If disposing equals true, the method
            /// has been called directly or indirectly by a user's code. Managed and unmanaged resources can be
            /// disposed. If disposing equals false, the method has been called by the runtime from inside the
            /// finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
            /// </summary>
            /// <param name="disposing">True if function called from user code, false if called from finalizer.</param>
            private void Dispose(bool disposing)
            {
                // Check to see if Dispose has already been called
                if (!\_disposed)
                {
                    // If disposing equals true, dispose all managed and unmanaged resources
                    if (disposing)
                    {
                        // Dispose managed resources (there are none)
                    }

                    // Clean up unmanaged resources here (there are none)

                    // Reset impersonation
                    UndoImpersonation();
                }
                \_disposed = true;
            }

            /// <summary>
            /// Implement IDisposable.
            /// </summary>
            public void Dispose()
            {
                // Clean up this object
                Dispose(true);

                // Take this object off the finalization queue and prevent finalization code for this object
                // from executing a second time.
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// This function can be used to impersonate a specific user while a section of code is run.
            /// This code is taken from the Microsoft Knowledge Base, article KB30615, "How to implement
            /// impersonation in an ASP.NET application".
            /// </summary>
            /// <param name="domain">Windows domain</param>
            /// <param name="userName">The user that is being impersonated.  This can be a user SAM account
            /// name or a user principal name. Whether or not UPN is selected is detected by looking for an
            /// occurrence of the @ character in userName.</param>
            /// <param name="password">The user's password.</param>
            /// <returns>True if impersonation works, false if impersonation fails.</returns>
            \[PermissionSet(SecurityAction.Demand, Name = "FullTrust")\]
            public bool ImpersonateValidUser(string domain, string userName, string password)
            {
                WindowsIdentity tempWindowsIdentity;
                IntPtr token = IntPtr.Zero;
                IntPtr tokenDuplicate = IntPtr.Zero;

                if (RevertToSelf())
                {   
                    if (LogonUserA(userName, domain, password, LOGON32\_LOGON\_INTERACTIVE, LOGON32\_PROVIDER\_DEFAULT, ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            \_wc = tempWindowsIdentity.Impersonate();

                            if (\_wc != null)
                            {
                                CloseHandle(token);
                                CloseHandle(tokenDuplicate);
                                return true;
                            }
                        }
                    }
                }

                if (token != IntPtr.Zero)
                    CloseHandle(token);
                if (tokenDuplicate != IntPtr.Zero)
                    CloseHandle(tokenDuplicate);
                return false;
            }

            /// <summary>
            /// Called after ImpersonateValidUser (see above).
            /// </summary>
            /// <param name="impersonationContext">An object that represents the Windows user prior to an
            /// impersonation operation.</param>
            public void UndoImpersonation()
            {
                if (\_wc != null)
                    \_wc.Undo();
            }
        } 
 
}
