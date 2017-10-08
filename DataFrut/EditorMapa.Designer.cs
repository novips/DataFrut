namespace DataFrut
{
    partial class EditorMapa
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
            this.txtNroPoligono = new System.Windows.Forms.TextBox();
            this.lbltipoPoligono = new System.Windows.Forms.Label();
            this.radioSectores = new System.Windows.Forms.RadioButton();
            this.radioBloques = new System.Windows.Forms.RadioButton();
            this.checkBoxCentro = new System.Windows.Forms.CheckBox();
            this.btnCrearPoligono = new System.Windows.Forms.Button();
            this.dgvVertices = new System.Windows.Forms.DataGridView();
            this.Vertice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoordY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDibujar = new System.Windows.Forms.Button();
            this.checkBoxBloquear = new System.Windows.Forms.CheckBox();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.btnCerrarEditorMapa = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSector = new System.Windows.Forms.ComboBox();
            this.cmbBloque = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxExistente = new System.Windows.Forms.CheckBox();
            this.grpBoxFiltros = new System.Windows.Forms.GroupBox();
            this.cBoxBloque = new System.Windows.Forms.CheckBox();
            this.cBoxSector = new System.Windows.Forms.CheckBox();
            this.grpBoxDibujar = new System.Windows.Forms.GroupBox();
            this.lblPolSeleccionado = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblSeleccion = new System.Windows.Forms.Label();
            this.grpBoxVertices = new System.Windows.Forms.GroupBox();
            this.cMenuStripDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moverArribaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moverAbajoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarVerticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbHileras = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.grpBoxHileras = new System.Windows.Forms.GroupBox();
            this.btnEliHilera = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.radioModificarHilera = new System.Windows.Forms.RadioButton();
            this.radioCrearHilera = new System.Windows.Forms.RadioButton();
            this.btnAgrHilera = new System.Windows.Forms.Button();
            this.txtSupHa = new System.Windows.Forms.TextBox();
            this.txtLargoHilera = new System.Windows.Forms.TextBox();
            this.txtHileras = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtKgRaiz = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.radioModificarPlantacion = new System.Windows.Forms.RadioButton();
            this.radioNuevaPlantacion = new System.Windows.Forms.RadioButton();
            this.dtpFechaPlantacion = new System.Windows.Forms.DateTimePicker();
            this.cmbTipoPlanta = new System.Windows.Forms.ComboBox();
            this.cmbFechaPlantacion = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblBloque = new System.Windows.Forms.Label();
            this.lblSectorRiego = new System.Windows.Forms.Label();
            this.btnEliminarPlantacion = new System.Windows.Forms.Button();
            this.btnAgregarPlantacion = new System.Windows.Forms.Button();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.txtPlantasEnterradas = new System.Windows.Forms.TextBox();
            this.cmbOrigen = new System.Windows.Forms.ComboBox();
            this.cmbVariedad = new System.Windows.Forms.ComboBox();
            this.tabEditor = new System.Windows.Forms.TabControl();
            this.tabPoligonos = new System.Windows.Forms.TabPage();
            this.tabHileras = new System.Windows.Forms.TabPage();
            this.grpPlantacion = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbHilerasPlantacion = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVertices)).BeginInit();
            this.grpBoxFiltros.SuspendLayout();
            this.grpBoxDibujar.SuspendLayout();
            this.grpBoxVertices.SuspendLayout();
            this.cMenuStripDGV.SuspendLayout();
            this.grpBoxHileras.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabEditor.SuspendLayout();
            this.tabPoligonos.SuspendLayout();
            this.tabHileras.SuspendLayout();
            this.grpPlantacion.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNroPoligono
            // 
            this.txtNroPoligono.Location = new System.Drawing.Point(105, 177);
            this.txtNroPoligono.Name = "txtNroPoligono";
            this.txtNroPoligono.Size = new System.Drawing.Size(54, 20);
            this.txtNroPoligono.TabIndex = 113;
            // 
            // lbltipoPoligono
            // 
            this.lbltipoPoligono.Location = new System.Drawing.Point(3, 180);
            this.lbltipoPoligono.Name = "lbltipoPoligono";
            this.lbltipoPoligono.Size = new System.Drawing.Size(96, 13);
            this.lbltipoPoligono.TabIndex = 112;
            this.lbltipoPoligono.Text = "Numero de Sector:";
            // 
            // radioSectores
            // 
            this.radioSectores.AutoSize = true;
            this.radioSectores.Checked = true;
            this.radioSectores.Location = new System.Drawing.Point(6, 19);
            this.radioSectores.Name = "radioSectores";
            this.radioSectores.Size = new System.Drawing.Size(67, 17);
            this.radioSectores.TabIndex = 99;
            this.radioSectores.TabStop = true;
            this.radioSectores.Text = "Sectores";
            this.radioSectores.UseVisualStyleBackColor = true;
            this.radioSectores.CheckedChanged += new System.EventHandler(this.radioSectores_CheckedChanged);
            // 
            // radioBloques
            // 
            this.radioBloques.AutoSize = true;
            this.radioBloques.Location = new System.Drawing.Point(6, 42);
            this.radioBloques.Name = "radioBloques";
            this.radioBloques.Size = new System.Drawing.Size(63, 17);
            this.radioBloques.TabIndex = 100;
            this.radioBloques.Text = "Bloques";
            this.radioBloques.UseVisualStyleBackColor = true;
            this.radioBloques.CheckedChanged += new System.EventHandler(this.radioBloques_CheckedChanged);
            // 
            // checkBoxCentro
            // 
            this.checkBoxCentro.AutoSize = true;
            this.checkBoxCentro.Location = new System.Drawing.Point(6, 65);
            this.checkBoxCentro.Name = "checkBoxCentro";
            this.checkBoxCentro.Size = new System.Drawing.Size(139, 17);
            this.checkBoxCentro.TabIndex = 110;
            this.checkBoxCentro.Text = "Ocultar centro del mapa";
            this.checkBoxCentro.UseVisualStyleBackColor = true;
            this.checkBoxCentro.CheckedChanged += new System.EventHandler(this.checkBoxCentro_CheckedChanged);
            // 
            // btnCrearPoligono
            // 
            this.btnCrearPoligono.Enabled = false;
            this.btnCrearPoligono.Location = new System.Drawing.Point(165, 175);
            this.btnCrearPoligono.Name = "btnCrearPoligono";
            this.btnCrearPoligono.Size = new System.Drawing.Size(75, 23);
            this.btnCrearPoligono.TabIndex = 109;
            this.btnCrearPoligono.Text = "Crear";
            this.btnCrearPoligono.UseVisualStyleBackColor = true;
            this.btnCrearPoligono.Click += new System.EventHandler(this.btnCrearPoligono_Click);
            // 
            // dgvVertices
            // 
            this.dgvVertices.AllowUserToAddRows = false;
            this.dgvVertices.AllowUserToDeleteRows = false;
            this.dgvVertices.AllowUserToResizeColumns = false;
            this.dgvVertices.AllowUserToResizeRows = false;
            this.dgvVertices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVertices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVertices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Vertice,
            this.CoordX,
            this.CoordY});
            this.dgvVertices.Enabled = false;
            this.dgvVertices.Location = new System.Drawing.Point(8, 19);
            this.dgvVertices.Name = "dgvVertices";
            this.dgvVertices.ReadOnly = true;
            this.dgvVertices.RowHeadersVisible = false;
            this.dgvVertices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVertices.Size = new System.Drawing.Size(240, 150);
            this.dgvVertices.TabIndex = 108;
            this.dgvVertices.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvVertices_MouseUp);
            // 
            // Vertice
            // 
            this.Vertice.HeaderText = "Vertice";
            this.Vertice.Name = "Vertice";
            this.Vertice.ReadOnly = true;
            // 
            // CoordX
            // 
            this.CoordX.HeaderText = "Longitud";
            this.CoordX.Name = "CoordX";
            this.CoordX.ReadOnly = true;
            // 
            // CoordY
            // 
            this.CoordY.HeaderText = "Latitud";
            this.CoordY.Name = "CoordY";
            this.CoordY.ReadOnly = true;
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(95, 30);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(75, 23);
            this.btnDibujar.TabIndex = 106;
            this.btnDibujar.Text = "Dibujar";
            this.btnDibujar.UseVisualStyleBackColor = true;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // checkBoxBloquear
            // 
            this.checkBoxBloquear.AutoSize = true;
            this.checkBoxBloquear.Location = new System.Drawing.Point(6, 88);
            this.checkBoxBloquear.Name = "checkBoxBloquear";
            this.checkBoxBloquear.Size = new System.Drawing.Size(170, 17);
            this.checkBoxBloquear.TabIndex = 105;
            this.checkBoxBloquear.Text = "Bloquear movimiento del mapa";
            this.checkBoxBloquear.UseVisualStyleBackColor = true;
            this.checkBoxBloquear.CheckedChanged += new System.EventHandler(this.checkBoxBloquear_CheckedChanged);
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(12, 12);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 50;
            this.gmap.MinZoom = 3;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(710, 572);
            this.gmap.TabIndex = 114;
            this.gmap.Zoom = 0D;
            this.gmap.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.gmap_OnPolygonClick);
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            this.gmap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseClick);
            // 
            // btnCerrarEditorMapa
            // 
            this.btnCerrarEditorMapa.Location = new System.Drawing.Point(1095, 590);
            this.btnCerrarEditorMapa.Name = "btnCerrarEditorMapa";
            this.btnCerrarEditorMapa.Size = new System.Drawing.Size(75, 23);
            this.btnCerrarEditorMapa.TabIndex = 115;
            this.btnCerrarEditorMapa.Text = "Cerrar";
            this.btnCerrarEditorMapa.UseVisualStyleBackColor = true;
            this.btnCerrarEditorMapa.Click += new System.EventHandler(this.btnCerrarEditorMapa_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 116;
            this.label2.Text = "Sector de riego:";
            // 
            // cmbSector
            // 
            this.cmbSector.Enabled = false;
            this.cmbSector.FormattingEnabled = true;
            this.cmbSector.Location = new System.Drawing.Point(105, 203);
            this.cmbSector.Name = "cmbSector";
            this.cmbSector.Size = new System.Drawing.Size(54, 21);
            this.cmbSector.TabIndex = 117;
            // 
            // cmbBloque
            // 
            this.cmbBloque.Enabled = false;
            this.cmbBloque.FormattingEnabled = true;
            this.cmbBloque.Location = new System.Drawing.Point(105, 230);
            this.cmbBloque.Name = "cmbBloque";
            this.cmbBloque.Size = new System.Drawing.Size(54, 21);
            this.cmbBloque.TabIndex = 119;
            this.cmbBloque.SelectedIndexChanged += new System.EventHandler(this.cmbBloque_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 118;
            this.label3.Text = "Bloques:";
            // 
            // checkBoxExistente
            // 
            this.checkBoxExistente.Enabled = false;
            this.checkBoxExistente.Location = new System.Drawing.Point(165, 203);
            this.checkBoxExistente.Name = "checkBoxExistente";
            this.checkBoxExistente.Size = new System.Drawing.Size(75, 46);
            this.checkBoxExistente.TabIndex = 120;
            this.checkBoxExistente.Text = "Utilizar Bloque existente";
            this.checkBoxExistente.UseVisualStyleBackColor = true;
            this.checkBoxExistente.CheckedChanged += new System.EventHandler(this.checkBoxExistente_CheckedChanged);
            // 
            // grpBoxFiltros
            // 
            this.grpBoxFiltros.Controls.Add(this.cBoxBloque);
            this.grpBoxFiltros.Controls.Add(this.cBoxSector);
            this.grpBoxFiltros.Controls.Add(this.checkBoxCentro);
            this.grpBoxFiltros.Controls.Add(this.checkBoxBloquear);
            this.grpBoxFiltros.Location = new System.Drawing.Point(6, 25);
            this.grpBoxFiltros.Name = "grpBoxFiltros";
            this.grpBoxFiltros.Size = new System.Drawing.Size(182, 110);
            this.grpBoxFiltros.TabIndex = 121;
            this.grpBoxFiltros.TabStop = false;
            this.grpBoxFiltros.Text = "Filtros";
            // 
            // cBoxBloque
            // 
            this.cBoxBloque.AutoSize = true;
            this.cBoxBloque.Checked = true;
            this.cBoxBloque.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxBloque.Location = new System.Drawing.Point(6, 19);
            this.cBoxBloque.Name = "cBoxBloque";
            this.cBoxBloque.Size = new System.Drawing.Size(83, 17);
            this.cBoxBloque.TabIndex = 117;
            this.cBoxBloque.Text = "Ver Bloques";
            this.cBoxBloque.UseVisualStyleBackColor = true;
            this.cBoxBloque.CheckedChanged += new System.EventHandler(this.cBoxBloque_CheckedChanged);
            // 
            // cBoxSector
            // 
            this.cBoxSector.AutoSize = true;
            this.cBoxSector.Checked = true;
            this.cBoxSector.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxSector.Location = new System.Drawing.Point(6, 42);
            this.cBoxSector.Name = "cBoxSector";
            this.cBoxSector.Size = new System.Drawing.Size(87, 17);
            this.cBoxSector.TabIndex = 118;
            this.cBoxSector.Text = "Ver Sectores";
            this.cBoxSector.UseVisualStyleBackColor = true;
            this.cBoxSector.CheckedChanged += new System.EventHandler(this.cBoxSector_CheckedChanged);
            // 
            // grpBoxDibujar
            // 
            this.grpBoxDibujar.Controls.Add(this.lblPolSeleccionado);
            this.grpBoxDibujar.Controls.Add(this.btnEliminar);
            this.grpBoxDibujar.Controls.Add(this.lblSeleccion);
            this.grpBoxDibujar.Controls.Add(this.radioSectores);
            this.grpBoxDibujar.Controls.Add(this.btnDibujar);
            this.grpBoxDibujar.Controls.Add(this.radioBloques);
            this.grpBoxDibujar.Location = new System.Drawing.Point(6, 151);
            this.grpBoxDibujar.Name = "grpBoxDibujar";
            this.grpBoxDibujar.Size = new System.Drawing.Size(182, 130);
            this.grpBoxDibujar.TabIndex = 1;
            this.grpBoxDibujar.TabStop = false;
            this.grpBoxDibujar.Text = "Sectores y bloques";
            // 
            // lblPolSeleccionado
            // 
            this.lblPolSeleccionado.AutoSize = true;
            this.lblPolSeleccionado.Location = new System.Drawing.Point(119, 74);
            this.lblPolSeleccionado.Name = "lblPolSeleccionado";
            this.lblPolSeleccionado.Size = new System.Drawing.Size(0, 13);
            this.lblPolSeleccionado.TabIndex = 109;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Location = new System.Drawing.Point(54, 98);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 107;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblSeleccion
            // 
            this.lblSeleccion.AutoSize = true;
            this.lblSeleccion.Location = new System.Drawing.Point(6, 74);
            this.lblSeleccion.Name = "lblSeleccion";
            this.lblSeleccion.Size = new System.Drawing.Size(107, 13);
            this.lblSeleccion.TabIndex = 108;
            this.lblSeleccion.Text = "Sector seleccionado:";
            this.lblSeleccion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpBoxVertices
            // 
            this.grpBoxVertices.Controls.Add(this.dgvVertices);
            this.grpBoxVertices.Controls.Add(this.btnCrearPoligono);
            this.grpBoxVertices.Controls.Add(this.lbltipoPoligono);
            this.grpBoxVertices.Controls.Add(this.checkBoxExistente);
            this.grpBoxVertices.Controls.Add(this.txtNroPoligono);
            this.grpBoxVertices.Controls.Add(this.cmbBloque);
            this.grpBoxVertices.Controls.Add(this.label2);
            this.grpBoxVertices.Controls.Add(this.label3);
            this.grpBoxVertices.Controls.Add(this.cmbSector);
            this.grpBoxVertices.Location = new System.Drawing.Point(194, 25);
            this.grpBoxVertices.Name = "grpBoxVertices";
            this.grpBoxVertices.Size = new System.Drawing.Size(254, 256);
            this.grpBoxVertices.TabIndex = 122;
            this.grpBoxVertices.TabStop = false;
            this.grpBoxVertices.Text = "Vertices";
            // 
            // cMenuStripDGV
            // 
            this.cMenuStripDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moverArribaToolStripMenuItem,
            this.moverAbajoToolStripMenuItem,
            this.eliminarVerticeToolStripMenuItem});
            this.cMenuStripDGV.Name = "cMenuStripDGV";
            this.cMenuStripDGV.Size = new System.Drawing.Size(156, 70);
            // 
            // moverArribaToolStripMenuItem
            // 
            this.moverArribaToolStripMenuItem.Name = "moverArribaToolStripMenuItem";
            this.moverArribaToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.moverArribaToolStripMenuItem.Text = "Mover arriba";
            this.moverArribaToolStripMenuItem.Click += new System.EventHandler(this.moverArribaToolStripMenuItem_Click);
            // 
            // moverAbajoToolStripMenuItem
            // 
            this.moverAbajoToolStripMenuItem.Name = "moverAbajoToolStripMenuItem";
            this.moverAbajoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.moverAbajoToolStripMenuItem.Text = "Mover Abajo";
            this.moverAbajoToolStripMenuItem.Click += new System.EventHandler(this.moverAbajoToolStripMenuItem_Click);
            // 
            // eliminarVerticeToolStripMenuItem
            // 
            this.eliminarVerticeToolStripMenuItem.Name = "eliminarVerticeToolStripMenuItem";
            this.eliminarVerticeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.eliminarVerticeToolStripMenuItem.Text = "Eliminar vertice";
            this.eliminarVerticeToolStripMenuItem.Click += new System.EventHandler(this.eliminarVerticeToolStripMenuItem_Click);
            // 
            // cmbHileras
            // 
            this.cmbHileras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHileras.FormattingEnabled = true;
            this.cmbHileras.Location = new System.Drawing.Point(109, 91);
            this.cmbHileras.Name = "cmbHileras";
            this.cmbHileras.Size = new System.Drawing.Size(100, 21);
            this.cmbHileras.TabIndex = 135;
            this.cmbHileras.SelectedIndexChanged += new System.EventHandler(this.cmbHileras_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(282, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 132;
            this.label10.Text = "Factor";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 131;
            this.label9.Text = "Nro. plantas enterradas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 130;
            this.label8.Text = "Origen";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 128;
            this.label6.Text = "Tipo de planta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 127;
            this.label5.Text = "Variedad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 126;
            this.label4.Text = "Sup. Ha.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "Largo hilera";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(224, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 24);
            this.label11.TabIndex = 124;
            this.label11.Text = "Sector riego";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(32, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 24);
            this.label17.TabIndex = 123;
            this.label17.Text = "Bloque";
            // 
            // grpBoxHileras
            // 
            this.grpBoxHileras.Controls.Add(this.btnEliHilera);
            this.grpBoxHileras.Controls.Add(this.panel1);
            this.grpBoxHileras.Controls.Add(this.btnAgrHilera);
            this.grpBoxHileras.Controls.Add(this.txtSupHa);
            this.grpBoxHileras.Controls.Add(this.txtLargoHilera);
            this.grpBoxHileras.Controls.Add(this.txtHileras);
            this.grpBoxHileras.Controls.Add(this.label12);
            this.grpBoxHileras.Controls.Add(this.cmbHileras);
            this.grpBoxHileras.Controls.Add(this.label1);
            this.grpBoxHileras.Controls.Add(this.label4);
            this.grpBoxHileras.Enabled = false;
            this.grpBoxHileras.Location = new System.Drawing.Point(6, 57);
            this.grpBoxHileras.Name = "grpBoxHileras";
            this.grpBoxHileras.Size = new System.Drawing.Size(443, 176);
            this.grpBoxHileras.TabIndex = 136;
            this.grpBoxHileras.TabStop = false;
            this.grpBoxHileras.Text = "Hileras";
            // 
            // btnEliHilera
            // 
            this.btnEliHilera.Enabled = false;
            this.btnEliHilera.Location = new System.Drawing.Point(310, 114);
            this.btnEliHilera.Name = "btnEliHilera";
            this.btnEliHilera.Size = new System.Drawing.Size(75, 23);
            this.btnEliHilera.TabIndex = 219;
            this.btnEliHilera.Text = "Eliminar";
            this.btnEliHilera.UseVisualStyleBackColor = true;
            this.btnEliHilera.Click += new System.EventHandler(this.btnEliHilera_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.radioModificarHilera);
            this.panel1.Controls.Add(this.radioCrearHilera);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 64);
            this.panel1.TabIndex = 218;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(334, 13);
            this.label7.TabIndex = 215;
            this.label7.Text = "Seleccione si desea crear una nueva hilera o modificar una existente:";
            // 
            // radioModificarHilera
            // 
            this.radioModificarHilera.AutoSize = true;
            this.radioModificarHilera.Location = new System.Drawing.Point(201, 35);
            this.radioModificarHilera.Name = "radioModificarHilera";
            this.radioModificarHilera.Size = new System.Drawing.Size(143, 17);
            this.radioModificarHilera.TabIndex = 217;
            this.radioModificarHilera.Text = "Modificar o eliminar hilera";
            this.radioModificarHilera.UseVisualStyleBackColor = true;
            this.radioModificarHilera.CheckedChanged += new System.EventHandler(this.radioModificarHilera_CheckedChanged);
            // 
            // radioCrearHilera
            // 
            this.radioCrearHilera.AutoSize = true;
            this.radioCrearHilera.Checked = true;
            this.radioCrearHilera.Location = new System.Drawing.Point(75, 35);
            this.radioCrearHilera.Name = "radioCrearHilera";
            this.radioCrearHilera.Size = new System.Drawing.Size(111, 17);
            this.radioCrearHilera.TabIndex = 216;
            this.radioCrearHilera.TabStop = true;
            this.radioCrearHilera.Text = "Crear nueva hilera";
            this.radioCrearHilera.UseVisualStyleBackColor = true;
            this.radioCrearHilera.CheckedChanged += new System.EventHandler(this.radioCrearHilera_CheckedChanged);
            // 
            // btnAgrHilera
            // 
            this.btnAgrHilera.Location = new System.Drawing.Point(310, 89);
            this.btnAgrHilera.Name = "btnAgrHilera";
            this.btnAgrHilera.Size = new System.Drawing.Size(75, 23);
            this.btnAgrHilera.TabIndex = 214;
            this.btnAgrHilera.Text = "Agregar";
            this.btnAgrHilera.UseVisualStyleBackColor = true;
            this.btnAgrHilera.Click += new System.EventHandler(this.btnAgrHilera_Click);
            // 
            // txtSupHa
            // 
            this.txtSupHa.Location = new System.Drawing.Point(109, 143);
            this.txtSupHa.Name = "txtSupHa";
            this.txtSupHa.Size = new System.Drawing.Size(100, 20);
            this.txtSupHa.TabIndex = 203;
            // 
            // txtLargoHilera
            // 
            this.txtLargoHilera.Location = new System.Drawing.Point(109, 117);
            this.txtLargoHilera.Name = "txtLargoHilera";
            this.txtLargoHilera.Size = new System.Drawing.Size(100, 20);
            this.txtLargoHilera.TabIndex = 202;
            // 
            // txtHileras
            // 
            this.txtHileras.Location = new System.Drawing.Point(109, 91);
            this.txtHileras.Name = "txtHileras";
            this.txtHileras.Size = new System.Drawing.Size(100, 20);
            this.txtHileras.TabIndex = 200;
            this.txtHileras.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 136;
            this.label12.Text = "Numero de hilera";
            // 
            // txtKgRaiz
            // 
            this.txtKgRaiz.Location = new System.Drawing.Point(128, 181);
            this.txtKgRaiz.Name = "txtKgRaiz";
            this.txtKgRaiz.Size = new System.Drawing.Size(100, 20);
            this.txtKgRaiz.TabIndex = 206;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(64, 184);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 213;
            this.label14.Text = "Kg de raiz";
            // 
            // radioModificarPlantacion
            // 
            this.radioModificarPlantacion.AutoSize = true;
            this.radioModificarPlantacion.Location = new System.Drawing.Point(200, 36);
            this.radioModificarPlantacion.Name = "radioModificarPlantacion";
            this.radioModificarPlantacion.Size = new System.Drawing.Size(167, 17);
            this.radioModificarPlantacion.TabIndex = 157;
            this.radioModificarPlantacion.Text = "Modificar o eliminar plantación";
            this.radioModificarPlantacion.UseVisualStyleBackColor = true;
            this.radioModificarPlantacion.CheckedChanged += new System.EventHandler(this.radioModificarPlantacion_CheckedChanged);
            // 
            // radioNuevaPlantacion
            // 
            this.radioNuevaPlantacion.AutoSize = true;
            this.radioNuevaPlantacion.Checked = true;
            this.radioNuevaPlantacion.Location = new System.Drawing.Point(74, 36);
            this.radioNuevaPlantacion.Name = "radioNuevaPlantacion";
            this.radioNuevaPlantacion.Size = new System.Drawing.Size(114, 17);
            this.radioNuevaPlantacion.TabIndex = 156;
            this.radioNuevaPlantacion.TabStop = true;
            this.radioNuevaPlantacion.Text = "Agregar plantación";
            this.radioNuevaPlantacion.UseVisualStyleBackColor = true;
            this.radioNuevaPlantacion.CheckedChanged += new System.EventHandler(this.radioNuevaPlantacion_CheckedChanged);
            // 
            // dtpFechaPlantacion
            // 
            this.dtpFechaPlantacion.Location = new System.Drawing.Point(128, 128);
            this.dtpFechaPlantacion.Name = "dtpFechaPlantacion";
            this.dtpFechaPlantacion.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaPlantacion.TabIndex = 201;
            // 
            // cmbTipoPlanta
            // 
            this.cmbTipoPlanta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPlanta.FormattingEnabled = true;
            this.cmbTipoPlanta.Location = new System.Drawing.Point(325, 127);
            this.cmbTipoPlanta.Name = "cmbTipoPlanta";
            this.cmbTipoPlanta.Size = new System.Drawing.Size(100, 21);
            this.cmbTipoPlanta.TabIndex = 208;
            // 
            // cmbFechaPlantacion
            // 
            this.cmbFechaPlantacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFechaPlantacion.FormattingEnabled = true;
            this.cmbFechaPlantacion.Location = new System.Drawing.Point(128, 128);
            this.cmbFechaPlantacion.Name = "cmbFechaPlantacion";
            this.cmbFechaPlantacion.Size = new System.Drawing.Size(100, 21);
            this.cmbFechaPlantacion.TabIndex = 152;
            this.cmbFechaPlantacion.Visible = false;
            this.cmbFechaPlantacion.SelectedIndexChanged += new System.EventHandler(this.cmbFechaPlantacion_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 131);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 13);
            this.label13.TabIndex = 151;
            this.label13.Text = "Seleccionar Fecha";
            // 
            // lblBloque
            // 
            this.lblBloque.AutoSize = true;
            this.lblBloque.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBloque.Location = new System.Drawing.Point(102, 22);
            this.lblBloque.Name = "lblBloque";
            this.lblBloque.Size = new System.Drawing.Size(0, 24);
            this.lblBloque.TabIndex = 150;
            // 
            // lblSectorRiego
            // 
            this.lblSectorRiego.AutoSize = true;
            this.lblSectorRiego.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSectorRiego.Location = new System.Drawing.Point(342, 22);
            this.lblSectorRiego.Name = "lblSectorRiego";
            this.lblSectorRiego.Size = new System.Drawing.Size(0, 24);
            this.lblSectorRiego.TabIndex = 149;
            // 
            // btnEliminarPlantacion
            // 
            this.btnEliminarPlantacion.Enabled = false;
            this.btnEliminarPlantacion.Location = new System.Drawing.Point(244, 222);
            this.btnEliminarPlantacion.Name = "btnEliminarPlantacion";
            this.btnEliminarPlantacion.Size = new System.Drawing.Size(75, 23);
            this.btnEliminarPlantacion.TabIndex = 213;
            this.btnEliminarPlantacion.Text = "Eliminar";
            this.btnEliminarPlantacion.UseVisualStyleBackColor = true;
            this.btnEliminarPlantacion.Click += new System.EventHandler(this.btnEliminarPlantacion_Click);
            // 
            // btnAgregarPlantacion
            // 
            this.btnAgregarPlantacion.Location = new System.Drawing.Point(153, 222);
            this.btnAgregarPlantacion.Name = "btnAgregarPlantacion";
            this.btnAgregarPlantacion.Size = new System.Drawing.Size(75, 23);
            this.btnAgregarPlantacion.TabIndex = 211;
            this.btnAgregarPlantacion.Text = "Agregar";
            this.btnAgregarPlantacion.UseVisualStyleBackColor = true;
            this.btnAgregarPlantacion.Click += new System.EventHandler(this.btnAgregarPlantacion_Click);
            // 
            // txtFactor
            // 
            this.txtFactor.Location = new System.Drawing.Point(325, 101);
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(100, 20);
            this.txtFactor.TabIndex = 207;
            // 
            // txtPlantasEnterradas
            // 
            this.txtPlantasEnterradas.Location = new System.Drawing.Point(128, 154);
            this.txtPlantasEnterradas.Name = "txtPlantasEnterradas";
            this.txtPlantasEnterradas.Size = new System.Drawing.Size(100, 20);
            this.txtPlantasEnterradas.TabIndex = 205;
            // 
            // cmbOrigen
            // 
            this.cmbOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrigen.FormattingEnabled = true;
            this.cmbOrigen.Location = new System.Drawing.Point(325, 181);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(100, 21);
            this.cmbOrigen.TabIndex = 210;
            this.cmbOrigen.SelectedIndexChanged += new System.EventHandler(this.cmbOrigen_SelectedIndexChanged);
            // 
            // cmbVariedad
            // 
            this.cmbVariedad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariedad.FormattingEnabled = true;
            this.cmbVariedad.Location = new System.Drawing.Point(325, 154);
            this.cmbVariedad.Name = "cmbVariedad";
            this.cmbVariedad.Size = new System.Drawing.Size(100, 21);
            this.cmbVariedad.TabIndex = 209;
            this.cmbVariedad.SelectedIndexChanged += new System.EventHandler(this.cmbVariedad_SelectedIndexChanged);
            // 
            // tabEditor
            // 
            this.tabEditor.Controls.Add(this.tabPoligonos);
            this.tabEditor.Controls.Add(this.tabHileras);
            this.tabEditor.Location = new System.Drawing.Point(728, 12);
            this.tabEditor.Name = "tabEditor";
            this.tabEditor.SelectedIndex = 0;
            this.tabEditor.Size = new System.Drawing.Size(463, 572);
            this.tabEditor.TabIndex = 214;
            // 
            // tabPoligonos
            // 
            this.tabPoligonos.Controls.Add(this.grpBoxFiltros);
            this.tabPoligonos.Controls.Add(this.grpBoxDibujar);
            this.tabPoligonos.Controls.Add(this.grpBoxVertices);
            this.tabPoligonos.Location = new System.Drawing.Point(4, 22);
            this.tabPoligonos.Name = "tabPoligonos";
            this.tabPoligonos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPoligonos.Size = new System.Drawing.Size(455, 546);
            this.tabPoligonos.TabIndex = 0;
            this.tabPoligonos.Text = "Sectores y bloques";
            this.tabPoligonos.UseVisualStyleBackColor = true;
            // 
            // tabHileras
            // 
            this.tabHileras.Controls.Add(this.grpPlantacion);
            this.tabHileras.Controls.Add(this.grpBoxHileras);
            this.tabHileras.Controls.Add(this.label17);
            this.tabHileras.Controls.Add(this.lblSectorRiego);
            this.tabHileras.Controls.Add(this.lblBloque);
            this.tabHileras.Controls.Add(this.label11);
            this.tabHileras.Location = new System.Drawing.Point(4, 22);
            this.tabHileras.Name = "tabHileras";
            this.tabHileras.Padding = new System.Windows.Forms.Padding(3);
            this.tabHileras.Size = new System.Drawing.Size(455, 546);
            this.tabHileras.TabIndex = 1;
            this.tabHileras.Text = "Hileras y plantacion";
            this.tabHileras.UseVisualStyleBackColor = true;
            // 
            // grpPlantacion
            // 
            this.grpPlantacion.Controls.Add(this.label16);
            this.grpPlantacion.Controls.Add(this.cmbHilerasPlantacion);
            this.grpPlantacion.Controls.Add(this.panel2);
            this.grpPlantacion.Controls.Add(this.txtKgRaiz);
            this.grpPlantacion.Controls.Add(this.label9);
            this.grpPlantacion.Controls.Add(this.dtpFechaPlantacion);
            this.grpPlantacion.Controls.Add(this.cmbFechaPlantacion);
            this.grpPlantacion.Controls.Add(this.btnEliminarPlantacion);
            this.grpPlantacion.Controls.Add(this.label14);
            this.grpPlantacion.Controls.Add(this.btnAgregarPlantacion);
            this.grpPlantacion.Controls.Add(this.label13);
            this.grpPlantacion.Controls.Add(this.label6);
            this.grpPlantacion.Controls.Add(this.label8);
            this.grpPlantacion.Controls.Add(this.label5);
            this.grpPlantacion.Controls.Add(this.label10);
            this.grpPlantacion.Controls.Add(this.cmbTipoPlanta);
            this.grpPlantacion.Controls.Add(this.cmbVariedad);
            this.grpPlantacion.Controls.Add(this.cmbOrigen);
            this.grpPlantacion.Controls.Add(this.txtPlantasEnterradas);
            this.grpPlantacion.Controls.Add(this.txtFactor);
            this.grpPlantacion.Location = new System.Drawing.Point(6, 239);
            this.grpPlantacion.Name = "grpPlantacion";
            this.grpPlantacion.Size = new System.Drawing.Size(443, 256);
            this.grpPlantacion.TabIndex = 137;
            this.grpPlantacion.TabStop = false;
            this.grpPlantacion.Text = "Plantación";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(35, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 13);
            this.label16.TabIndex = 216;
            this.label16.Text = "Numero de hilera";
            // 
            // cmbHilerasPlantacion
            // 
            this.cmbHilerasPlantacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHilerasPlantacion.FormattingEnabled = true;
            this.cmbHilerasPlantacion.Location = new System.Drawing.Point(128, 101);
            this.cmbHilerasPlantacion.Name = "cmbHilerasPlantacion";
            this.cmbHilerasPlantacion.Size = new System.Drawing.Size(100, 21);
            this.cmbHilerasPlantacion.TabIndex = 215;
            this.cmbHilerasPlantacion.SelectedIndexChanged += new System.EventHandler(this.cmbHilerasPlantacion_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.radioModificarPlantacion);
            this.panel2.Controls.Add(this.radioNuevaPlantacion);
            this.panel2.Location = new System.Drawing.Point(6, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(431, 62);
            this.panel2.TabIndex = 214;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(42, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(335, 13);
            this.label15.TabIndex = 216;
            this.label15.Text = "Seleccione si desea agregar o modificar una plantacion de una hilera:";
            // 
            // EditorMapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 622);
            this.Controls.Add(this.tabEditor);
            this.Controls.Add(this.btnCerrarEditorMapa);
            this.Controls.Add(this.gmap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EditorMapa";
            this.Text = "Editor Mapa";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVertices)).EndInit();
            this.grpBoxFiltros.ResumeLayout(false);
            this.grpBoxFiltros.PerformLayout();
            this.grpBoxDibujar.ResumeLayout(false);
            this.grpBoxDibujar.PerformLayout();
            this.grpBoxVertices.ResumeLayout(false);
            this.grpBoxVertices.PerformLayout();
            this.cMenuStripDGV.ResumeLayout(false);
            this.grpBoxHileras.ResumeLayout(false);
            this.grpBoxHileras.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabEditor.ResumeLayout(false);
            this.tabPoligonos.ResumeLayout(false);
            this.tabHileras.ResumeLayout(false);
            this.tabHileras.PerformLayout();
            this.grpPlantacion.ResumeLayout(false);
            this.grpPlantacion.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNroPoligono;
        private System.Windows.Forms.Label lbltipoPoligono;
        private System.Windows.Forms.RadioButton radioSectores;
        private System.Windows.Forms.RadioButton radioBloques;
        private System.Windows.Forms.CheckBox checkBoxCentro;
        private System.Windows.Forms.Button btnCrearPoligono;
        private System.Windows.Forms.DataGridView dgvVertices;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vertice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordX;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoordY;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.CheckBox checkBoxBloquear;
        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Button btnCerrarEditorMapa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSector;
        private System.Windows.Forms.ComboBox cmbBloque;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxExistente;
        private System.Windows.Forms.GroupBox grpBoxFiltros;
        private System.Windows.Forms.CheckBox cBoxBloque;
        private System.Windows.Forms.CheckBox cBoxSector;
        private System.Windows.Forms.GroupBox grpBoxDibujar;
        private System.Windows.Forms.GroupBox grpBoxVertices;
        private System.Windows.Forms.ContextMenuStrip cMenuStripDGV;
        private System.Windows.Forms.ToolStripMenuItem moverArribaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moverAbajoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarVerticeToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbHileras;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox grpBoxHileras;
        private System.Windows.Forms.ComboBox cmbOrigen;
        private System.Windows.Forms.ComboBox cmbVariedad;
        private System.Windows.Forms.TextBox txtSupHa;
        private System.Windows.Forms.TextBox txtLargoHilera;
        private System.Windows.Forms.TextBox txtHileras;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.TextBox txtPlantasEnterradas;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblSeleccion;
        private System.Windows.Forms.Label lblPolSeleccionado;
        private System.Windows.Forms.Button btnEliminarPlantacion;
        private System.Windows.Forms.Button btnAgregarPlantacion;
        private System.Windows.Forms.Label lblBloque;
        private System.Windows.Forms.Label lblSectorRiego;
        private System.Windows.Forms.ComboBox cmbFechaPlantacion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbTipoPlanta;
        private System.Windows.Forms.DateTimePicker dtpFechaPlantacion;
        private System.Windows.Forms.RadioButton radioModificarPlantacion;
        private System.Windows.Forms.RadioButton radioNuevaPlantacion;
        private System.Windows.Forms.TextBox txtKgRaiz;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabControl tabEditor;
        private System.Windows.Forms.TabPage tabPoligonos;
        private System.Windows.Forms.TabPage tabHileras;
        private System.Windows.Forms.GroupBox grpPlantacion;
        private System.Windows.Forms.Button btnAgrHilera;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioModificarHilera;
        private System.Windows.Forms.RadioButton radioCrearHilera;
        private System.Windows.Forms.Button btnEliHilera;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbHilerasPlantacion;
    }
}