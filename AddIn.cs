﻿//************************************************************************************************
// Copyright © 2021 Steven M Cohn. All rights reserved.
//************************************************************************************************                

namespace OneNoteVanilla5
{
	using Extensibility;
	using Microsoft.Office.Core;
	using Microsoft.Office.Interop.OneNote;
	using System;
	using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Threading;
    using System.Xml.Linq;
    using OneNoteVanilla5.WPF;
    using MessageBox = System.Windows.Forms.MessageBox;
    using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Application is for Windows 10 and upwards only")]
    [ComVisible(true)]
	[Guid("4D86B2FD-0C2D-4610-8916-DE24C4BB70B5")] // change this!
	[ProgId("OneNoteVanilla5")] // change this!
	public class AddIn : IDTExtensibility2, IRibbonExtensibility
	{
		private Microsoft.Office.Interop.OneNote.Application onenote;


		// IDTExtensibility2...

		public void OnConnection(
			object Application, ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
		{
			onenote = (Microsoft.Office.Interop.OneNote.Application)Application;
		}

		public void OnDisconnection(ext_DisconnectMode RemoveMode, ref Array custom)
		{
			// required to shut down OneNote and release .NET
			// both of these lines are necessary along with the two lines in OnBeginShutdown
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public void OnAddInsUpdate(ref Array custom)
		{
			//
		}

		public void OnStartupComplete(ref Array custom)
		{
			// intialize your stuff here
		}


        public void OnBeginShutdown(ref Array custom)
		{
			// required to shut down OneNote
			// both of these lines are necessary along with the two lines in OnDisconnection
			onenote = null;
			System.Windows.Forms.Application.Exit();
		}


		// IRibbonExtensibility...

		public string GetCustomUI(string RibbonID)
		{
			return @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<customUI xmlns=""http://schemas.microsoft.com/office/2009/07/customui"">
  <ribbon>
    <tabs>
      <tab idMso=""TabHome"">
        <group id=""vanillaAddInGroup"" label=""Vanilla Add-in"">
          <button id=""helloButton"" imageMso=""HappyFace"" size=""large"" label=""Say Hello!"" onAction=""ShowWindow""/>
        </group>
      </tab>
    </tabs>
  </ribbon>
</customUI>";
		}


		// Ribbon handlers

		public void SayHello(IRibbonControl control)
		{
			onenote.GetHierarchy(
				onenote.Windows.CurrentWindow.CurrentNotebookId,
				HierarchyScope.hsSelf,
				out var xml,
				XMLSchema.xs2013);

			var root = XElement.Parse(xml);
			//var ns = root.GetNamespaceOfPrefix("one");

			var name = root.Attribute("name").Value;

			MessageBox.Show(
				$"Hello from {name}!",
				"Vanilla OneNote on .NET 8",
				MessageBoxButtons.OK, MessageBoxIcon.None,
				MessageBoxDefaultButton.Button1,
				MessageBoxOptions.DefaultDesktopOnly);
		}

		public void ShowWindow(IRibbonControl control)
		{
			// Start a STA thread to show the WPF window as COM thread is MTA which cannot access UI components since the components are not thread-safe 
			Thread thread = new Thread(() =>
			{
				try
				{
                    var window = new MainWindow(onenote);
                    window.ShowDialog();
                } catch (Exception e)
				{
                    MessageBox.Show(
					$"An exception occured: {e.Message}",
                    "Exception",
                    MessageBoxButtons.OK);
                }
			});
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			// Block the thread until dialog is closed to prevent users from editing OneNote until the dialog is closed
			thread.Join();
		}
	}
}
