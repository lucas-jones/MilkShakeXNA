namespace MilkshakeEditor
{
    partial class Editor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.SelectToolBtn = new System.Windows.Forms.ToolStripButton();
            this.TransformToolBtn = new System.Windows.Forms.ToolStripButton();
            this.PolygonToolBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveForwardBtn = new System.Windows.Forms.ToolStripButton();
            this.MoveBackBtn = new System.Windows.Forms.ToolStripButton();
            this.EditorTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SceneToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddEntityBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.AddSpriteBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.AddSoundBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raGameObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveEntityBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.SceneTreeView = new System.Windows.Forms.TreeView();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.previewPannel = new System.Windows.Forms.PictureBox();
            this.ToolStrip.SuspendLayout();
            this.EditorTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SceneToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPannel)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(12, 509);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(238, 244);
            this.propertyGrid1.TabIndex = 2;
            this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
            // 
            // ToolStrip
            // 
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectToolBtn,
            this.TransformToolBtn,
            this.PolygonToolBtn,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.MoveForwardBtn,
            this.MoveBackBtn});
            this.ToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(1551, 25);
            this.ToolStrip.TabIndex = 3;
            this.ToolStrip.Text = "ToolStrip";
            this.ToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolStrip_ItemClicked);
            // 
            // SelectToolBtn
            // 
            this.SelectToolBtn.Checked = true;
            this.SelectToolBtn.CheckOnClick = true;
            this.SelectToolBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelectToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("SelectToolBtn.Image")));
            this.SelectToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectToolBtn.Name = "SelectToolBtn";
            this.SelectToolBtn.Size = new System.Drawing.Size(23, 22);
            this.SelectToolBtn.Text = "Select";
            this.SelectToolBtn.Click += new System.EventHandler(this.SelectToolBtn_Click);
            // 
            // TransformToolBtn
            // 
            this.TransformToolBtn.CheckOnClick = true;
            this.TransformToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TransformToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("TransformToolBtn.Image")));
            this.TransformToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TransformToolBtn.Name = "TransformToolBtn";
            this.TransformToolBtn.Size = new System.Drawing.Size(23, 22);
            this.TransformToolBtn.Text = "Transform";
            this.TransformToolBtn.Click += new System.EventHandler(this.TransformBtn_Click);
            // 
            // PolygonToolBtn
            // 
            this.PolygonToolBtn.CheckOnClick = true;
            this.PolygonToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PolygonToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("PolygonToolBtn.Image")));
            this.PolygonToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PolygonToolBtn.Name = "PolygonToolBtn";
            this.PolygonToolBtn.Size = new System.Drawing.Size(23, 22);
            this.PolygonToolBtn.Text = "Polygon Create";
            this.PolygonToolBtn.Click += new System.EventHandler(this.PolygonToolBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Checked = true;
            this.toolStripButton2.CheckOnClick = true;
            this.toolStripButton2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Debug View";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // MoveForwardBtn
            // 
            this.MoveForwardBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveForwardBtn.Image = ((System.Drawing.Image)(resources.GetObject("MoveForwardBtn.Image")));
            this.MoveForwardBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveForwardBtn.Name = "MoveForwardBtn";
            this.MoveForwardBtn.Size = new System.Drawing.Size(23, 22);
            this.MoveForwardBtn.Text = "Foward";
            this.MoveForwardBtn.Click += new System.EventHandler(this.MoveForwardBtn_Click);
            // 
            // MoveBackBtn
            // 
            this.MoveBackBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveBackBtn.Image = ((System.Drawing.Image)(resources.GetObject("MoveBackBtn.Image")));
            this.MoveBackBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveBackBtn.Name = "MoveBackBtn";
            this.MoveBackBtn.Size = new System.Drawing.Size(23, 22);
            this.MoveBackBtn.Text = "Back";
            this.MoveBackBtn.Click += new System.EventHandler(this.MoveBackBtn_Click);
            // 
            // EditorTab
            // 
            this.EditorTab.Controls.Add(this.tabPage1);
            this.EditorTab.Location = new System.Drawing.Point(12, 33);
            this.EditorTab.Name = "EditorTab";
            this.EditorTab.SelectedIndex = 0;
            this.EditorTab.Size = new System.Drawing.Size(241, 470);
            this.EditorTab.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SceneToolStrip);
            this.tabPage1.Controls.Add(this.SceneTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(233, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scene";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SceneToolStrip
            // 
            this.SceneToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SceneToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddEntityBtn,
            this.RemoveEntityBtn,
            this.toolStripButton1});
            this.SceneToolStrip.Location = new System.Drawing.Point(3, 3);
            this.SceneToolStrip.Name = "SceneToolStrip";
            this.SceneToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.SceneToolStrip.Size = new System.Drawing.Size(227, 25);
            this.SceneToolStrip.TabIndex = 3;
            this.SceneToolStrip.Text = "toolStrip2";
            // 
            // AddEntityBtn
            // 
            this.AddEntityBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddEntityBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSpriteBtn,
            this.AddSoundBtn,
            this.lightToolStripMenuItem,
            this.triggerToolStripMenuItem,
            this.waterToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.raGameObjectsToolStripMenuItem});
            this.AddEntityBtn.Image = ((System.Drawing.Image)(resources.GetObject("AddEntityBtn.Image")));
            this.AddEntityBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddEntityBtn.Name = "AddEntityBtn";
            this.AddEntityBtn.Size = new System.Drawing.Size(29, 22);
            this.AddEntityBtn.Text = "toolStripDropDownButton1";
            // 
            // AddSpriteBtn
            // 
            this.AddSpriteBtn.Image = global::MilkshakeEditor.Properties.Resources.picture;
            this.AddSpriteBtn.Name = "AddSpriteBtn";
            this.AddSpriteBtn.Size = new System.Drawing.Size(145, 22);
            this.AddSpriteBtn.Text = "Sprite";
            this.AddSpriteBtn.Click += new System.EventHandler(this.AddSpriteBtn_Click);
            // 
            // AddSoundBtn
            // 
            this.AddSoundBtn.Image = global::MilkshakeEditor.Properties.Resources.sound;
            this.AddSoundBtn.Name = "AddSoundBtn";
            this.AddSoundBtn.Size = new System.Drawing.Size(145, 22);
            this.AddSoundBtn.Text = "Sound";
            this.AddSoundBtn.Click += new System.EventHandler(this.AddSoundBtn_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Image = global::MilkshakeEditor.Properties.Resources.lightbulb;
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // triggerToolStripMenuItem
            // 
            this.triggerToolStripMenuItem.Image = global::MilkshakeEditor.Properties.Resources.picture_empty;
            this.triggerToolStripMenuItem.Name = "triggerToolStripMenuItem";
            this.triggerToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.triggerToolStripMenuItem.Text = "Trigger";
            // 
            // waterToolStripMenuItem
            // 
            this.waterToolStripMenuItem.Image = global::MilkshakeEditor.Properties.Resources.drink;
            this.waterToolStripMenuItem.Name = "waterToolStripMenuItem";
            this.waterToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.waterToolStripMenuItem.Text = "Water";
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Image = global::MilkshakeEditor.Properties.Resources.folder;
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.groupToolStripMenuItem.Text = "Group";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // raGameObjectsToolStripMenuItem
            // 
            this.raGameObjectsToolStripMenuItem.Image = global::MilkshakeEditor.Properties.Resources.brick;
            this.raGameObjectsToolStripMenuItem.Name = "raGameObjectsToolStripMenuItem";
            this.raGameObjectsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.raGameObjectsToolStripMenuItem.Text = "GameObjects";
            // 
            // RemoveEntityBtn
            // 
            this.RemoveEntityBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveEntityBtn.Image = ((System.Drawing.Image)(resources.GetObject("RemoveEntityBtn.Image")));
            this.RemoveEntityBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveEntityBtn.Name = "RemoveEntityBtn";
            this.RemoveEntityBtn.Size = new System.Drawing.Size(23, 22);
            this.RemoveEntityBtn.Text = "Remove Entity";
            this.RemoveEntityBtn.Click += new System.EventHandler(this.RemoveEntityBtn_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // SceneTreeView
            // 
            this.SceneTreeView.AllowDrop = true;
            this.SceneTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SceneTreeView.ImageIndex = 0;
            this.SceneTreeView.ImageList = this.ImageList;
            this.SceneTreeView.Location = new System.Drawing.Point(3, 31);
            this.SceneTreeView.Name = "SceneTreeView";
            this.SceneTreeView.SelectedImageIndex = 0;
            this.SceneTreeView.Size = new System.Drawing.Size(227, 410);
            this.SceneTreeView.TabIndex = 2;
            this.SceneTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect_1);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "image.png");
            this.ImageList.Images.SetKeyName(1, "folder.png");
            this.ImageList.Images.SetKeyName(2, "world.png");
            this.ImageList.Images.SetKeyName(3, "brick.png");
            this.ImageList.Images.SetKeyName(4, "picture.png");
            this.ImageList.Images.SetKeyName(5, "sound.png");
            this.ImageList.Images.SetKeyName(6, "drink.png");
            this.ImageList.Images.SetKeyName(7, "world.png");
            this.ImageList.Images.SetKeyName(8, "paintbrush.png");
            this.ImageList.Images.SetKeyName(9, "vector.png");
            // 
            // previewPannel
            // 
            this.previewPannel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPannel.Location = new System.Drawing.Point(259, 33);
            this.previewPannel.Name = "previewPannel";
            this.previewPannel.Size = new System.Drawing.Size(1280, 720);
            this.previewPannel.TabIndex = 0;
            this.previewPannel.TabStop = false;
            this.previewPannel.Click += new System.EventHandler(this.previewPannel_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1551, 765);
            this.Controls.Add(this.EditorTab);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.previewPannel);
            this.Name = "Editor";
            this.Text = "Ra Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.EditorTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.SceneToolStrip.ResumeLayout(false);
            this.SceneToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPannel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox previewPannel;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton SelectToolBtn;
        private System.Windows.Forms.ToolStripButton PolygonToolBtn;
        private System.Windows.Forms.TabControl EditorTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView SceneTreeView;
        private System.Windows.Forms.ToolStrip SceneToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton AddEntityBtn;
        private System.Windows.Forms.ToolStripMenuItem AddSpriteBtn;
        private System.Windows.Forms.ToolStripButton RemoveEntityBtn;
        private System.Windows.Forms.ToolStripMenuItem AddSoundBtn;
        private System.Windows.Forms.ToolStripButton TransformToolBtn;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton MoveForwardBtn;
        private System.Windows.Forms.ToolStripButton MoveBackBtn;
        private System.Windows.Forms.ToolStripMenuItem raGameObjectsToolStripMenuItem;
        private System.Windows.Forms.ImageList ImageList;
    }
}

