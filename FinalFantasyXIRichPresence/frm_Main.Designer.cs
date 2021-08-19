
namespace FinalFantasyXIRichPresence
{
    partial class frm_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmr_ProcessCheck = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmr_ProcessCheck
            // 
            this.tmr_ProcessCheck.Enabled = true;
            this.tmr_ProcessCheck.Interval = 5000;
            this.tmr_ProcessCheck.Tick += new System.EventHandler(this.tmr_ProcessCheck_Tick);
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 6);
            this.MaximumSize = new System.Drawing.Size(242, 45);
            this.MinimumSize = new System.Drawing.Size(242, 45);
            this.Name = "frm_Main";
            this.Text = "Final Fantasy XI Presence";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmr_ProcessCheck;
    }
}

