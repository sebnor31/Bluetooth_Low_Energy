partial class TdsBleForm
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
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
         System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
         System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
         this.StartButton = new System.Windows.Forms.Button();
         this.displayTextBox = new System.Windows.Forms.TextBox();
         this.sensorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
         this.servicesGroupBox = new System.Windows.Forms.GroupBox();
         this.mpuMagnetoRadio = new System.Windows.Forms.RadioButton();
         this.mpuGyroRadio = new System.Windows.Forms.RadioButton();
         this.accelRadio = new System.Windows.Forms.RadioButton();
         this.sensorMagn4Radio = new System.Windows.Forms.RadioButton();
         this.sensorMagn3Radio = new System.Windows.Forms.RadioButton();
         this.sensorMagn2Radio = new System.Windows.Forms.RadioButton();
         this.sensorMagn1Radio = new System.Windows.Forms.RadioButton();
         this.exitButton = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.sensorChart)).BeginInit();
         this.servicesGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // StartButton
         // 
         this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.StartButton.ForeColor = System.Drawing.Color.ForestGreen;
         this.StartButton.Location = new System.Drawing.Point(1097, 15);
         this.StartButton.Name = "StartButton";
         this.StartButton.Size = new System.Drawing.Size(139, 32);
         this.StartButton.TabIndex = 0;
         this.StartButton.Text = "Begin Test";
         this.StartButton.UseVisualStyleBackColor = true;
         this.StartButton.Click += new System.EventHandler(this.mainButton_Click);
         // 
         // displayTextBox
         // 
         this.displayTextBox.AcceptsReturn = true;
         this.displayTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.displayTextBox.Location = new System.Drawing.Point(621, 12);
         this.displayTextBox.Multiline = true;
         this.displayTextBox.Name = "displayTextBox";
         this.displayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.displayTextBox.Size = new System.Drawing.Size(457, 73);
         this.displayTextBox.TabIndex = 1;
         this.displayTextBox.Text = "Select a GATT Service...";
         // 
         // sensorChart
         // 
         this.sensorChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.sensorChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.None;
         this.sensorChart.CausesValidation = false;
         chartArea4.Name = "ChartArea1";
         chartArea5.Name = "ChartArea2";
         chartArea6.Name = "ChartArea3";
         this.sensorChart.ChartAreas.Add(chartArea4);
         this.sensorChart.ChartAreas.Add(chartArea5);
         this.sensorChart.ChartAreas.Add(chartArea6);
         this.sensorChart.Location = new System.Drawing.Point(0, 91);
         this.sensorChart.Name = "sensorChart";
         series4.ChartArea = "ChartArea1";
         series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
         series4.Name = "X-Axis";
         series5.ChartArea = "ChartArea2";
         series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
         series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
         series5.Name = "Y-Axis";
         series6.ChartArea = "ChartArea3";
         series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
         series6.Color = System.Drawing.Color.Olive;
         series6.Name = "Z-Axis";
         this.sensorChart.Series.Add(series4);
         this.sensorChart.Series.Add(series5);
         this.sensorChart.Series.Add(series6);
         this.sensorChart.Size = new System.Drawing.Size(1248, 567);
         this.sensorChart.TabIndex = 3;
         this.sensorChart.Text = "Sensor Data";
         this.sensorChart.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
         title4.Alignment = System.Drawing.ContentAlignment.TopCenter;
         title4.DockedToChartArea = "ChartArea1";
         title4.DockingOffset = -4;
         title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
         title4.Name = "X_Axis_Title";
         title4.Text = "X - Axis";
         title5.Alignment = System.Drawing.ContentAlignment.TopCenter;
         title5.DockedToChartArea = "ChartArea2";
         title5.DockingOffset = -5;
         title5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
         title5.Name = "Y_Axis_title";
         title5.Text = "Y - Axis";
         title6.Alignment = System.Drawing.ContentAlignment.TopCenter;
         title6.DockedToChartArea = "ChartArea3";
         title6.DockingOffset = -5;
         title6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
         title6.Name = "Z_Axis_Title";
         title6.Text = "Z - Axis";
         this.sensorChart.Titles.Add(title4);
         this.sensorChart.Titles.Add(title5);
         this.sensorChart.Titles.Add(title6);
         // 
         // servicesGroupBox
         // 
         this.servicesGroupBox.BackColor = System.Drawing.Color.LightSteelBlue;
         this.servicesGroupBox.Controls.Add(this.mpuMagnetoRadio);
         this.servicesGroupBox.Controls.Add(this.mpuGyroRadio);
         this.servicesGroupBox.Controls.Add(this.accelRadio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn4Radio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn3Radio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn2Radio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn1Radio);
         this.servicesGroupBox.Location = new System.Drawing.Point(13, 12);
         this.servicesGroupBox.Name = "servicesGroupBox";
         this.servicesGroupBox.Size = new System.Drawing.Size(602, 73);
         this.servicesGroupBox.TabIndex = 4;
         this.servicesGroupBox.TabStop = false;
         this.servicesGroupBox.Text = "GATT Services";
         // 
         // mpuMagnetoRadio
         // 
         this.mpuMagnetoRadio.AutoSize = true;
         this.mpuMagnetoRadio.Location = new System.Drawing.Point(502, 30);
         this.mpuMagnetoRadio.Name = "mpuMagnetoRadio";
         this.mpuMagnetoRadio.Size = new System.Drawing.Size(97, 17);
         this.mpuMagnetoRadio.TabIndex = 3;
         this.mpuMagnetoRadio.Text = "MPU Magneto.";
         this.mpuMagnetoRadio.UseVisualStyleBackColor = true;
         this.mpuMagnetoRadio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // mpuGyroRadio
         // 
         this.mpuGyroRadio.AutoSize = true;
         this.mpuGyroRadio.Location = new System.Drawing.Point(422, 30);
         this.mpuGyroRadio.Name = "mpuGyroRadio";
         this.mpuGyroRadio.Size = new System.Drawing.Size(74, 17);
         this.mpuGyroRadio.TabIndex = 2;
         this.mpuGyroRadio.Text = "MPU Gyro";
         this.mpuGyroRadio.UseVisualStyleBackColor = true;
         this.mpuGyroRadio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // accelRadio
         // 
         this.accelRadio.AutoSize = true;
         this.accelRadio.Location = new System.Drawing.Point(319, 30);
         this.accelRadio.Name = "accelRadio";
         this.accelRadio.Size = new System.Drawing.Size(97, 17);
         this.accelRadio.TabIndex = 1;
         this.accelRadio.Text = "MPU Accelero.";
         this.accelRadio.UseVisualStyleBackColor = true;
         this.accelRadio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn4Radio
         // 
         this.sensorMagn4Radio.AutoSize = true;
         this.sensorMagn4Radio.Location = new System.Drawing.Point(243, 30);
         this.sensorMagn4Radio.Name = "sensorMagn4Radio";
         this.sensorMagn4Radio.Size = new System.Drawing.Size(70, 17);
         this.sensorMagn4Radio.TabIndex = 0;
         this.sensorMagn4Radio.Text = "Magnet 4";
         this.sensorMagn4Radio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.sensorMagn4Radio.UseVisualStyleBackColor = true;
         this.sensorMagn4Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn3Radio
         // 
         this.sensorMagn3Radio.AutoSize = true;
         this.sensorMagn3Radio.Location = new System.Drawing.Point(167, 30);
         this.sensorMagn3Radio.Name = "sensorMagn3Radio";
         this.sensorMagn3Radio.Size = new System.Drawing.Size(70, 17);
         this.sensorMagn3Radio.TabIndex = 0;
         this.sensorMagn3Radio.Text = "Magnet 3";
         this.sensorMagn3Radio.UseVisualStyleBackColor = true;
         this.sensorMagn3Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn2Radio
         // 
         this.sensorMagn2Radio.AutoSize = true;
         this.sensorMagn2Radio.Location = new System.Drawing.Point(91, 30);
         this.sensorMagn2Radio.Name = "sensorMagn2Radio";
         this.sensorMagn2Radio.Size = new System.Drawing.Size(70, 17);
         this.sensorMagn2Radio.TabIndex = 0;
         this.sensorMagn2Radio.Text = "Magnet 2";
         this.sensorMagn2Radio.UseVisualStyleBackColor = true;
         this.sensorMagn2Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn1Radio
         // 
         this.sensorMagn1Radio.AutoSize = true;
         this.sensorMagn1Radio.Checked = true;
         this.sensorMagn1Radio.Location = new System.Drawing.Point(6, 30);
         this.sensorMagn1Radio.Name = "sensorMagn1Radio";
         this.sensorMagn1Radio.Size = new System.Drawing.Size(70, 17);
         this.sensorMagn1Radio.TabIndex = 0;
         this.sensorMagn1Radio.TabStop = true;
         this.sensorMagn1Radio.Text = "Magnet 1";
         this.sensorMagn1Radio.UseVisualStyleBackColor = true;
         this.sensorMagn1Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // exitButton
         // 
         this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.exitButton.BackColor = System.Drawing.SystemColors.Control;
         this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.exitButton.ForeColor = System.Drawing.Color.Red;
         this.exitButton.Location = new System.Drawing.Point(1097, 53);
         this.exitButton.Name = "exitButton";
         this.exitButton.Size = new System.Drawing.Size(137, 32);
         this.exitButton.TabIndex = 5;
         this.exitButton.Text = "Exit";
         this.exitButton.UseVisualStyleBackColor = false;
         this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
         // 
         // TdsBleForm
         // 
         this.AcceptButton = this.StartButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.exitButton;
         this.ClientSize = new System.Drawing.Size(1248, 657);
         this.Controls.Add(this.exitButton);
         this.Controls.Add(this.servicesGroupBox);
         this.Controls.Add(this.sensorChart);
         this.Controls.Add(this.displayTextBox);
         this.Controls.Add(this.StartButton);
         this.DoubleBuffered = true;
         this.Name = "TdsBleForm";
         this.Text = "TDS BLE Test";
         ((System.ComponentModel.ISupportInitialize)(this.sensorChart)).EndInit();
         this.servicesGroupBox.ResumeLayout(false);
         this.servicesGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.Button StartButton;
   private System.Windows.Forms.TextBox displayTextBox;
   private System.Windows.Forms.DataVisualization.Charting.Chart sensorChart;
   private System.Windows.Forms.GroupBox servicesGroupBox;
   private System.Windows.Forms.RadioButton mpuMagnetoRadio;
   private System.Windows.Forms.RadioButton mpuGyroRadio;
   private System.Windows.Forms.RadioButton accelRadio;
   private System.Windows.Forms.RadioButton sensorMagn1Radio;
   private System.Windows.Forms.RadioButton sensorMagn4Radio;
   private System.Windows.Forms.RadioButton sensorMagn3Radio;
   private System.Windows.Forms.RadioButton sensorMagn2Radio;
   private System.Windows.Forms.Button exitButton;
}


