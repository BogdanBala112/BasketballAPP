// using System;
// using System.Drawing;
// using System.Windows.Forms;
// using projectC.ControllerEntities;
// using Domain;
//
// namespace projectC.Forms
// {
//     public class UserForm : Form
//     {
//         private TextBox usernameBox;
//         private TextBox passwordBox;
//         private Button loginButton;
//         private Button signInButton;
//         private Button closeButton;
//
//         private readonly UserController userController = new();
//
//         public UserForm()
//         {
//             this.Text = "Authentication Window";
//             this.Size = new Size(800, 700);
//             this.StartPosition = FormStartPosition.CenterScreen;
//             this.BackColor = Color.FromArgb(245, 245, 245); // Light gray background
//             this.FormBorderStyle = FormBorderStyle.FixedDialog;
//
//             var layout = new FlowLayoutPanel
//             {
//                 Dock = DockStyle.Fill,
//                 FlowDirection = FlowDirection.TopDown,
//                 Padding = new Padding(50),
//                 AutoScroll = true,
//                 WrapContents = false
//             };
//
//             var titleLabel = new Label
//             {
//                 Text = "LOG IN",
//                 Font = new Font("Segoe UI", 20, FontStyle.Bold),
//                 ForeColor = Color.FromArgb(40, 40, 40),
//                 AutoSize = true,
//                 Margin = new Padding(0, 0, 0, 30)
//             };
//
//             usernameBox = new TextBox
//             {
//                 PlaceholderText = "Username",
//                 Width = 300,
//                 Font = new Font("Segoe UI", 12)
//             };
//
//             passwordBox = new TextBox
//             {
//                 PlaceholderText = "Password",
//                 Width = 300,
//                 Font = new Font("Segoe UI", 12),
//                 UseSystemPasswordChar = true
//             };
//
//             loginButton = CreateStyledButton("Log In");
//             signInButton = CreateStyledButton("Sign In");
//             closeButton = CreateStyledButton("Close Application");
//
//             loginButton.Click += LoginButton_Click;
//             signInButton.Click += SignInButton_Click;
//             closeButton.Click += (s, e) => Application.Exit();
//
//             layout.Controls.Add(titleLabel);
//             layout.Controls.Add(usernameBox);
//             layout.Controls.Add(passwordBox);
//             layout.Controls.Add(loginButton);
//             layout.Controls.Add(signInButton);
//             layout.Controls.Add(closeButton);
//
//             this.Controls.Add(layout);
//         }
//
//         private Button CreateStyledButton(string text)
//         {
//             return new Button
//             {
//                 Text = text,
//                 Width = 300,
//                 Height = 40,
//                 Font = new Font("Segoe UI", 10, FontStyle.Bold),
//                 BackColor = Color.FromArgb(70, 130, 180), // Steel Blue
//                 ForeColor = Color.White,
//                 FlatStyle = FlatStyle.Flat,
//                 Margin = new Padding(0, 10, 0, 0)
//             };
//         }
//
//         private void LoginButton_Click(object sender, EventArgs e)
//         {
//             string username = usernameBox.Text;
//             string password = passwordBox.Text;
//
//             if (userController.Authenticate(username, password))
//             {
//                 var matchForm = new MatchForm();
//                 matchForm.Show();
//                 this.Hide();
//             }
//             else
//             {
//                 MessageBox.Show("Cannot find user or password!", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
//             }
//         }
//
//         private void SignInButton_Click(object sender, EventArgs e)
//         {
//             var createUserForm = new SignUpForm();
//             createUserForm.Show();
//             this.Hide();
//         }
//     }
// }
