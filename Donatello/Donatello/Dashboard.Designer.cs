using System;
using System.Windows.Forms;
namespace Donatello
{
    partial class Dashboard
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
            this.pnl_Login = new System.Windows.Forms.Panel();
            this.btn_CreateAcc = new System.Windows.Forms.Button();
            this.btn_Login = new System.Windows.Forms.Button();
            this.passBox = new Donatello.FocusTextBox();
            this.emailBox = new Donatello.FocusTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_NewAccount = new System.Windows.Forms.Panel();
            this.btn_ExistingAccount = new System.Windows.Forms.Button();
            this.new_repeatPasswordBox = new Donatello.FocusTextBox();
            this.btn_NewAccount = new System.Windows.Forms.Button();
            this.new_passwordBox = new Donatello.FocusTextBox();
            this.new_emailBox = new Donatello.FocusTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_AccountDetails = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.new_dobBox = new System.Windows.Forms.DateTimePicker();
            this.btn_Start = new System.Windows.Forms.Button();
            this.new_nameBox = new Donatello.FocusTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_Login.SuspendLayout();
            this.pnl_NewAccount.SuspendLayout();
            this.pnl_AccountDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Login
            // 
            this.pnl_Login.Controls.Add(this.btn_CreateAcc);
            this.pnl_Login.Controls.Add(this.btn_Login);
            this.pnl_Login.Controls.Add(this.passBox);
            this.pnl_Login.Controls.Add(this.emailBox);
            this.pnl_Login.Controls.Add(this.label1);
            this.pnl_Login.Location = new System.Drawing.Point(123, 12);
            this.pnl_Login.Name = "pnl_Login";
            this.pnl_Login.Size = new System.Drawing.Size(325, 285);
            this.pnl_Login.TabIndex = 0;
            // 
            // btn_CreateAcc
            // 
            this.btn_CreateAcc.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateAcc.Location = new System.Drawing.Point(12, 214);
            this.btn_CreateAcc.Name = "btn_CreateAcc";
            this.btn_CreateAcc.Size = new System.Drawing.Size(94, 58);
            this.btn_CreateAcc.TabIndex = 3;
            this.btn_CreateAcc.Text = "Create Account";
            this.btn_CreateAcc.UseVisualStyleBackColor = true;
            this.btn_CreateAcc.Click += new System.EventHandler(this.btn_CreateAcc_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Login.Location = new System.Drawing.Point(214, 214);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(94, 58);
            this.btn_Login.TabIndex = 0;
            this.btn_Login.Text = "Go";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // passBox
            // 
            this.passBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passBox.ForeColor = System.Drawing.Color.DimGray;
            this.passBox.Location = new System.Drawing.Point(31, 153);
            this.passBox.Name = "passBox";
            this.passBox.PasswordChar = '●';
            this.passBox.Size = new System.Drawing.Size(255, 35);
            this.passBox.TabIndex = 2;
            this.passBox.Text = "password";
            // 
            // emailBox
            // 
            this.emailBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emailBox.ForeColor = System.Drawing.Color.DimGray;
            this.emailBox.Location = new System.Drawing.Point(31, 93);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(255, 35);
            this.emailBox.TabIndex = 1;
            this.emailBox.Text = "email@address.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // pnl_NewAccount
            // 
            this.pnl_NewAccount.Controls.Add(this.btn_ExistingAccount);
            this.pnl_NewAccount.Controls.Add(this.new_repeatPasswordBox);
            this.pnl_NewAccount.Controls.Add(this.btn_NewAccount);
            this.pnl_NewAccount.Controls.Add(this.new_passwordBox);
            this.pnl_NewAccount.Controls.Add(this.new_emailBox);
            this.pnl_NewAccount.Controls.Add(this.label2);
            this.pnl_NewAccount.Location = new System.Drawing.Point(123, 12);
            this.pnl_NewAccount.Name = "pnl_NewAccount";
            this.pnl_NewAccount.Size = new System.Drawing.Size(325, 285);
            this.pnl_NewAccount.TabIndex = 4;
            this.pnl_NewAccount.Visible = false;
            // 
            // btn_ExistingAccount
            // 
            this.btn_ExistingAccount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExistingAccount.Location = new System.Drawing.Point(12, 214);
            this.btn_ExistingAccount.Name = "btn_ExistingAccount";
            this.btn_ExistingAccount.Size = new System.Drawing.Size(94, 58);
            this.btn_ExistingAccount.TabIndex = 4;
            this.btn_ExistingAccount.Text = "Existing Account";
            this.btn_ExistingAccount.UseVisualStyleBackColor = true;
            this.btn_ExistingAccount.Click += new System.EventHandler(this.btn_ExistingAccount_Click);
            // 
            // new_repeatPasswordBox
            // 
            this.new_repeatPasswordBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_repeatPasswordBox.ForeColor = System.Drawing.Color.DimGray;
            this.new_repeatPasswordBox.Location = new System.Drawing.Point(31, 157);
            this.new_repeatPasswordBox.Name = "new_repeatPasswordBox";
            this.new_repeatPasswordBox.PasswordChar = '●';
            this.new_repeatPasswordBox.Size = new System.Drawing.Size(255, 35);
            this.new_repeatPasswordBox.TabIndex = 3;
            this.new_repeatPasswordBox.Text = "passward";
            // 
            // btn_NewAccount
            // 
            this.btn_NewAccount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NewAccount.Location = new System.Drawing.Point(214, 214);
            this.btn_NewAccount.Name = "btn_NewAccount";
            this.btn_NewAccount.Size = new System.Drawing.Size(94, 58);
            this.btn_NewAccount.TabIndex = 0;
            this.btn_NewAccount.Text = "Go";
            this.btn_NewAccount.UseVisualStyleBackColor = true;
            this.btn_NewAccount.Click += new System.EventHandler(this.btn_NewAccount_Click);
            // 
            // new_passwordBox
            // 
            this.new_passwordBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_passwordBox.ForeColor = System.Drawing.Color.DimGray;
            this.new_passwordBox.Location = new System.Drawing.Point(31, 116);
            this.new_passwordBox.Name = "new_passwordBox";
            this.new_passwordBox.PasswordChar = '●';
            this.new_passwordBox.Size = new System.Drawing.Size(255, 35);
            this.new_passwordBox.TabIndex = 2;
            this.new_passwordBox.Text = "password";
            // 
            // new_emailBox
            // 
            this.new_emailBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_emailBox.ForeColor = System.Drawing.Color.DimGray;
            this.new_emailBox.Location = new System.Drawing.Point(31, 75);
            this.new_emailBox.Name = "new_emailBox";
            this.new_emailBox.Size = new System.Drawing.Size(255, 35);
            this.new_emailBox.TabIndex = 1;
            this.new_emailBox.Text = "email@address.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "New Account";
            // 
            // pnl_AccountDetails
            // 
            this.pnl_AccountDetails.Controls.Add(this.new_dobBox);
            this.pnl_AccountDetails.Controls.Add(this.label4);
            this.pnl_AccountDetails.Controls.Add(this.btn_Start);
            this.pnl_AccountDetails.Controls.Add(this.new_nameBox);
            this.pnl_AccountDetails.Controls.Add(this.label3);
            this.pnl_AccountDetails.Location = new System.Drawing.Point(123, 12);
            this.pnl_AccountDetails.Name = "pnl_AccountDetails";
            this.pnl_AccountDetails.Size = new System.Drawing.Size(322, 285);
            this.pnl_AccountDetails.TabIndex = 5;
            this.pnl_AccountDetails.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(69, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "DOB";
            // 
            // new_dobBox
            // 
            this.new_dobBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_dobBox.Location = new System.Drawing.Point(31, 152);
            this.new_dobBox.Name = "new_dobBox";
            this.new_dobBox.Size = new System.Drawing.Size(255, 35);
            this.new_dobBox.TabIndex = 6;
            // 
            // btn_Start
            // 
            this.btn_Start.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.Location = new System.Drawing.Point(214, 198);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(94, 58);
            this.btn_Start.TabIndex = 5;
            this.btn_Start.Text = "Sign Up";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // new_nameBox
            // 
            this.new_nameBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_nameBox.ForeColor = System.Drawing.Color.DimGray;
            this.new_nameBox.Location = new System.Drawing.Point(31, 75);
            this.new_nameBox.Name = "new_nameBox";
            this.new_nameBox.Size = new System.Drawing.Size(255, 35);
            this.new_nameBox.TabIndex = 2;
            this.new_nameBox.Text = "Your Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 50);
            this.label3.TabIndex = 1;
            this.label3.Text = "Your Details";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 318);
            this.Controls.Add(this.pnl_AccountDetails);
            this.Controls.Add(this.pnl_NewAccount);
            this.Controls.Add(this.pnl_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            this.Text = "Donatello";
            this.pnl_Login.ResumeLayout(false);
            this.pnl_Login.PerformLayout();
            this.pnl_NewAccount.ResumeLayout(false);
            this.pnl_NewAccount.PerformLayout();
            this.pnl_AccountDetails.ResumeLayout(false);
            this.pnl_AccountDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnl_Login;
        private FocusTextBox passBox;
        private FocusTextBox emailBox;
        private Label label1;
        private Button btn_CreateAcc;
        private Button btn_Login;
        private Panel pnl_NewAccount;
        private Button btn_NewAccount;
        private FocusTextBox new_passwordBox;
        private FocusTextBox new_emailBox;
        private Label label2;
        private FocusTextBox new_repeatPasswordBox;
        private Button btn_ExistingAccount;
        private Panel pnl_AccountDetails;
        private Button btn_Start;
        private FocusTextBox new_nameBox;
        private Label label3;
        private DateTimePicker new_dobBox;
        private Label label4;
    }

    /// <summary>
    /// FocusTextBox class
    /// Creates a textbox that, when receiving focus, highlights all its text (like a URL bar in a browser)
    /// Inherits from System.Windows.Forms.TextBox
    /// Used in place of all text boxes in this designer to create the illusion of placeholder text.
    /// </summary>
    public class FocusTextBox : System.Windows.Forms.TextBox
    {
        private bool _focussed;

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (MouseButtons == MouseButtons.None)
            {
                SelectAll();
                _focussed = true;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            _focussed = false;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (!_focussed)
            {
                if (SelectionLength == 0)
                    SelectAll();
                _focussed = true;
            }
        }
    }
}

