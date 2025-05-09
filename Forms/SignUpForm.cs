// using System;
// using System.Drawing;
// using System.Windows.Forms;
// using projectC.ControllerEntities;
// using Domain;
//
// namespace projectC.Forms
// {
//     public class SignUpForm : Form
//     {
//         private TextBox usernameBox;
//         private TextBox passwordBox;
//         private TextBox passwordConfirmBox;
//         private Button createButton;
//
//         private readonly UserController userController = new();
//
//         public SignUpForm()
//         {
//             this.Text = "Create Your Account";
//             this.Size = new Size(600, 500); // Increased size
//             this.StartPosition = FormStartPosition.CenterScreen;
//             this.FormBorderStyle = FormBorderStyle.FixedDialog;
//             this.BackColor = Color.White;
//
//             var layoutPanel = new TableLayoutPanel
//             {
//                 Dock = DockStyle.Fill,
//                 Padding = new Padding(50),
//                 RowCount = 5,
//                 ColumnCount = 1
//             };
//             layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
//             layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
//             layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
//             layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
//             layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
//
//             var titleLabel = new Label
//             {
//                 Text = "Sign Up",
//                 Font = new Font("Segoe UI", 24, FontStyle.Bold),
//                 ForeColor = Color.FromArgb(50, 50, 50),
//                 TextAlign = ContentAlignment.MiddleCenter,
//                 Dock = DockStyle.Fill
//             };
//
//             usernameBox = CreateStyledTextBox("Username");
//             passwordBox = CreateStyledTextBox("Password", true);
//             passwordConfirmBox = CreateStyledTextBox("Confirm Password", true);
//
//             createButton = new Button
//             {
//                 Text = "Create Account",
//                 Width = 300,
//                 Height = 45,
//                 Font = new Font("Segoe UI", 12, FontStyle.Bold),
//                 BackColor = Color.FromArgb(0, 120, 215),
//                 ForeColor = Color.White,
//                 FlatStyle = FlatStyle.Flat,
//                 Anchor = AnchorStyles.Top
//             };
//             createButton.FlatAppearance.BorderSize = 0;
//             createButton.Click += CreateButton_Click;
//
//             var buttonPanel = new Panel
//             {
//                 Dock = DockStyle.Top,
//                 Height = 70
//             };
//             buttonPanel.Controls.Add(createButton);
//             createButton.Location = new Point((buttonPanel.Width - createButton.Width) / 2, 10);
//             buttonPanel.Resize += (s, e) =>
//             {
//                 createButton.Left = (buttonPanel.Width - createButton.Width) / 2;
//             };
//
//             layoutPanel.Controls.Add(titleLabel);
//             layoutPanel.Controls.Add(usernameBox);
//             layoutPanel.Controls.Add(passwordBox);
//             layoutPanel.Controls.Add(passwordConfirmBox);
//             layoutPanel.Controls.Add(buttonPanel);
//
//             this.Controls.Add(layoutPanel);
//         }
//
//         private TextBox CreateStyledTextBox(string placeholder, bool isPassword = false)
//         {
//             return new TextBox
//             {
//                 Width = 400,
//                 Font = new Font("Segoe UI", 12),
//                 PlaceholderText = placeholder,
//                 UseSystemPasswordChar = isPassword,
//                 Margin = new Padding(0, 10, 0, 10),
//                 Anchor = AnchorStyles.Left | AnchorStyles.Right
//             };
//         }
//
//         private void CreateButton_Click(object sender, EventArgs e)
//         {
//             string username = usernameBox.Text.Trim();
//             string password = passwordBox.Text;
//             string confirm = passwordConfirmBox.Text;
//
//             if (string.IsNullOrWhiteSpace(username) ||
//                 string.IsNullOrWhiteSpace(password) ||
//                 password != confirm)
//             {
//                 MessageBox.Show("Please enter valid credentials and make sure passwords match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                 return;
//             }
//
//             var newUser = new User(username, password);
//             userController.AddUser(newUser);
//
//             MessageBox.Show("User created! Please log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//
//             var loginForm = new UserForm();
//             loginForm.Show();
//             this.Close();
//         }
//     }
// }
