// using System;
// using System.Collections.Generic;
// using System.Drawing;
// using System.Linq;
// using System.Windows.Forms;
// using projectC.ControllerEntities;
// using Domain;
// using projectC.Server_Connection;
//
// namespace projectC.Forms
// {
//     public class MatchForm : Form
//     {
//         private readonly MatchController matchController = new();
//         private readonly UserController userController = new();
//         private SimpleClientConnection connection;
//
//         private ListBox matchListBox;
//         private ComboBox userComboBox;
//         private TextBox seatBox;
//         private TextBox searchBox;
//         private Button sellButton;
//         private Button logoutButton;
//
//         private List<Match> allMatches = new();
//         private List<Match> filteredMatches = new();
//
//         public MatchForm()
//         {
//             this.Text = "Match Ticketing";
//             this.Size = new Size(1200, 800);
//             this.StartPosition = FormStartPosition.CenterScreen;
//             this.BackColor = Color.FromArgb(245, 245, 245);
//             this.FormBorderStyle = FormBorderStyle.FixedDialog;
//
//             var mainLayout = new TableLayoutPanel
//             {
//                 Dock = DockStyle.Fill,
//                 RowCount = 2,
//                 ColumnCount = 1
//             };
//             mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
//             mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
//
//             var contentPanel = new FlowLayoutPanel
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
//                 Text = "Sell Tickets",
//                 Font = new Font("Segoe UI", 24, FontStyle.Bold),
//                 ForeColor = Color.FromArgb(50, 50, 50),
//                 AutoSize = true,
//                 Margin = new Padding(0, 0, 0, 30)
//             };
//
//             searchBox = new TextBox
//             {
//                 PlaceholderText = "Search for matches...",
//                 Width = 400,
//                 Font = new Font("Segoe UI", 12),
//                 Margin = new Padding(0, 0, 0, 10)
//             };
//             searchBox.TextChanged += SearchBox_TextChanged;
//
//             userComboBox = new ComboBox
//             {
//                 Width = 400,
//                 DropDownStyle = ComboBoxStyle.DropDownList,
//                 Font = new Font("Segoe UI", 12),
//                 Margin = new Padding(0, 10, 0, 10)
//             };
//
//             seatBox = new TextBox
//             {
//                 PlaceholderText = "Number of Seats",
//                 Width = 400,
//                 Font = new Font("Segoe UI", 12),
//                 Margin = new Padding(0, 0, 0, 10)
//             };
//
//             sellButton = CreateStyledButton("Sell Tickets");
//             sellButton.Click += SellButton_Click;
//
//             matchListBox = new ListBox
//             {
//                 Width = 1050,
//                 Height = 400,
//                 Font = new Font("Segoe UI", 11),
//                 Margin = new Padding(0, 30, 0, 20),
//                 DrawMode = DrawMode.OwnerDrawFixed
//             };
//             matchListBox.DrawItem += MatchListBox_DrawItem;
//
//             contentPanel.Controls.Add(titleLabel);
//             contentPanel.Controls.Add(searchBox);
//             contentPanel.Controls.Add(userComboBox);
//             contentPanel.Controls.Add(seatBox);
//             contentPanel.Controls.Add(sellButton);
//             contentPanel.Controls.Add(matchListBox);
//
//             mainLayout.Controls.Add(contentPanel, 0, 0);
//
//             var footerPanel = new Panel
//             {
//                 Dock = DockStyle.Fill,
//                 Padding = new Padding(50, 10, 50, 10)
//             };
//
//             logoutButton = CreateStyledButton("Logout");
//             logoutButton.Dock = DockStyle.Fill;
//             logoutButton.Click += LogoutButton_Click;
//
//             footerPanel.Controls.Add(logoutButton);
//             mainLayout.Controls.Add(footerPanel, 0, 1);
//
//             this.Controls.Add(mainLayout);
//             LoadData();
//             
//             connection = new SimpleClientConnection(this);
//
//         }
//
//         public void RefreshMatchesFromServer()
//         {
//             matchController.RefreshMatches();
//             allMatches = matchController.GetMatchList().ToList();
//             ApplySearchFilter();         
//         }
//
//         
//         private Button CreateStyledButton(string text)
//         {
//             return new Button
//             {
//                 Text = text,
//                 Width = 400,
//                 Height = 45,
//                 Font = new Font("Segoe UI", 11, FontStyle.Bold),
//                 BackColor = Color.FromArgb(70, 130, 180),
//                 ForeColor = Color.White,
//                 FlatStyle = FlatStyle.Flat,
//                 Margin = new Padding(0, 10, 0, 0)
//             };
//         }
//
//         private void LoadData()
//         {
//             allMatches = matchController.GetMatchList().ToList();
//             userComboBox.Items.Clear();
//             userComboBox.Items.AddRange(userController.GetUsernames().ToArray());
//             ApplySearchFilter();
//         }
//
//         private void SearchBox_TextChanged(object sender, EventArgs e)
//         {
//             ApplySearchFilter();
//         }
//
//         private void ApplySearchFilter()
//         {
//             string keyword = searchBox.Text.ToLower();
//             filteredMatches = allMatches
//                 .Where(m => m.matchName.ToLower().Contains(keyword))
//                 .ToList();
//
//             matchListBox.Items.Clear();
//             foreach (var match in filteredMatches)
//             {
//                 matchListBox.Items.Add(match);
//             }
//         }
//
//         private void SellButton_Click(object sender, EventArgs e)
//         {
//             Match selectedMatch = null;
//
//             if (filteredMatches.Count == 1)
//             {
//                 selectedMatch = filteredMatches.First();
//             }
//             else if (matchListBox.SelectedItem is Match match)
//             {
//                 selectedMatch = match;
//             }
//
//             if (selectedMatch == null)
//             {
//                 MessageBox.Show("Please select a match or refine your search.");
//                 return;
//             }
//
//             if (selectedMatch.nr_seats == 0)
//             {
//                 MessageBox.Show("Match is SOLD OUT!", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                 return;
//             }
//
//             string username = userComboBox.SelectedItem?.ToString();
//             if (string.IsNullOrWhiteSpace(username) || !userController.FindUser(username))
//             {
//                 MessageBox.Show("Cannot find user!");
//                 return;
//             }
//
//             if (!int.TryParse(seatBox.Text, out int seats))
//             {
//                 MessageBox.Show("Invalid number of seats.");
//                 return;
//             }
//
//             bool success = matchController.BuyTickets(selectedMatch.matchId, seats);
//
//             if (!success)
//             {
//                 MessageBox.Show("Not enough available seats!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                 return;
//             }
//             
//             matchController.RefreshMatches();
//             LoadData();
//             
//             if (connection != null)
//             {
//                 connection.NotifyServer();
//             }
//             
//             seatBox.Clear();
//             searchBox.Clear();
//         }
//
//         private void LogoutButton_Click(object sender, EventArgs e)
//         {
//             var userForm = new UserForm();
//             userForm.Show();
//             this.Close();
//         }
//
//         private void MatchListBox_DrawItem(object sender, DrawItemEventArgs e)
//         {
//             if (e.Index < 0 || e.Index >= matchListBox.Items.Count) return;
//
//             var match = matchListBox.Items[e.Index] as Match;
//             string display = match?.ToString() ?? "";
//
//             if (match == null) return;
//
//             e.DrawBackground();
//
//             var color = match.nr_seats == 0 ? Brushes.Red : Brushes.Black;
//             var text = match.nr_seats == 0 ? $"{display} (SOLD OUT)" : display;
//
//             e.Graphics.DrawString(text, e.Font, color, e.Bounds);
//             e.DrawFocusRectangle();
//         }
//     }
// }
