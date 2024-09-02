
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
            components = new System.ComponentModel.Container();
            tmr_ProcessCheck = new System.Windows.Forms.Timer(components);
            txt_CustomServerName = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            btn_Save = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // tmr_ProcessCheck
            // 
            tmr_ProcessCheck.Enabled = true;
            tmr_ProcessCheck.Interval = 5000;
            tmr_ProcessCheck.Tick += tmr_ProcessCheck_Tick;
            // 
            // txt_CustomServerName
            // 
            txt_CustomServerName.Location = new System.Drawing.Point(2, 27);
            txt_CustomServerName.Name = "txt_CustomServerName";
            txt_CustomServerName.Size = new System.Drawing.Size(100, 23);
            txt_CustomServerName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(2, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(119, 15);
            label1.TabIndex = 1;
            label1.Text = "Custom Server Name";
            // 
            // btn_Save
            // 
            btn_Save.Location = new System.Drawing.Point(108, 26);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new System.Drawing.Size(75, 23);
            btn_Save.TabIndex = 2;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // frm_Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(226, 61);
            Controls.Add(btn_Save);
            Controls.Add(label1);
            Controls.Add(txt_CustomServerName);
            MaximumSize = new System.Drawing.Size(242, 100);
            MinimumSize = new System.Drawing.Size(242, 100);
            Name = "frm_Main";
            Text = "Final Fantasy XI Presence";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer tmr_ProcessCheck;
        private System.Windows.Forms.TextBox txt_CustomServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Save;
    }
}

