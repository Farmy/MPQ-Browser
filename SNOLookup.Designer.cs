namespace MPQBrowser
{
    partial class SNOLookup
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
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.SnoInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(147, 57);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(61, 25);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok.Location = new System.Drawing.Point(214, 57);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(61, 25);
            this.Ok.TabIndex = 1;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // SnoInput
            // 
            this.SnoInput.Location = new System.Drawing.Point(23, 22);
            this.SnoInput.Name = "SnoInput";
            this.SnoInput.Size = new System.Drawing.Size(252, 20);
            this.SnoInput.TabIndex = 0;
            // 
            // SNOLookup
            // 
            this.AcceptButton = this.Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(293, 98);
            this.Controls.Add(this.SnoInput);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SNOLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SNOLookup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.TextBox SnoInput;
    }
}