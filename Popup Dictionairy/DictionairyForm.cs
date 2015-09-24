using PopupDictionairy.App.Controller;
using System;
using System.Windows.Forms;

namespace PopupDictionairy.App
{
    public partial class DictionairyForm : Form
    {
        private int interval;
        private Timer timer1;
        private TranslationsController controller;

        public DictionairyForm()
        {
            interval = SettingsManager.Current.QuestionIntervalSeconds;
            timer1 = new Timer();
            timer1.Interval = interval;
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            SettingsManager.Current.QuestionIntervalChanged += Current_QuestionIntervalChanged;
            InitializeComponent();

            FileSystemPersistenceProvider persisteneProvider = new FileSystemPersistenceProvider(Application.UserAppDataPath);
            controller = new TranslationsController(persisteneProvider);

            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClicked;
            notifyIcon1.BalloonTipTitle = "Popup Dictionairy";
            notifyIcon1.BalloonTipText = String.Format("The program will popup every {0} seconds. Click here if you want to change the interval.", (interval / 1000).ToString());
        }

        private void Current_QuestionIntervalChanged(object sender, int e)
        {
            timer1.Stop();
            timer1.Interval = e;
            interval = e;
            notifyIcon1.BalloonTipText = String.Format("The program will popup every {0} seconds. Click here if you want to change the interval.", (interval / 1000).ToString());
            timer1.Start();
        }

        private void DictionairyForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
                timer1.Start();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            settingsToolStripMenuItem1_Click(null, null);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            PreferencesForm pf = new PreferencesForm();
            pf.ShowDialog();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                timer1.Stop();
                //this.WindowState = FormWindowState.Maximized;
                //this.Show();
                //this.Activate();

                //TODO: QuestionsPerSession
                //QuestionForm qf = new QuestionForm(controller.GetTranslationsForSession(SettingsManager.Current.QuestionPerSession);

                QuestionForm qf = new QuestionForm(controller.GetTranslationsForSession(2));

                qf.FormClosed += (d, a) => { timer1.Start(); };
                qf.Show();
                qf.Activate();
            }
        }

        private void dictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TranslationsForm tf = new TranslationsForm(controller);
            tf.Show();
        }
    }
}