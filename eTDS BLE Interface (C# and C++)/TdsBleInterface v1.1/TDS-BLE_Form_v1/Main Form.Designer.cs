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
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
         System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
         System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
         this.mainButton = new System.Windows.Forms.Button();
         this.displayTextBox = new System.Windows.Forms.TextBox();
         this.sensorReadout = new System.Windows.Forms.TextBox();
         this.sensorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
         this.servicesGroupBox = new System.Windows.Forms.GroupBox();
         this.mpuMagnetoRadio = new System.Windows.Forms.RadioButton();
         this.mpuGyroRadio = new System.Windows.Forms.RadioButton();
         this.accelRadio = new System.Windows.Forms.RadioButton();
         this.sensorMagn4Radio = new System.Windows.Forms.RadioButton();
         this.sensorMagn3Radio = new System.Windows.Forms.RadioButton();
         this.sensorMagn2Radio = new System.Windows.Forms.RadioButton();
         this.sensorMagn1Radio = new System.Windows.Forms.RadioButton();
         ((System.ComponentModel.ISupportInitialize)(this.sensorChart)).BeginInit();
         this.servicesGroupBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // mainButton
         // 
         this.mainButton.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.mainButton.Location = new System.Drawing.Point(0, 655);
         this.mainButton.Name = "mainButton";
         this.mainButton.Size = new System.Drawing.Size(980, 37);
         this.mainButton.TabIndex = 0;
         this.mainButton.Text = "Begin Test";
         this.mainButton.UseVisualStyleBackColor = true;
         this.mainButton.Click += new System.EventHandler(this.mainButton_Click);
         // 
         // displayTextBox
         // 
         this.displayTextBox.AcceptsReturn = true;
         this.displayTextBox.Location = new System.Drawing.Point(155, 12);
         this.displayTextBox.Multiline = true;
         this.displayTextBox.Name = "displayTextBox";
         this.displayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.displayTextBox.Size = new System.Drawing.Size(531, 182);
         this.displayTextBox.TabIndex = 1;
         this.displayTextBox.Text = "Select a GATT Service...";
         // 
         // sensorReadout
         // 
         this.sensorReadout.AcceptsReturn = true;
         this.sensorReadout.AcceptsTab = true;
         this.sensorReadout.Location = new System.Drawing.Point(714, 12);
         this.sensorReadout.Multiline = true;
         this.sensorReadout.Name = "sensorReadout";
         this.sensorReadout.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.sensorReadout.Size = new System.Drawing.Size(157, 123);
         this.sensorReadout.TabIndex = 2;
         // 
         // sensorChart
         // 
         this.sensorChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         chartArea1.Name = "ChartArea1";
         chartArea2.Name = "ChartArea2";
         chartArea3.Name = "ChartArea3";
         this.sensorChart.ChartAreas.Add(chartArea1);
         this.sensorChart.ChartAreas.Add(chartArea2);
         this.sensorChart.ChartAreas.Add(chartArea3);
         this.sensorChart.Location = new System.Drawing.Point(0, 200);
         this.sensorChart.Name = "sensorChart";
         series1.ChartArea = "ChartArea1";
         series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
         series1.Name = "X-Axis";
         series2.ChartArea = "ChartArea2";
         series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
         series2.Name = "Y-Axis";
         series3.ChartArea = "ChartArea3";
         series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
         series3.Name = "Z-Axis";
         this.sensorChart.Series.Add(series1);
         this.sensorChart.Series.Add(series2);
         this.sensorChart.Series.Add(series3);
         this.sensorChart.Size = new System.Drawing.Size(980, 449);
         this.sensorChart.TabIndex = 3;
         this.sensorChart.Text = "Sensor Data";
         title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
         title1.DockedToChartArea = "ChartArea1";
         title1.DockingOffset = -5;
         title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
         title1.Name = "X_Axis_Title";
         title1.Text = "X - Axis";
         title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
         title2.DockedToChartArea = "ChartArea2";
         title2.DockingOffset = -5;
         title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
         title2.Name = "Y_Axis_title";
         title2.Text = "Y - Axis";
         title3.Alignment = System.Drawing.ContentAlignment.TopCenter;
         title3.DockedToChartArea = "ChartArea3";
         title3.DockingOffset = -5;
         title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
         title3.Name = "Z_Axis_Title";
         title3.Text = "Z - Axis";
         this.sensorChart.Titles.Add(title1);
         this.sensorChart.Titles.Add(title2);
         this.sensorChart.Titles.Add(title3);
         // 
         // servicesGroupBox
         // 
         this.servicesGroupBox.Controls.Add(this.mpuMagnetoRadio);
         this.servicesGroupBox.Controls.Add(this.mpuGyroRadio);
         this.servicesGroupBox.Controls.Add(this.accelRadio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn4Radio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn3Radio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn2Radio);
         this.servicesGroupBox.Controls.Add(this.sensorMagn1Radio);
         this.servicesGroupBox.Location = new System.Drawing.Point(13, 12);
         this.servicesGroupBox.Name = "servicesGroupBox";
         this.servicesGroupBox.Size = new System.Drawing.Size(125, 182);
         this.servicesGroupBox.TabIndex = 4;
         this.servicesGroupBox.TabStop = false;
         this.servicesGroupBox.Text = "GATT Services";
         // 
         // mpuMagnetoRadio
         // 
         this.mpuMagnetoRadio.AutoSize = true;
         this.mpuMagnetoRadio.Location = new System.Drawing.Point(7, 158);
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
         this.mpuGyroRadio.Location = new System.Drawing.Point(7, 135);
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
         this.accelRadio.Location = new System.Drawing.Point(7, 112);
         this.accelRadio.Name = "accelRadio";
         this.accelRadio.Size = new System.Drawing.Size(93, 17);
         this.accelRadio.TabIndex = 1;
         this.accelRadio.Text = "Accelerometer";
         this.accelRadio.UseVisualStyleBackColor = true;
         this.accelRadio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn4Radio
         // 
         this.sensorMagn4Radio.AutoSize = true;
         this.sensorMagn4Radio.Location = new System.Drawing.Point(7, 89);
         this.sensorMagn4Radio.Name = "sensorMagn4Radio";
         this.sensorMagn4Radio.Size = new System.Drawing.Size(112, 17);
         this.sensorMagn4Radio.TabIndex = 0;
         this.sensorMagn4Radio.Text = "Sensor Magnet - 4";
         this.sensorMagn4Radio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.sensorMagn4Radio.UseVisualStyleBackColor = true;
         this.sensorMagn4Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn3Radio
         // 
         this.sensorMagn3Radio.AutoSize = true;
         this.sensorMagn3Radio.Location = new System.Drawing.Point(7, 66);
         this.sensorMagn3Radio.Name = "sensorMagn3Radio";
         this.sensorMagn3Radio.Size = new System.Drawing.Size(112, 17);
         this.sensorMagn3Radio.TabIndex = 0;
         this.sensorMagn3Radio.Text = "Sensor Magnet - 3";
         this.sensorMagn3Radio.UseVisualStyleBackColor = true;
         this.sensorMagn3Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn2Radio
         // 
         this.sensorMagn2Radio.AutoSize = true;
         this.sensorMagn2Radio.Location = new System.Drawing.Point(7, 43);
         this.sensorMagn2Radio.Name = "sensorMagn2Radio";
         this.sensorMagn2Radio.Size = new System.Drawing.Size(112, 17);
         this.sensorMagn2Radio.TabIndex = 0;
         this.sensorMagn2Radio.Text = "Sensor Magnet - 2";
         this.sensorMagn2Radio.UseVisualStyleBackColor = true;
         this.sensorMagn2Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // sensorMagn1Radio
         // 
         this.sensorMagn1Radio.AutoSize = true;
         this.sensorMagn1Radio.Checked = true;
         this.sensorMagn1Radio.Location = new System.Drawing.Point(7, 20);
         this.sensorMagn1Radio.Name = "sensorMagn1Radio";
         this.sensorMagn1Radio.Size = new System.Drawing.Size(112, 17);
         this.sensorMagn1Radio.TabIndex = 0;
         this.sensorMagn1Radio.TabStop = true;
         this.sensorMagn1Radio.Text = "Sensor Magnet - 1";
         this.sensorMagn1Radio.UseVisualStyleBackColor = true;
         this.sensorMagn1Radio.CheckedChanged += new System.EventHandler(this.Service_CheckedChanged);
         // 
         // TdsBleForm
         // 
         this.AcceptButton = this.mainButton;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(980, 692);
         this.Controls.Add(this.servicesGroupBox);
         this.Controls.Add(this.sensorChart);
         this.Controls.Add(this.sensorReadout);
         this.Controls.Add(this.displayTextBox);
         this.Controls.Add(this.mainButton);
         this.Name = "TdsBleForm";
         this.Text = "TDS BLE Test";
         ((System.ComponentModel.ISupportInitialize)(this.sensorChart)).EndInit();
         this.servicesGroupBox.ResumeLayout(false);
         this.servicesGroupBox.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.Button mainButton;
   private System.Windows.Forms.TextBox displayTextBox;
   private System.Windows.Forms.TextBox sensorReadout;
   private System.Windows.Forms.DataVisualization.Charting.Chart sensorChart;
   private System.Windows.Forms.GroupBox servicesGroupBox;
   private System.Windows.Forms.RadioButton mpuMagnetoRadio;
   private System.Windows.Forms.RadioButton mpuGyroRadio;
   private System.Windows.Forms.RadioButton accelRadio;
   private System.Windows.Forms.RadioButton sensorMagn1Radio;
   private System.Windows.Forms.RadioButton sensorMagn4Radio;
   private System.Windows.Forms.RadioButton sensorMagn3Radio;
   private System.Windows.Forms.RadioButton sensorMagn2Radio;
}


