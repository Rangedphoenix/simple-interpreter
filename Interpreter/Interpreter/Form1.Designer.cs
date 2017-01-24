namespace Interpreter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.CommandBox = new System.Windows.Forms.TextBox();
            this.ExeButton = new System.Windows.Forms.Button();
            this.DebugLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OutputLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Command:";
            // 
            // CommandBox
            // 
            this.CommandBox.Location = new System.Drawing.Point(76, 20);
            this.CommandBox.Name = "CommandBox";
            this.CommandBox.Size = new System.Drawing.Size(717, 20);
            this.CommandBox.TabIndex = 1;
            // 
            // ExeButton
            // 
            this.ExeButton.Location = new System.Drawing.Point(799, 17);
            this.ExeButton.Name = "ExeButton";
            this.ExeButton.Size = new System.Drawing.Size(75, 23);
            this.ExeButton.TabIndex = 2;
            this.ExeButton.Text = "Execute";
            this.ExeButton.UseVisualStyleBackColor = true;
            this.ExeButton.Click += new System.EventHandler(this.ExeButton_Click);
            // 
            // DebugLog
            // 
            this.DebugLog.Location = new System.Drawing.Point(12, 89);
            this.DebugLog.Multiline = true;
            this.DebugLog.Name = "DebugLog";
            this.DebugLog.ReadOnly = true;
            this.DebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DebugLog.Size = new System.Drawing.Size(851, 196);
            this.DebugLog.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Debug Log:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output:";
            // 
            // OutputLog
            // 
            this.OutputLog.Location = new System.Drawing.Point(12, 313);
            this.OutputLog.Multiline = true;
            this.OutputLog.Name = "OutputLog";
            this.OutputLog.ReadOnly = true;
            this.OutputLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputLog.Size = new System.Drawing.Size(851, 196);
            this.OutputLog.TabIndex = 6;
            // 
            // Form1
            // 
            this.AcceptButton = this.ExeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 530);
            this.Controls.Add(this.OutputLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DebugLog);
            this.Controls.Add(this.ExeButton);
            this.Controls.Add(this.CommandBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Interpreter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CommandBox;
        private System.Windows.Forms.Button ExeButton;
        private System.Windows.Forms.TextBox DebugLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OutputLog;
    }
}

