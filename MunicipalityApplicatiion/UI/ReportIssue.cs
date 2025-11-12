using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MunicipalityApplicatiion.Models;
using MunicipalityApplicatiion.Repositories;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MunicipalityApplicatiion.UI
{
    public partial class ReportIssue : Form

    {
        // Repository reference
        private ServiceRequestRepository? _repo;

        public ReportIssue(ServiceRequestRepository repo) : this()
        {
            _repo = repo;
        }


        public ReportIssue()
        {
            InitializeComponent(); 

            Text = "Report Issue";
            MinimumSize = new Size(900, 700);
            Size = new Size(900, 700);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.OldLace;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Hook button events
            btnSubmit.Click += btnSubmit_Click;
            btnBack.Click += btnBack_Click;

            // Hook progress bar tracking
            txtLocation.TextChanged += UpdateProgress;
            comboBox1.SelectedIndexChanged += UpdateProgress;
            txtDescription.TextChanged += UpdateProgress;
            listAttachments.SelectedIndexChanged += UpdateProgress;


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Custom draw event for combo box items to handle placeholder styling
        private void ComboBox1_DrawItem(object? sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                string text = comboBox1.Items[e.Index]?.ToString() ?? "";
                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    e.Font,
                    e.Bounds,
                    (e.State.HasFlag(DrawItemState.Disabled) ? SystemColors.GrayText : e.ForeColor),
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            e.DrawFocusRectangle();
        }


        // Event handler for when the user changes the selected item in the combo box
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.BringToFront();

        }

        // Event handler when the description TextBox gains focus
        private void textDescription_Enter(object sender, EventArgs e)
        {
            if (txtDescription.Text == "Enter a description of your issue")
            {
                txtDescription.Text = "";
                txtDescription.ForeColor = Color.Black;
                UpdateProgress(sender, EventArgs.Empty); // Update progress
            }
        }

        // Event handler when the description TextBox loses focus
        private void textDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.Text = "Enter a description of your issue";
                txtDescription.ForeColor = Color.Gray;
                UpdateProgress(sender, EventArgs.Empty); // Update progress
            }
        }

        // Event handler for double-clicking the attachments list
        private void listAttachments_DoubleClick(object sender, EventArgs e)
        {

        }

        // Event handler for form load
        private void ReportIssue_Load(object sender, EventArgs e)
        {
            // Initialize the progress bar values
            progressBar.Minimum = 0;
            progressBar.Maximum = 4;
            progressBar.Value = 0;
            lblEngagement.Text = "Let’s make our city better. Start by filling in the details.";

            comboBox1.DrawItem += ComboBox1_DrawItem;

            // Set up combo box with placeholder and options
            comboBox1.Items.Clear();
            comboBox1.ForeColor = Color.Gray;
            comboBox1.Items.Add("Select a category");
            comboBox1.Items.AddRange(new object[] { "Sanitation", "Roads", "Utilities", "Safety", "Other" });
            comboBox1.SelectedIndex = 0;

            // Change the text color when user selects a category
            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                if (comboBox1.SelectedIndex > 0)
                    comboBox1.ForeColor = Color.Black;
                else
                    comboBox1.ForeColor = Color.Gray;
            };

            // Ensure comboBox behaves as DropDownList
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            // Initialize description TextBox with placeholder
            txtDescription.Text = "Enter a description of your issue";
            txtDescription.ForeColor = Color.Gray;

            txtDescription.TextChanged += UpdateProgress;
            txtDescription.Enter += textDescription_Enter;
            txtDescription.Leave += textDescription_Leave;
        }

        // Event handler for clicking on the PictureBox
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // Event handler for the Upload button
        private void btnUpload_Click(object sender, EventArgs e)
        {
            // Open a file dialog to allow users to select one or more files
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true; // Allow selecting multiple files
                ofd.Filter = "Images/Docs|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.pdf;*.docx;*.xlsx;*.txt";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Loop through each selected file and add its name to the ListBox
                    foreach (var file in ofd.FileNames)
                    {
                        listAttachments.Items.Add(System.IO.Path.GetFileName(file));
                    }
                    // Update progress after adding attachments
                    UpdateProgress(sender, e);
                }
            }
        }
        // Method to update the progress bar and engagement label based on filled steps
        private void UpdateProgress(object? sender, EventArgs e)
        {
            int filled = 0; // Counter for completed steps

            // Step 1: Location
            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
                filled++;

            // Step 2: Category
            if (comboBox1.SelectedIndex > 0)
                filled++;

            // Step 3: Description (must not be placeholder)
            if (!string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text != "Enter a description of your issue")
                filled++;

            // Step 4: Attachments
            if (listAttachments.Items.Count > 0)
                filled++;

            // Update progress bar safely
            progressBar.Value = Math.Min(filled, progressBar.Maximum);

            // Update engagement label
            switch (filled)
            {
                case 0:
                    lblEngagement.Text = "Let’s make our city better. Start by filling in the details.";
                    break;
                case 1:
                    lblEngagement.Text = "Step 1 complete! Now add the category of your issue.";
                    break;
                case 2:
                    lblEngagement.Text = "Step 2 complete! Describe your issues in detail.";
                    break;
                case 3:
                    lblEngagement.Text = "Step 3 complete! Attach supporting documents for reference.";
                    break;
                case 4:
                    lblEngagement.Text = "All steps done! Submit your report now.";
                    break;
            }
        }

        // Event handler for the Back button
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide the ReportIssue form

            // Create a new Main Menu instance
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show(); // show the main menu
        }

        // Event handler for the Submit button
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtLocation.Text) ||
                comboBox1.SelectedIndex <= 0 ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                txtDescription.Text == "Enter a description of your issue" ||
                listAttachments.Items.Count == 0)
            {
                MessageBox.Show("Please complete all steps before submitting.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create request
            var req = new MunicipalityApplicatiion.Models.ServiceRequest
            {
                Title = $"{comboBox1.SelectedItem} issue @ {txtLocation.Text}",
                Description = txtDescription.Text.Trim(),
                Priority = 2,                 // keep your behavior
                Status = RequestStatus.Submitted,
                LocationNode = txtLocation.Text.Trim()
            };

            // Save
            _repo?.Add(req);

            // Success message WITH ID 
            try { Clipboard.SetText(req.RequestId); } catch { /* ignore clipboard failures */ }

            MessageBox.Show(
                $"Your report has been submitted successfully!\n\nReport ID: {req.RequestId}\n(the ID has been copied to your clipboard)",
                "Report Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Close and return to menu
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLocation_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}