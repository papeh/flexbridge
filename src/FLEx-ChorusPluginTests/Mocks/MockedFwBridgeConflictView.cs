﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FLEx_ChorusPlugin.Model;
using FLEx_ChorusPlugin.View;

namespace FLEx_ChorusPluginTests.Mocks
{
	internal class MockedFwBridgeConflictView : UserControl, IFwBridgeView
	{
		private readonly IProjectView _mockedProjectView = new MockedConflictProjectView();

		//internal void RaiseProjectSelected(LanguageProject selectedProject)
		//{
		//    ProjectSelected(this, new ProjectEventArgs(selectedProject));
		//}

		//internal void RaiseSynchronizeProject()
		//{
		//    SynchronizeProject(this, new EventArgs());
		//}

		#region Implementation of IFwBridgeView

		public event ProjectSelectedEventHandler ProjectSelected;
		public event SynchronizeProjectEventHandler SynchronizeProject;

		public IEnumerable<LanguageProject> Projects { set; internal get; }

		public IProjectView ProjectView
		{
			get { return _mockedProjectView; }
		}

		public void EnableSendReceiveControls(bool enableSendReceiveBtn, bool makeWarningsVisible)
		{
			throw new NotImplementedException();
		}

		internal bool EnableSendReceive { set; get; }

		internal bool WarningsAreVisible { set; get; }

		#endregion
	}
}