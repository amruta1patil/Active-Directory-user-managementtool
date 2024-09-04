namespace ADCreation
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
            this.btnSign_in = new System.Windows.Forms.Button();
            this.btnReSet = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // btnSign_in
            // 
            this.btnSign_in.BackColor = System.Drawing.Color.Teal;
            this.btnSign_in.ForeColor = System.Drawing.Color.White;
            this.btnSign_in.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSign_in.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnSign_in.Location = new System.Drawing.Point(100, 100);
            this.btnSign_in.Name = "btnSign_in";
            this.btnSign_in.Size = new System.Drawing.Size(120, 40);
            this.btnSign_in.TabIndex = 0;
            this.btnSign_in.Text = "Sign In";
            this.btnSign_in.UseVisualStyleBackColor = false;
            this.btnSign_in.Click += new System.EventHandler(this.btnSign_in_Click);

            // 
            // btnReSet
            // 
            this.btnReSet.BackColor = System.Drawing.Color.DarkOrange;
            this.btnReSet.ForeColor = System.Drawing.Color.White;
            this.btnReSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReSet.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnReSet.Location = new System.Drawing.Point(500, 100);
            this.btnReSet.Name = "btnReSet";
            this.btnReSet.Size = new System.Drawing.Size(120, 40);
            this.btnReSet.TabIndex = 1;
            this.btnReSet.Text = "Reset";
            this.btnReSet.UseVisualStyleBackColor = false;
            this.btnReSet.Click += new System.EventHandler(this.btnReSet_Click);

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 300);
            this.Controls.Add(this.btnReSet);
            this.Controls.Add(this.btnSign_in);
            this.Name = "Form1";
            this.Text = "Login Form";
            this.ResumeLayout(false);
        }


        #endregion

        private System.Windows.Forms.Button btnSign_in;
        private System.Windows.Forms.Button btnReSet;
    }
}

