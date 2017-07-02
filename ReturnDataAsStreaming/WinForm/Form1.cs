using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Data.Access.Layer.Core;
using Data.Access.Layer.Model;

namespace WinForm
{
    public partial class Form1 : Form
    {
        private UserRepository _userRepository;

        private int BufferSize = 10;

        private List<User> _users = new List<User>();
        public Form1()
        {
            InitializeComponent();
            this._userRepository = new UserRepository();
            this.Worker.WorkerReportsProgress = true;
            this.btn_Cancel.Enabled = false;

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (!this.Worker.IsBusy)
            {
                this.grd_Result.Rows.Clear();
                Worker.RunWorkerAsync(this.txt_Search.Text);
                btn_Search.Enabled = false;
                this.btn_Cancel.Enabled = true;
            }

        }

        private void Worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var data = new List<User>();
            var count = 1;

            foreach (var user in _userRepository.GetUsers(e.Argument.ToString()))
            {
                data.Add(user);

                //if (data.Count % BufferSize != 0) continue;
                Worker.ReportProgress(count, data.ToArray());
                data.Clear();
                count++;
            }

            Worker.ReportProgress(0, data.ToArray());
        }

        private void Worker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            var data = e.UserState as IEnumerable<User>;

            if (data == null) return;

            var index = Math.Max(this.grd_Result.FirstDisplayedScrollingRowIndex, 0);

            this._users.AddRange(data);
            this.lbl_result.Text = $"Loaded {this._users.Count} rows";
            this.grd_Result.FirstDisplayedScrollingRowIndex = index;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!Worker.IsBusy) return;

            Worker.CancelAsync();
            btn_Cancel.Enabled = false;
            btn_Search.Enabled = true;
        }

        private void Worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.lbl_result.Text = "Finish";
                return;
            }


            if (e.Error != null)
            {
                this.lbl_result.Text = e.Error.Message;
                return;
            }

            this.lbl_result.Text = "All records was loaded!";
        }
    }
}
