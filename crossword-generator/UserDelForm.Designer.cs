
namespace crossword_generator
{
    partial class UserDelForm
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
            this.ConfirmCheckBox = new System.Windows.Forms.CheckBox();
            this.DelButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConfirmCheckBox
            // 
            this.ConfirmCheckBox.AutoSize = true;
            this.ConfirmCheckBox.Location = new System.Drawing.Point(101, 45);
            this.ConfirmCheckBox.Name = "ConfirmCheckBox";
            this.ConfirmCheckBox.Size = new System.Drawing.Size(142, 17);
            this.ConfirmCheckBox.TabIndex = 0;
            this.ConfirmCheckBox.Text = "Подтвердить удаление";
            this.ConfirmCheckBox.UseVisualStyleBackColor = true;
            // 
            // DelButton
            // 
            this.DelButton.Location = new System.Drawing.Point(57, 107);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(75, 23);
            this.DelButton.TabIndex = 1;
            this.DelButton.Text = "Ок";
            this.DelButton.UseVisualStyleBackColor = true;
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(200, 106);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // UserDelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 158);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DelButton);
            this.Controls.Add(this.ConfirmCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserDelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ConfirmCheckBox;
        private System.Windows.Forms.Button DelButton;
        private System.Windows.Forms.Button CancelButton;
    }
}