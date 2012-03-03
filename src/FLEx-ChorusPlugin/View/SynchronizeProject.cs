﻿using System.IO;
using System.Windows.Forms;
using Chorus;
using Chorus.UI.Sync;
using FLEx_ChorusPlugin.Infrastructure.DomainServices;
using FLEx_ChorusPlugin.Model;

namespace FLEx_ChorusPlugin.View
{
	internal sealed class SynchronizeProject : ISynchronizeProject
	{
		#region Implementation of ISynchronizeProject

		public void SynchronizeFieldWorksProject(Form parent, ChorusSystem chorusSystem, LanguageProject langProject)
		{
			// Add the 'lock' file to keep FW apps from starting up at such an inopportune moment.
			var lockPathname = Path.Combine(langProject.DirectoryName, langProject.Name + ".fwdata.lock");

			try
			{
				File.WriteAllText(lockPathname, "");

				var origPathname = Path.Combine(langProject.DirectoryName, langProject.Name + ".fwdata");
				// Break up into smaller files.
				MultipleFileServices.PushHumptyOffTheWall(origPathname);

				// Do the Chorus business.
				using (var syncDlg = (SyncDialog)chorusSystem.WinForms.CreateSynchronizationDialog())
				{
					// Chorus does it in ths order:
					// local Commit
					// Pull
					// Merge (Only if anything came in with the pull from other sources)
					// Push
					syncDlg.SyncOptions.DoPullFromOthers = true;
					syncDlg.SyncOptions.DoMergeWithOthers = true;
					syncDlg.SyncOptions.DoSendToOthers = true;
					syncDlg.ShowDialog(parent);

					if (syncDlg.SyncResult.DidGetChangesFromOthers)
					{
						// Put Humpty together again.
						MultipleFileServices.PutHumptyTogetherAgain(origPathname);
					}
				}
			}
			finally
			{
				if (File.Exists(lockPathname))
					File.Delete(lockPathname);
			}
		}

		#endregion

		public void SynchronizeFieldWorksProject(IFwBridgeController controller)
		{
			SynchronizeFieldWorksProject(controller.MainForm, controller.ChorusSystem, controller.CurrentProject);
		}
	}
}