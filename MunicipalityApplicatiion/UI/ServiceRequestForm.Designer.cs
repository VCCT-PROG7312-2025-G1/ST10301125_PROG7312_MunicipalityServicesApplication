using System.Drawing;
using System.Windows.Forms;
using MunicipalityApplicatiion.Models;

namespace MunicipalityApplicatiion.Forms
{
    partial class ServiceRequestForm
    {
        private System.ComponentModel.IContainer components = null!;
        private Panel headerPanel = null!;
        private TextBox txtHeading = null!;
        private TextBox txtSubHeading = null!;

        private Panel searchPanel = null!;
        private Label lblSearch = null!;
        private TextBox txtSearchId = null!;
        private Button btnFind = null!;
        private Label lblFilter = null!;
        private ComboBox cboStatus = null!;
        private Button btnTopPriority = null!;
        private Button btnBack = new Button();

        private Panel gridPanel = null!;
        private DataGridView grid = null!;

        private GroupBox analyticsBox = null!;
        private ListBox lstInfo = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {

            components = new System.ComponentModel.Container();

            // ===== Header =====
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.OldLace
            };

            // ===== Logo =====
            PictureBox pbLogo = new PictureBox
            {
                Image = MunicipalityApplicatiion.Properties.Resources.MunicipalityCover, // resource image
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 80,
                Height = 80,
                Location = new Point(20, 5) // adjust vertical position to align with text
            };
            headerPanel.Controls.Add(pbLogo);

            // Back Button — top right corner
            btnBack = new Button
            {
                Text = "⬅︎ Back to Main Menu",
                Width = 160,
                Height = 35,
                BackColor = Color.White,
                UseVisualStyleBackColor = true,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Standard,
                Location = new Point(20, 25),
                Font = new Font("Verdana", 9, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Padding = new Padding(3)
            };

            btnBack.Click += (s, e) => Close();


            txtHeading = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Color.OldLace,
                Font = new Font("Verdana", 20F, FontStyle.Bold),
                Text = "Service Request Status",
                Location = new Point(120, 14),
                Width = 500
            };


            txtSubHeading = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = Color.OldLace,
                Font = new Font("Verdana", 9F, FontStyle.Italic),
                Text = "Track municipal requests and view progress in real time.",
                Location = new Point(120, 54),
                Width = 500
            };



            headerPanel.Controls.Add(txtHeading);
            headerPanel.Controls.Add(txtSubHeading);
            headerPanel.Controls.Add(btnBack);

            // ===== Search/Filter row =====
            searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(255, 248, 236),
                Padding = new Padding(12, 8, 12, 8)
            };

            lblSearch = new Label
            {
                Text = "Enter Request ID:",
                AutoSize = true,
                Location = new Point(8, 14),
                Font = new Font("Verdana", 8F, FontStyle.Bold)
            };

            txtSearchId = new TextBox
            {
                Width = 220,
                Location = new Point(140, 10),
                PlaceholderText = "e.g., a1b2c3..."
            };

            btnFind = new Button
            {
                Text = "Track by ID",
                Location = new Point(368, 8),
                Size = new Size(100, 26)
            };
            btnFind.Click += btnFind_Click;

            lblFilter = new Label
            {
                Text = "Filter by Status:",
                AutoSize = true,
                Location = new Point(490, 14),
                Font = new Font("Verdana", 8F, FontStyle.Bold)
            };

            cboStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(600, 10),
                Width = 150
            };
            cboStatus.Items.AddRange(System.Enum.GetNames(typeof(RequestStatus)));
            cboStatus.SelectedIndexChanged += (_, __) => RefreshGrid();

            btnTopPriority = new Button
            {
                Text = "Show Top",
                Location = new Point(762, 8),
                Size = new Size(100, 26)
            };
            btnTopPriority.Click += btnTopPriority_Click;

            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearchId);
            searchPanel.Controls.Add(btnFind);
            searchPanel.Controls.Add(lblFilter);
            searchPanel.Controls.Add(cboStatus);
            searchPanel.Controls.Add(btnTopPriority);

            // ===== Grid =====
            gridPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 360,
                BackColor = Color.White,
                Padding = new Padding(12, 12, 12, 6)
            };

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Define columns (bind to your model properties)
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Request ID",
                DataPropertyName = "Id",
                FillWeight = 160
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Title",
                DataPropertyName = "Title",
                FillWeight = 160
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Description",
                DataPropertyName = "Description",
                FillWeight = 220
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Priority",
                DataPropertyName = "Priority",
                FillWeight = 70
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Status",
                DataPropertyName = "Status",
                FillWeight = 100
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "CreatedAt",
                DataPropertyName = "CreatedAt",
                FillWeight = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "UpdatedAt",
                DataPropertyName = "UpdatedAt",
                FillWeight = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Location",
                DataPropertyName = "LocationNode",
                FillWeight = 120
            });

            gridPanel.Controls.Add(grid);

            // ===== Analytics / Summary =====
            analyticsBox = new GroupBox
            {
                Text = "Insights",
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                Font = new Font("Verdana", 9F, FontStyle.Bold)
            };

            lstInfo = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Verdana", 8.25F, FontStyle.Regular),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            analyticsBox.Controls.Add(lstInfo);

            // ===== Form =====
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.OldLace;
            ClientSize = new Size(1000, 650);
            Text = "Service Request Status";


            Controls.Add(analyticsBox);
            Controls.Add(gridPanel);
            Controls.Add(searchPanel);
            Controls.Add(headerPanel);
        }
    }
}
