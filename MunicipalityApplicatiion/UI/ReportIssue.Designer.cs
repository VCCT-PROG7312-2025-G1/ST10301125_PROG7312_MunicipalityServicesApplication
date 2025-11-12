namespace MunicipalityApplicatiion.UI
{
    partial class ReportIssue
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportIssue));
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            Location = new Label();
            AttachFile = new Label();
            Description = new Label();
            Category = new Label();
            txtLocation = new TextBox();
            comboBox1 = new ComboBox();
            txtDescription = new RichTextBox();
            imageList = new ImageList(components);
            picUpload = new PictureBox();
            btnUpload = new Button();
            progressBar = new ProgressBar();
            btnSubmit = new Button();
            btnBack = new Button();
            panel1 = new Panel();
            listAttachments = new ListBox();
            label1 = new Label();
            lblEngagement = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picUpload).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.OldLace;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Verdana", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(128, 33);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(519, 35);
            textBox1.TabIndex = 0;
            textBox1.Text = "Report a Municipal Issue";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.OldLace;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Verdana", 9F, FontStyle.Italic);
            textBox2.Location = new Point(138, 74);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(532, 15);
            textBox2.TabIndex = 1;
            textBox2.Text = "Log problems and get support - Help us keep our community running smoothly.";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // Location
            // 
            Location.AutoSize = true;
            Location.Font = new Font("Verdana", 8F, FontStyle.Bold);
            Location.Location = new Point(34, 120);
            Location.Name = "Location";
            Location.Size = new Size(200, 13);
            Location.TabIndex = 2;
            Location.Text = "Location (Street and Suburb):";
            // 
            // AttachFile
            // 
            AttachFile.AutoSize = true;
            AttachFile.Font = new Font("Verdana", 8F, FontStyle.Bold);
            AttachFile.Location = new Point(34, 318);
            AttachFile.Name = "AttachFile";
            AttachFile.Size = new Size(165, 13);
            AttachFile.TabIndex = 3;
            AttachFile.Text = "Attach Proof/Document:";
            // 
            // Description
            // 
            Description.AutoSize = true;
            Description.Font = new Font("Verdana", 8F, FontStyle.Bold);
            Description.Location = new Point(34, 214);
            Description.Name = "Description";
            Description.Size = new Size(142, 13);
            Description.TabIndex = 4;
            Description.Text = "Description of Issue:";
            // 
            // Category
            // 
            Category.AutoSize = true;
            Category.Font = new Font("Verdana", 8F, FontStyle.Bold);
            Category.Location = new Point(34, 161);
            Category.Name = "Category";
            Category.Size = new Size(127, 13);
            Category.TabIndex = 5;
            Category.Text = "Category of Issue:";
            // 
            // txtLocation
            // 
            txtLocation.Anchor = AnchorStyles.Bottom;
            txtLocation.BackColor = SystemColors.ButtonHighlight;
            txtLocation.BorderStyle = BorderStyle.FixedSingle;
            txtLocation.Font = new Font("Verdana", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            txtLocation.ForeColor = Color.Black;
            txtLocation.Location = new Point(251, 120);
            txtLocation.Margin = new Padding(3, 3, 3, 5);
            txtLocation.Multiline = true;
            txtLocation.Name = "txtLocation";
            txtLocation.PlaceholderText = "Enter street number, street name, city, state/province, postal code";
            txtLocation.Size = new Size(420, 22);
            txtLocation.TabIndex = 6;
            txtLocation.TextChanged += txtLocation_TextChanged_1;
            // 
            // comboBox1
            // 
            comboBox1.AllowDrop = true;
            comboBox1.BackColor = Color.White;
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FlatStyle = FlatStyle.Popup;
            comboBox1.Font = new Font("Verdana", 8F, FontStyle.Italic);
            comboBox1.ForeColor = Color.Black;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Sanitation", "Roads", "Utilities", "Safety", "Other" });
            comboBox1.Location = new Point(251, 161);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(420, 21);
            comboBox1.TabIndex = 8;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.None;
            txtDescription.Location = new Point(251, 196);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(420, 96);
            txtDescription.TabIndex = 1933;
            txtDescription.Text = "";
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "Upload.png");
            // 
            // picUpload
            // 
            picUpload.BackColor = Color.White;
            picUpload.Image = Properties.Resources.Upload;
            picUpload.Location = new Point(148, 3);
            picUpload.Name = "picUpload";
            picUpload.Size = new Size(105, 68);
            picUpload.SizeMode = PictureBoxSizeMode.Zoom;
            picUpload.TabIndex = 12;
            picUpload.TabStop = false;
            picUpload.Click += pictureBox1_Click;
            // 
            // btnUpload
            // 
            btnUpload.BackColor = Color.Transparent;
            btnUpload.BackgroundImage = Properties.Resources.Upload;
            btnUpload.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            btnUpload.FlatAppearance.MouseDownBackColor = Color.WhiteSmoke;
            btnUpload.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            btnUpload.FlatStyle = FlatStyle.Popup;
            btnUpload.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnUpload.ForeColor = SystemColors.ControlDarkDark;
            btnUpload.Location = new Point(111, 74);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(182, 31);
            btnUpload.TabIndex = 14;
            btnUpload.Text = "UPLOAD FILE";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(255, 583);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(418, 23);
            progressBar.TabIndex = 15;
            // 
            // btnSubmit
            // 
            btnSubmit.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnSubmit.Location = new Point(255, 508);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(416, 33);
            btnSubmit.TabIndex = 16;
            btnSubmit.Text = "Submit Report";
            btnSubmit.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Verdana", 9F, FontStyle.Bold);
            btnBack.Location = new Point(683, 33);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(164, 25);
            btnBack.TabIndex = 17;
            btnBack.Text = "⬅︎ Back to Main Menu";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(listAttachments);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(picUpload);
            panel1.Controls.Add(btnUpload);
            panel1.Location = new Point(255, 318);
            panel1.Name = "panel1";
            panel1.Size = new Size(416, 177);
            panel1.TabIndex = 1931;
            // 
            // listAttachments
            // 
            listAttachments.BorderStyle = BorderStyle.None;
            listAttachments.Font = new Font("Verdana", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listAttachments.ForeColor = SystemColors.GrayText;
            listAttachments.FormattingEnabled = true;
            listAttachments.ItemHeight = 13;
            listAttachments.Location = new Point(164, 148);
            listAttachments.Margin = new Padding(0, 3, 3, 3);
            listAttachments.Name = "listAttachments";
            listAttachments.Size = new Size(129, 26);
            listAttachments.TabIndex = 1932;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 8F, FontStyle.Italic);
            label1.ForeColor = SystemColors.GrayText;
            label1.Location = new Point(84, 119);
            label1.Name = "label1";
            label1.Size = new Size(252, 13);
            label1.TabIndex = 0;
            label1.Text = "Files Supported: PDF, JPG, JPEG, TXT, DOC";
            // 
            // lblEngagement
            // 
            lblEngagement.AutoSize = true;
            lblEngagement.Font = new Font("Verdana", 8F, FontStyle.Italic);
            lblEngagement.ForeColor = SystemColors.GrayText;
            lblEngagement.Location = new Point(255, 609);
            lblEngagement.Name = "lblEngagement";
            lblEngagement.Size = new Size(155, 13);
            lblEngagement.TabIndex = 1932;
            lblEngagement.Text = "Let's track your Progress!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 8F, FontStyle.Bold);
            label2.Location = new Point(38, 583);
            label2.Name = "label2";
            label2.Size = new Size(103, 13);
            label2.TabIndex = 1934;
            label2.Text = "Your Progress:";
            label2.Click += label2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.MunicipalityCover;
            pictureBox1.Location = new Point(34, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1935;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click_1;
            // 
            // ReportIssue
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.OldLace;
            ClientSize = new Size(888, 668);
            Controls.Add(label2);
            Controls.Add(lblEngagement);
            Controls.Add(comboBox1);
            Controls.Add(txtDescription);
            Controls.Add(panel1);
            Controls.Add(btnBack);
            Controls.Add(btnSubmit);
            Controls.Add(progressBar);
            Controls.Add(txtLocation);
            Controls.Add(Category);
            Controls.Add(Description);
            Controls.Add(AttachFile);
            Controls.Add(Location);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Name = "ReportIssue";
            Text = "Report An Issue";
            Load += ReportIssue_Load;
            ((System.ComponentModel.ISupportInitialize)picUpload).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Label Location;
        private Label AttachFile;
        private Label Description;
        private Label Category;
        private TextBox txtLocation;
        private ComboBox comboBox1;
        private RichTextBox txtDescription;
        private ImageList imageList;
        private PictureBox picUpload;
        private Button btnUpload;
        private ProgressBar progressBar;
        private Button btnSubmit;
        private Button btnBack;
        private Panel panel1;
        private Label label1;
        private ListBox listAttachments;
        private Label lblEngagement;
        private Label label2;
        private PictureBox pictureBox1;
    }
}