using MunicipalityApplicatiion.Models;
using MunicipalityApplicatiion.Repositories;
using MunicipalityApplicatiion.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MunicipalityApplicatiion.UI
{
    public partial class LocalEvents : Form
    {
        private readonly EventRepository _repo;
        private ListView lvEvents;
        private FlowLayoutPanel flpEvents;
        private TextBox txtSearch;
        private ComboBox cmbCategory;
        private DateTimePicker dtpDate;
        private Button btnSearch;
        private ListBox lbRecommendations;
        private Button btnBack;
        private Label lblHeading;
        private Label lblSubHeading;
        private ImageList imgList;

        public LocalEvents()
        {
            _repo = SampleData.SeedEventsWithImages(); // Sample data displayed on page

            Text = "Local Events & Announcements";
            Size = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.OldLace;

            InitializeUI();
            LoadInitialEvents();
            PopulateCategories();
            ShowRecommendations();
        }

        private void InitializeUI()
        {

            // Logo PictureBox
            PictureBox pbLogo = new PictureBox
            {
                Image = MunicipalityApplicatiion.Properties.Resources.MunicipalityCover, 
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 80,
                Height = 80,
                Location = new Point(20, 20)
            };

            // Heading Label
            lblHeading = new Label
            {
                Text = "Local Events && Announcements",
                Font = new Font("Verdana", 20, FontStyle.Bold),
                BackColor = Color.OldLace,
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(pbLogo.Right + 20, 30), 
                AutoSize = true
            };

            // Sub-heading Label
            lblSubHeading = new Label
            {
                Text = "Discover our latest events and announcements",
                Font = new Font("Verdana", 9, FontStyle.Italic),
                ForeColor = Color.Black,
                Location = new Point(pbLogo.Right + 20, lblHeading.Bottom + 10), 
                AutoSize = true
            };

            // Add controls to form
            Controls.Add(pbLogo);
            Controls.Add(lblHeading);
            Controls.Add(lblSubHeading);

            // Back Button
            btnBack = new Button
            {
                Text = "⬅︎ Back to Main Menu",
                Width = 160,
                Height = 35,
                BackColor = Color.White,
                UseVisualStyleBackColor = true,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Standard,
                Font = new Font("Verdana", 9, FontStyle.Bold),
                Padding = new Padding(3)
            };

            btnBack.Click += (s, e) => Close();

            btnBack.Location = new Point(this.ClientSize.Width - btnBack.Width - 30, 40);

            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnBack.Click += (s, e) => this.Close();

            Controls.Add(btnBack);

            // Search Panel
            Panel searchPanel = new Panel
            {
                Location = new Point(20, lblSubHeading.Bottom + 10),
                Size = new Size(940, 60),
                BackColor = Color.Transparent
            };

            Label lblSearch = new Label { Text = "Search:", Location = new Point(10, 18), AutoSize = true };
            txtSearch = new TextBox
            {
                Location = new Point(70, 15),
                Width = 250,
                BackColor = Color.White,
                Font = new Font("Verdana", 10, FontStyle.Regular),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCategory = new Label { Text = "Category:", Location = new Point(340, 18), AutoSize = true };
            cmbCategory = new ComboBox { Location = new Point(410, 15), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblDate = new Label { Text = "Date:", Location = new Point(610, 18), AutoSize = true };
            dtpDate = new DateTimePicker { Location = new Point(650, 15), Width = 160, Format = DateTimePickerFormat.Short };

            // Search button
            btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(830, 14),
                Width = 90,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Verdana", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            searchPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblCategory, cmbCategory, lblDate, dtpDate, btnSearch });

            // Clear button
            Button btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(btnSearch.Left, btnSearch.Bottom + 2), 
                Width = 90,                            
                Height = btnSearch.Height,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Verdana", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) =>
            {
                txtSearch.Text = "";
                cmbCategory.SelectedIndex = 0;
                dtpDate.Value = DateTime.Today;
                RefreshEventList(_repo.GetAllUpcomingEvents());
            };

            searchPanel.Controls.Add(btnClear);

            // Events panel - vertical scroll 
            flpEvents = new FlowLayoutPanel
            {
                Location = new Point(20, searchPanel.Bottom + 10),
                Size = new Size(650, this.ClientSize.Height - searchPanel.Bottom - 40),
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.White,
                Padding = new Padding(5)
            };

            // Recommendations panel
            Panel pnlRecommendations = new Panel
            {
                Location = new Point(690, searchPanel.Bottom + 10),
                Size = new Size(270, this.ClientSize.Height - searchPanel.Bottom - 40),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            // Recommended heading
            Label lblRecs = new Label
            {
                Text = "Recommended for You",
                Font = new Font("Verdana", 12, FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(10, 10),
                AutoSize = true
            };

            // Recommendations ListBox
            lbRecommendations = new ListBox
            {
                Location = new Point(10, lblRecs.Bottom + 10),
                Size = new Size(pnlRecommendations.Width - 20, pnlRecommendations.Height - lblRecs.Bottom - 20),
                BorderStyle = BorderStyle.None
            };
            lbRecommendations.DoubleClick += LbRecommendations_DoubleClick;

            pnlRecommendations.Controls.Add(lblRecs);
            pnlRecommendations.Controls.Add(lbRecommendations);

            Controls.AddRange(new Control[] { btnBack, lblHeading, lblSubHeading, searchPanel, flpEvents, pnlRecommendations });

        }

        // Loads events from repository 
        private void LoadInitialEvents()
        {
            RefreshEventList(_repo.GetAllUpcomingEvents());
        }

        // Categorised the events 
        private void PopulateCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");
            foreach (var c in _repo.Categories) cmbCategory.Items.Add(c);
            cmbCategory.SelectedIndex = 0;
        }

        private readonly SearchTrack _tracker = new();

        // Event card panel that has the seeded examples 
        private void RefreshEventList(IEnumerable<EventItem> events)
        {
            flpEvents.Controls.Clear();

            foreach (var evt in events)
            {
                Panel card = new Panel
                {
                    Width = 350,
                    Height = 410,
                    Margin = new Padding(10),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                PictureBox pic = new PictureBox
                {
                    Width = 330,
                    Height = 250,
                    Top = 10,
                    Left = 10,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BorderStyle = BorderStyle.FixedSingle
                };
                try { pic.Image = Image.FromFile(evt.ImagePath); }
                catch { pic.Image = SystemIcons.Application.ToBitmap(); }

                // Event Title
                Label lblTitle = new Label
                {
                    Text = evt.EventTitle,
                    Font = new Font("Verdana", 13, FontStyle.Bold),
                    AutoSize = false,
                    Width = 300,
                    Height = 40,
                    Top = pic.Bottom + 10,
                    Left = 10
                };

                // Event Info
                Label lblInfo = new Label
                {
                    Text = $"{evt.EventDate:MMM dd, yyyy} | {evt.EventCategory}",
                    Font = new Font("Verdana", 9, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    Width = 280,
                    Height = 20,
                    Top = lblTitle.Bottom + 2,
                    Left = 10
                };

                // Event Description
                Label lblDesc = new Label
                {
                    Text = evt.EventDescription,
                    Font = new Font("Verdana", 9, FontStyle.Regular),
                    AutoSize = false,
                    Width = 280,
                    Height = 80,
                    Top = lblInfo.Bottom + 2,
                    Left = 10
                };

                card.Controls.Add(pic);
                card.Controls.Add(lblTitle);
                card.Controls.Add(lblInfo);
                card.Controls.Add(lblDesc);

                // Click to view details
                card.Click += (s, e) => ShowEventDetails(evt);
                foreach (Control c in card.Controls)
                    c.Click += (s, e) => ShowEventDetails(evt);

                flpEvents.Controls.Add(card);
            }
        }

        // Shows the evnts details in message box 
        private void ShowEventDetails(EventItem evt)
        {
            MessageBox.Show($"{evt.EventTitle}\n\nDate: {evt.EventDate:yyyy-MM-dd}\nCategory: {evt.EventCategory}\n\n{evt.EventDescription}",
                "Event Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Search button logic
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();
            string category = (cmbCategory.SelectedIndex > 0)
                ? cmbCategory.SelectedItem.ToString().Trim()
                : null; // treat null as All

            DateTime? selectedDate = null;
            if (dtpDate.Value.Date != DateTime.Today)
                selectedDate = dtpDate.Value.Date;

            var results = _repo.Search(query, category, selectedDate);

            if (!results.Any())
            {
                MessageBox.Show("No matching events found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            RefreshEventList(results);
            RefreshRecommendations();
        }

        // Displays recommendations based on user interactions
        private void ShowRecommendations()
        {
            lbRecommendations.Items.Clear();
            foreach (var e in _repo.Recommend(6))
            {
                lbRecommendations.Items.Add($"{e.EventDate:MM-dd} {e.EventTitle}");
            }
        }

        // Shows the evnts details in message box 
        private void LvEvents_DoubleClick(object sender, EventArgs e)
        {
            if (lvEvents.SelectedItems.Count == 0) return;
            var eItem = lvEvents.SelectedItems[0].Tag as EventItem;
            if (eItem == null) return;

            MessageBox.Show($"{eItem.EventTitle}\n\nDate: {eItem.EventDate:yyyy-MM-dd}\nCategory: {eItem.EventCategory}\n\n{eItem.EventDescription}",
                "Event Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _repo.MarkEventViewed(eItem);
        }

        // Updates in the recommnedations box from algorithms 
        private void LbRecommendations_DoubleClick(object sender, EventArgs e)
        {
            if (lbRecommendations.SelectedItem == null) return;

            string titleWord = lbRecommendations.SelectedItem.ToString().Split(' ', 2).Last();
            txtSearch.Text = titleWord;
            BtnSearch_Click(this, EventArgs.Empty);
        }

        // Refreshes recommendations based on user interactions
        private void RefreshRecommendations()
        {
            lbRecommendations.Items.Clear();

            var recommended = _repo.Recommend(5).ToList();

            if (recommended.Count == 0)
            {
                lbRecommendations.Items.Add("No recommendations yet. Try searching to personalize!");
                return;
            }

            lbRecommendations.Items.Add("Recommended for You:");
            foreach (var ev in recommended)
            {
                lbRecommendations.Items.Add($"• {ev.EventTitle} ({ev.EventCategory}, {ev.EventDate:MMM dd})");
            }
        }

    }
}

