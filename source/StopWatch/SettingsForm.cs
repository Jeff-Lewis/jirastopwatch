﻿/**************************************************************************
Copyright 2015 Carsten Gehling

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**************************************************************************/
using System.Linq;
using System.Windows.Forms;

namespace StopWatch
{


    internal partial class SettingsForm : Form
    {
        #region public members
        public Settings settings { get; private set; }
        #endregion


        #region public methods
        public SettingsForm(Settings settings)
        {
            this.settings = settings;

            InitializeComponent();

            tbJiraBaseUrl.Text = this.settings.JiraBaseUrl;
            numIssueCount.Value = this.settings.IssueCount;
            cbAlwaysOnTop.Checked = this.settings.AlwaysOnTop;
            cbMinimizeToTray.Checked = this.settings.MinimizeToTray;
            cbTimerEditable.Checked = this.settings.TimerEditable;

            cbSaveTimerState.DisplayMember = "Text";
            cbSaveTimerState.ValueMember = "Value";
            cbSaveTimerState.DataSource = new[]
            {
                new { Text = "Reset all timers on exit", Value = SaveTimerSetting.NoSave },
                new { Text = "Save current timetracking, pause active timer", Value = SaveTimerSetting.SavePause },
                new { Text = "Save current timetracking, active timer continues", Value = SaveTimerSetting.SaveRunActive }
            };
            cbSaveTimerState.SelectedValue = this.settings.SaveTimerState;

            cbPauseOnSessionLock.DisplayMember = "Text";
            cbPauseOnSessionLock.ValueMember = "Value";
            cbPauseOnSessionLock.DataSource = new[]
            {
                new { Text = "No pause", Value = PauseAndResumeSetting.NoPause },
                new { Text = "Pause active timer", Value = PauseAndResumeSetting.Pause },
                new { Text = "Pause and resume on unlock", Value = PauseAndResumeSetting.PauseAndResume }
            };
            cbPauseOnSessionLock.SelectedValue = this.settings.PauseOnSessionLock;
        }
        #endregion


        #region private eventhandlers
        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.settings.JiraBaseUrl = tbJiraBaseUrl.Text;
                this.settings.IssueCount = (int)numIssueCount.Value;
                this.settings.AlwaysOnTop = cbAlwaysOnTop.Checked;
                this.settings.MinimizeToTray = cbMinimizeToTray.Checked;
                this.settings.TimerEditable = cbTimerEditable.Checked;

                this.settings.SaveTimerState = (SaveTimerSetting)cbSaveTimerState.SelectedValue;
                this.settings.PauseOnSessionLock = (PauseAndResumeSetting)cbPauseOnSessionLock.SelectedValue;
            }
        }


        private void btnAbout_Click(object sender, System.EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }
        #endregion
    }
}