using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFrut.Clases;
using GMap.NET;
using MySql.Data.MySqlClient;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using System.Collections;
using System.Text.RegularExpressions;

namespace DataFrut
{
    public partial class EditorMapa : Form
    {
        GMap.NET.WindowsForms.GMapOverlay polygons = new GMap.NET.WindowsForms.GMapOverlay("polygons");
        GMap.NET.WindowsForms.GMapOverlay polygonsBloques = new GMap.NET.WindowsForms.GMapOverlay("polygons");
        GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
        List<GMap.NET.PointLatLng> points;
        List<Vertice> lisVer;
        GMapPolygon sector;
        GMapPolygon bloque;
        int codigoUltimaHilera = 0;

        public EditorMapa()
        {
            InitializeComponent();
            gmap.Overlays.Add(polygonsBloques);
            gmap.Overlays.Add(polygons);
            gmap.Overlays.Add(markers);
            cargarCmbSectores();
            cargarCmbBloques();
            cmbHileras.Visible = false;
            txtHileras.Visible = true;
        }


        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.Position = new GMap.NET.PointLatLng(-37.1992883, -72.3719246);
            gmap.Zoom = 16;
            cargarBloques();
            cargarSectores();
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && btnDibujar.Text == "Terminar")
            {
                String labelNroVertice = (dgvVertices.Rows.Count + 1).ToString();
                double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lng = gmap.FromLocalToLatLng(e.X, e.Y).Lng;
                dgvVertices.Rows.Add((dgvVertices.Rows.Count + 1).ToString(), lng, lat);
                GMapLabeledMarker marker = new GMapLabeledMarker(new PointLatLng(lat, lng), labelNroVertice, GMarkerGoogleType.blue_small);
                markers.Markers.Add(marker);
            }
        }

        #region Polygon      
        private void gmap_OnPolygonClick(GMap.NET.WindowsForms.GMapPolygon item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (item.tipo == "bloque")
                {
                    tipoPol = "bloque";
                    grpBoxHileras.Enabled = true;
                    lblPolSeleccionado.Text = item.codigo.ToString();
                    lblBloque.Text = item.codigo.ToString();
                    cmbHileras.Items.Clear();
                    cmbHilerasPlantacion.Items.Clear();
                    Conexion.abrirConexion();
                    MySqlCommand cmd = Conexion.con.CreateCommand();
                    int codBloque = traerCodigoBloquePorNumero(Convert.ToInt32(item.codigo));
                    cmd.CommandText = "SELECT * FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = " + codBloque + " ORDER BY 3;";
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbHileras.Items.Add(new Item(dr["NUMERO_HILERA"].ToString(), codBloque));
                        cmbHilerasPlantacion.Items.Add(new Item(dr["NUMERO_HILERA"].ToString(), codBloque));
                    }
                    Conexion.cerrarConexion();
                    //if(cmbHileras.Items.Count > 0)
                    //    cmbHileras.SelectedIndex = 0;
                    btnEliminar.Enabled = true;
                    lblSeleccion.Text = "Bloque seleccionado:";
                    cargarOrigen();
                    cargarVariedades();
                    cargarTipo();
                    mostrarSectorRiego();
                }
                else if (item.tipo == "sector")
                {
                    tipoPol = "sector";
                    grpBoxHileras.Enabled = false;
                    lblPolSeleccionado.Text = item.codigo.ToString();
                    btnEliminar.Enabled = true;
                    lblSeleccion.Text = "Sector seleccionado:";
                }
            }
        }

        private void mostrarSectorRiego()
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            int codBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            cmd.CommandText = "select NUMERO_SECTOR FROM DATAFRUT_SECTORES WHERE COD_SECTOR = (select DATAFRUT_SECTORES_COD_SECTOR FROM DATAFRUT_BLOQUES WHERE COD_BLOQUE = " + codBloque + ");";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblSectorRiego.Text = dr[0].ToString();
            }
            Conexion.cerrarConexion();
        }

        private void btnCrearPoligono_Click(object sender, EventArgs e)
        {
            if (txtNroPoligono.Text != "" && dgvVertices.Rows.Count > 2)
            {
                try
                {
                    int test = Convert.ToInt32(txtNroPoligono.Text);
                    List<Vertice> vertices = new List<Vertice>();
                    Vertice vertice;
                    points = new List<GMap.NET.PointLatLng>();
                    foreach (DataGridViewRow dgvr in dgvVertices.Rows)
                    {
                        points.Add(new GMap.NET.PointLatLng(Convert.ToDouble(dgvr.Cells[2].Value), Convert.ToDouble(dgvr.Cells[1].Value)));
                        vertice = new Vertice(Convert.ToInt32(dgvr.Cells[0].Value), Convert.ToDouble(dgvr.Cells[2].Value), Convert.ToDouble(dgvr.Cells[1].Value));
                        vertices.Add(vertice);
                    }

                    if (radioSectores.Checked)
                    {
                        sector = new GMapPolygon(points, nombrePoligono());
                        sector.IsHitTestVisible = true;
                        sector.Fill = new SolidBrush(Color.FromArgb(50, Color.Blue));
                        sector.Stroke = new Pen(Color.Blue, 1);
                        try
                        {
                            if (validarNumeroSector(txtNroPoligono.Text))
                            {
                                sector.codigo = Convert.ToInt32(txtNroPoligono.Text);
                                DialogResult dialogResult = MessageBox.Show("¿Esta seguro de que desea\ncrear este sector?", "Confirmación", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    ingresarSector(txtNroPoligono.Text);
                                    ingresarPoligonoSector(vertices, txtNroPoligono.Text);
                                    polygons.Polygons.Add(sector);
                                    dgvVertices.Rows.Clear();
                                    markers.Markers.Clear();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    //do something else
                                }
                            }
                            else
                            {
                                MessageBox.Show("El valor ingresado no es valido");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else if (radioBloques.Checked)
                    {
                        bloque = new GMapPolygon(points, nombrePoligono());
                        bloque.IsHitTestVisible = true;
                        bloque.Fill = new SolidBrush(Color.FromArgb(50, Color.Green));
                        bloque.Stroke = new Pen(Color.Green, 1);
                        try
                        {
                            if (validarNumeroBloque(cmbBloque.SelectedItem.ToString()))
                            {
                                bloque.codigo = Convert.ToInt32(cmbBloque.SelectedItem);
                                DialogResult dialogResult = MessageBox.Show("¿Esta seguro de que desea\ncrear este Bloque?", "Confirmación", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (checkBoxExistente.Checked == false)
                                    {
                                        ingresarBloque(txtNroPoligono.Text, cmbSector.SelectedItem.ToString());
                                    }
                                    ingresarPoligonoBloque(vertices, txtNroPoligono.Text);
                                    polygons.Polygons.Add(bloque);
                                    dgvVertices.Rows.Clear();
                                    markers.Markers.Clear();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    //do something else
                                }
                            }
                            else
                            {
                                MessageBox.Show("El valor ingresado no es valido");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    String hola = ex.ToString();
                    MessageBox.Show("Debe ingresar un numero");
                    txtNroPoligono.Focus();
                }

            }
            else if (txtNroPoligono.Text != "" && dgvVertices.Rows.Count <= 2)
            {
                MessageBox.Show("Deben haber almenos 3 vertices");
            }
            else
            {
                MessageBox.Show("Debe ingresar un numero");
                txtNroPoligono.Focus();
            }
        }

        private string nombrePoligono()
        {
            string nombre = "";
            if (radioSectores.Checked)
            {
                nombre = "sectores";
            }
            else if (radioBloques.Checked)
            {
                nombre = "bloques";
            }
            return nombre;
        }
        #endregion

        #region sector
        private void cargarCmbSectores()
        {
            int contador = 0;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT NUMERO_SECTOR FROM DATAFRUT_SECTORES;";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt32(dr["NUMERO_SECTOR"]) <= 0)
                {

                }
                else
                {
                    int numeroSector = Convert.ToInt32(dr["NUMERO_SECTOR"]);
                    cmbSector.Items.Add(numeroSector);
                }
                contador++;
            }
            Conexion.cerrarConexion();
            cmbSector.SelectedIndex = 0;
        }
        private void cargarSectores()
        {
            List<List<Vertice>> sectoresTemporales = new List<List<Vertice>>();
            lisVer = new List<Vertice>();
            Vertice vertice;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT * FROM viewPoligonosSectores;";
            MySqlDataReader dr = cmd.ExecuteReader();
            vertice = new Vertice();
            vertice.points = new List<GMap.NET.PointLatLng>();
            int cod_sector = 0;
            bool first = false;
            while (dr.Read())
            {
                vertice = new Vertice();
                vertice.points = new List<GMap.NET.PointLatLng>();
                vertice.codigoSector = Convert.ToInt32(dr["COD_SECTOR"]);
                if (cod_sector != vertice.codigoSector)
                {
                    lisVer = new List<Vertice>();
                    vertice.codigoSector = Convert.ToInt32(dr["COD_SECTOR"]);
                    cod_sector = vertice.codigoSector;
                    if (first)
                    {
                        sectoresTemporales[0] = lisVer;
                        first = false;
                    }
                    else
                    {
                        sectoresTemporales.Add(lisVer);
                    }
                }
                vertice.latitud = Convert.ToDouble(dr["LATITUD"].ToString().Replace('.', ','));
                vertice.longitud = Convert.ToDouble(dr["LONGITUD"].ToString().Replace('.', ','));
                vertice.numeroSector = Convert.ToInt32(dr["NUMERO_SECTOR"]);
                vertice.numeroVertice = Convert.ToInt32(dr["NUMERO_VERTICE"]);
                vertice.points.Add(new GMap.NET.PointLatLng(vertice.latitud, vertice.longitud));
                lisVer.Add(vertice);
            }
            Conexion.cerrarConexion();

            foreach (List<Vertice> lisVer in sectoresTemporales)
            {
                int codSectorTemporal = -100;
                List<GMap.NET.PointLatLng> pointsTemp = new List<PointLatLng>();
                foreach (Vertice ver in lisVer)
                {
                    pointsTemp.Add(new GMap.NET.PointLatLng(ver.latitud, ver.longitud));
                    codSectorTemporal = ver.codigoSector;
                }
                sector = new GMapPolygon(pointsTemp, "sector");
                sector.tipo = "sector";
                Conexion.abrirConexion();
                MySqlCommand cmdT = Conexion.con.CreateCommand();
                cmd.CommandText = "SELECT NUMERO_SECTOR FROM DATAFRUT_SECTORES WHERE COD_SECTOR = " + codSectorTemporal + ";";
                MySqlDataReader drT = cmd.ExecuteReader();
                while (drT.Read())
                {
                    sector.codigo = Convert.ToInt32(drT["NUMERO_SECTOR"]);
                }
                Conexion.cerrarConexion();
                sector.IsHitTestVisible = true;
                sector.Fill = new SolidBrush(Color.FromArgb(50, Color.Blue));
                sector.Stroke = new Pen(Color.Blue, 1);
                polygons.Polygons.Add(sector);
            }
        }
        public bool validarNumeroSector(string numero)
        {
            bool existe = false;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT NUMERO_SECTOR FROM DATAFRUT_SECTORES WHERE NUMERO_SECTOR = " + numero + ";";
            int cantidadResultados = Convert.ToInt32(cmd.ExecuteScalar());
            if (cantidadResultados > 0)
            {
                existe = true;
                if (checkBoxExistente.Checked)
                {
                    existe = false;
                }
            }
            else
            {
                existe = false;
                if (checkBoxExistente.Checked)
                {
                    existe = true;
                }
            }
            Conexion.cerrarConexion();
            return !existe;
        }

        public int traerCodigoSectorPorNumero(int numero)
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT COD_SECTOR FROM DATAFRUT_SECTORES WHERE NUMERO_SECTOR = " + numero + ";";
            int codigoSector = Convert.ToInt32(cmd.ExecuteScalar());
            return codigoSector;
        }

        public bool ingresarSector(string numeroSector)
        {
            bool exito = false;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "INSERT INTO DATAFRUT_SECTORES (NUMERO_SECTOR) VALUES (" + numeroSector + ");";
            try
            {
                cmd.ExecuteNonQuery();
                exito = true;
            }
            catch
            {
                exito = false;
            }
            Conexion.cerrarConexion();
            return exito;
        }



        public bool ingresarPoligonoSector(List<Vertice> vertices, string numeroSector)
        {
            bool exito = false;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            int codigoSector = traerCodigoSectorPorNumero(Convert.ToInt32(numeroSector));
            try
            {
                foreach (Vertice ver in vertices)
                {
                    cmd.CommandText = "INSERT INTO DATAFRUT_POLIGONOS_SECTOR VALUES ("
                        + ver.numeroVertice + "," + codigoSector + ","
                        + ver.latitud.ToString().Replace(',', '.') + ","
                        + ver.longitud.ToString().Replace(',', '.') + ");";
                    cmd.ExecuteNonQuery();
                }
                exito = true;
            }
            catch
            {
                exito = false;
            }
            Conexion.cerrarConexion();
            return exito;
        }

        #endregion

        #region bloque
        private void cargarCmbBloques()
        {
            int contador = 0;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "select bl.NUMERO_BLOQUE from DATAFRUT_BLOQUES as bl left join DATAFRUT_POLIGONOS_BLOQUE as pbl ON bl.COD_BLOQUE = pbl.DATAFRUT_BLOQUES_COD_BLOQUE where pbl.NUMERO_VERTICE is null;";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt32(dr["NUMERO_BLOQUE"]) <= 0)
                {

                }
                else
                {
                    int numeroBloque = Convert.ToInt32(dr["NUMERO_BLOQUE"]);
                    cmbBloque.Items.Add(numeroBloque);
                }
                contador++;
            }
            Conexion.cerrarConexion();
            cmbBloque.SelectedIndex = 0;
        }
        private void cargarBloques()
        {
            List<List<Vertice>> bloquesTemporales = new List<List<Vertice>>();
            lisVer = new List<Vertice>();
            Vertice vertice;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT * FROM viewPoligonosBloques;";
            MySqlDataReader dr = cmd.ExecuteReader();
            vertice = new Vertice();
            vertice.points = new List<GMap.NET.PointLatLng>();
            int cod_bloque = 0;
            bool first = false;
            while (dr.Read())
            {
                vertice = new Vertice();
                vertice.points = new List<GMap.NET.PointLatLng>();
                vertice.codigoBloque = Convert.ToInt32(dr["COD_BLOQUE"]);
                if (cod_bloque != vertice.codigoBloque)
                {
                    lisVer = new List<Vertice>();
                    vertice.codigoBloque = Convert.ToInt32(dr["COD_BLOQUE"]);
                    cod_bloque = vertice.codigoBloque;
                    if (first)
                    {
                        bloquesTemporales[0] = lisVer;
                        first = false;
                    }
                    else
                    {
                        bloquesTemporales.Add(lisVer);
                    }
                }
                vertice.latitud = Convert.ToDouble(dr["LATITUD"].ToString().Replace('.', ','));
                vertice.longitud = Convert.ToDouble(dr["LONGITUD"].ToString().Replace('.', ','));
                vertice.numeroSector = Convert.ToInt32(dr["NUMERO_BLOQUE"]);
                vertice.numeroVertice = Convert.ToInt32(dr["NUMERO_VERTICE"]);
                vertice.points.Add(new GMap.NET.PointLatLng(vertice.latitud, vertice.longitud));
                lisVer.Add(vertice);
            }
            Conexion.cerrarConexion();

            foreach (List<Vertice> lisVer in bloquesTemporales)
            {
                int codBloqueTemporal = -100;
                List<GMap.NET.PointLatLng> pointsTemp = new List<PointLatLng>();
                foreach (Vertice ver in lisVer)
                {
                    pointsTemp.Add(new GMap.NET.PointLatLng(ver.latitud, ver.longitud));
                    codBloqueTemporal = ver.codigoBloque;
                }
                bloque = new GMapPolygon(pointsTemp, "bloque");
                bloque.tipo = "bloque";
                Conexion.abrirConexion();
                MySqlCommand cmdT = Conexion.con.CreateCommand();
                cmd.CommandText = "SELECT NUMERO_BLOQUE FROM DATAFRUT_BLOQUES WHERE COD_BLOQUE = " + codBloqueTemporal + ";";
                MySqlDataReader drT = cmd.ExecuteReader();
                while (drT.Read())
                {
                    bloque.codigo = Convert.ToInt32(drT["NUMERO_BLOQUE"]);
                }
                Conexion.cerrarConexion();
                bloque.IsHitTestVisible = true;
                bloque.Fill = new SolidBrush(Color.FromArgb(50, Color.Green));
                bloque.Stroke = new Pen(Color.Green, 1);
                polygonsBloques.Polygons.Add(bloque);
            }
        }
        public bool validarNumeroBloque(string numero)
        {
            bool existe = false;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT NUMERO_BLOQUE FROM DATAFRUT_BLOQUES WHERE NUMERO_BLOQUE = " + numero + ";";
            int cantidadResultados = Convert.ToInt32(cmd.ExecuteScalar());
            if (cantidadResultados > 0)
            {
                existe = true;
            }
            else
            {
                existe = false;
            }
            Conexion.cerrarConexion();
            return existe;
        }
        public int traerCodigoBloquePorNumero(int numero)
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT COD_BLOQUE FROM DATAFRUT_BLOQUES WHERE NUMERO_BLOQUE = " + numero + ";";
            int codigoBloque = Convert.ToInt32(cmd.ExecuteScalar());
            return codigoBloque;
        }
        public bool ingresarBloque(string numeroBloque, string codTSector)
        {
            bool exito = false;
            int codigoDeSector = traerCodigoSectorPorNumero(Convert.ToInt32(codTSector));
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "INSERT INTO DATAFRUT_BLOQUES (NUMERO_BLOQUE, DATAFRUT_SECTORES_COD_SECTOR) VALUES (" + numeroBloque + "," + codigoDeSector + ");";
            try
            {
                cmd.ExecuteNonQuery();
                exito = true;
            }
            catch
            {
                exito = false;
            }
            Conexion.cerrarConexion();
            return exito;
        }
        public bool ingresarPoligonoBloque(List<Vertice> vertices, string numeroBloque)
        {
            bool exito = false;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(numeroBloque));
            try
            {
                foreach (Vertice ver in vertices)
                {
                    cmd.CommandText = "INSERT INTO DATAFRUT_POLIGONOS_BLOQUE VALUES ("
                        + ver.numeroVertice + "," + codigoBloque + ","
                        + ver.latitud.ToString().Replace(',', '.') + ","
                        + ver.longitud.ToString().Replace(',', '.') + ");";
                    cmd.ExecuteNonQuery();
                }
                exito = true;
            }
            catch
            {
                exito = false;
            }
            Conexion.cerrarConexion();
            return exito;
        }

        #endregion

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (btnDibujar.Text == "Dibujar")
            {
                btnDibujar.Text = "Terminar";
                grpBoxHileras.Enabled = false;
                dgvVertices.Enabled = true;
                btnCrearPoligono.Enabled = true;
                grpBoxDibujar.Enabled = true;
            }
            else if (btnDibujar.Text == "Dibujar" && radioBloques.Checked)
            {
                btnDibujar.Text = "Terminar";
                dgvVertices.Enabled = true;
                btnCrearPoligono.Enabled = true;
                grpBoxDibujar.Enabled = true;
                cmbSector.Enabled = true;
                cmbBloque.Enabled = true;
            }
            else
            {
                btnDibujar.Text = "Dibujar";
                dgvVertices.Enabled = false;
                btnCrearPoligono.Enabled = false;
                grpBoxDibujar.Enabled = true;
                cmbSector.Enabled = false;
                cmbBloque.Enabled = false;
                grpBoxHileras.Enabled = true;
            }
        }

        private void checkBoxBloquear_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBloquear.Checked)
            {
                gmap.CanDragMap = false;
            }
            else
            {
                gmap.CanDragMap = true;

            }
        }

        private void checkBoxCentro_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCentro.Checked)
            {
                gmap.ShowCenter = false;
            }
            else
            {
                gmap.ShowCenter = true;

            }
        }

        private void actualizarOverlay()
        {
            polygons = new GMap.NET.WindowsForms.GMapOverlay("polygons");
            polygons.Polygons.Clear();
            polygonsBloques.Polygons.Clear();
            cargarSectores();
            cargarBloques();
        }

        private void btnCerrarEditorMapa_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        string tipoPol = "sector";
        private void radioBloques_CheckedChanged(object sender, EventArgs e)
        {
            cmbSector.Enabled = true;
            lbltipoPoligono.Text = "Número de Bloque";
            txtNroPoligono.Text = cmbBloque.SelectedItem.ToString();
            checkBoxExistente.Enabled = true;
            cBoxBloque.Checked = true;
            cBoxSector.Checked = false;
            lblSeleccion.Text = "Bloque seleccionado:";
            tipoPol = "bloque";
        }

        private void radioSectores_CheckedChanged(object sender, EventArgs e)
        {
            cmbSector.Enabled = false;
            cmbBloque.Enabled = false;
            txtNroPoligono.Text = "";
            lbltipoPoligono.Text = "Número de Sector";
            checkBoxExistente.Enabled = false;
            cBoxBloque.Checked = false;
            cBoxSector.Checked = true;
            lblSeleccion.Text = "Sector seleccionado:";
            tipoPol = "sector";
        }


        private void cmbBloque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBloque.Enabled)
                txtNroPoligono.Text = cmbBloque.SelectedItem.ToString();
        }

        private void checkBoxExistente_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExistente.Checked)
                cmbBloque.Enabled = true;
            else
                cmbBloque.Enabled = false;
        }

        private void cBoxBloque_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxBloque.Checked)
            {
                polygonsBloques.IsVisibile = true;
            }
            else
            {
                polygonsBloques.IsVisibile = false;
            }
        }

        private void cBoxSector_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxSector.Checked)
            {
                polygons.IsVisibile = true;
            }
            else
            {
                polygons.IsVisibile = false;
            }
        }

        private void dgvVertices_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = dgvVertices.HitTest(e.X, e.Y);
                // If column is first column
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 0)
                {
                    rightClickSelectRow(hitTestInfo);
                    cMenuStripDGV.Show(dgvVertices, new Point(e.X, e.Y));
                }
                // If column is second column
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 1)
                {
                    rightClickSelectRow(hitTestInfo);
                    cMenuStripDGV.Show(dgvVertices, new Point(e.X, e.Y));
                }

                if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 2)
                {
                    rightClickSelectRow(hitTestInfo);
                    cMenuStripDGV.Show(dgvVertices, new Point(e.X, e.Y));
                }
            }
        }

        private void rightClickSelectRow(DataGridView.HitTestInfo hitTestInfo)
        {
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                if (row.Index == hitTestInfo.RowIndex)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }
            //dgvVertices.Rows[hitTestInfo.RowIndex].Selected = true;
        }

        private void moverArribaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int fIndex = 0;
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                int rIndex = row.Index;
                DataGridViewRow tRow = row;
                if (row.Selected == true && rIndex > 0)
                {
                    fIndex = rIndex;
                    //el vertice = indice + 1
                    dgvVertices.Rows.Remove(row);
                    tRow.Cells[0].Value = rIndex;
                    dgvVertices.Rows[rIndex - 1].Cells[0].Value = rIndex + 1;
                    dgvVertices.Rows.Insert(rIndex - 1, tRow);
                    break;
                }
            }
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                if (row.Index == fIndex - 1)
                    row.Selected = true;
                else
                    row.Selected = false;
            }
            if (fIndex > 0)
                dgvVertices.Rows[fIndex - 1].Selected = true;
            actualizarMarkers();
        }

        private void actualizarMarkers()
        {
            markers.Markers.Clear();
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                String labelNroVertice = row.Cells[0].Value.ToString();
                double lat = Convert.ToDouble(row.Cells[2].Value);
                double lng = Convert.ToDouble(row.Cells[1].Value);
                GMapLabeledMarker marker = new GMapLabeledMarker(new PointLatLng(lat, lng), labelNroVertice, GMarkerGoogleType.blue_small);
                markers.Markers.Add(marker);
            }
        }

        private void moverAbajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int fIndex = dgvVertices.Rows.Count - 1;
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                int rIndex = row.Index;
                DataGridViewRow tRow = row;
                if (row.Selected == true && rIndex < dgvVertices.Rows.Count - 1)
                {
                    fIndex = rIndex;
                    dgvVertices.Rows.Remove(row);
                    dgvVertices.Rows[rIndex].Cells[0].Value = rIndex + 1;
                    tRow.Cells[0].Value = rIndex + 2;
                    dgvVertices.Rows.Insert(rIndex + 1, tRow);
                    //dgvVertices.Rows.Add(tRow);
                    break;
                }
            }
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                if (row.Index == fIndex + 1)
                    row.Selected = true;
                else
                    row.Selected = false;
            }
            if (fIndex < dgvVertices.Rows.Count - 1)
                dgvVertices.Rows[fIndex + 1].Selected = true;
            actualizarMarkers();
        }

        private void eliminarVerticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int contador = 0;
            foreach (DataGridViewRow row in dgvVertices.Rows)
            {
                int rIndex = row.Index;
                if (row.Selected == true && rIndex < dgvVertices.Rows.Count)
                {
                    contador = row.Index;
                    dgvVertices.Rows.Remove(row);
                    if (rIndex < dgvVertices.Rows.Count - 1)
                        dgvVertices.Rows[rIndex].Cells[0].Value = rIndex + 1;
                    break;
                }
            }
            for (int i = contador + 1; i < dgvVertices.Rows.Count; i++)
            {
                dgvVertices.Rows[i].Cells[0].Value = i + 1;
            }
            actualizarMarkers();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tipoPol == "bloque")
            {
                int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblPolSeleccionado.Text));
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "DELETE FROM DATAFRUT_POLIGONOS_BLOQUE WHERE DATAFRUT_BLOQUES_COD_BLOQUE = " + codigoBloque + ";";
                DialogResult dialogResult = MessageBox.Show("¿Esta seguro de eliminar este bloque?\nEsta accion solo eliminara el dibujo y no la informacion relacionada.", "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                }
                Conexion.cerrarConexion();
                grpBoxHileras.Enabled = false;
            }
            else if (tipoPol == "sector")
            {
                int codigoSector = traerCodigoSectorPorNumero(Convert.ToInt32(lblPolSeleccionado.Text));
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "DELETE FROM DATAFRUT_POLIGONOS_SECTOR WHERE DATAFRUT_SECTORES_COD_SECTOR = " + codigoSector + ";";
                MySqlDataReader dr = cmd.ExecuteReader();
                Conexion.cerrarConexion();
            }
            btnEliminar.Enabled = false;
            actualizarOverlay();
        }


        private void cmbHileras_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoHilera = traerCodigoHilera();
            Conexion.abrirConexion();
            String query = "SELECT LARGO, SUPERFICIE FROM DATAFRUT_HILERAS WHERE COD_HILERA = " + codigoHilera + ";";
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = query;
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtLargoHilera.Text = dr[0].ToString();
                txtSupHa.Text = dr[1].ToString();
            }
            Conexion.cerrarConexion();
        }

        private void traerFechasPorHilera()
        {
            if(cmbHilerasPlantacion.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una hilera");
            }
            else
            {
                //llamar a todas las fechas de plantacion de la hilera escogida
                ArrayList fechas = traerFechasHileras();
                Conexion.abrirConexion();
                String query = "SELECT FECHA, COD_PLANTACION FROM DATAFRUT_PLANTACIONES WHERE DATAFRUT_HILERAS_COD_HILERA = " + fechas[cmbHilerasPlantacion.SelectedIndex];
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = query;
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] fechaO = dr[0].ToString().Split(' ');
                    string[] fechaT = fechaO[0].Split('-');
                    string fechaF = fechaT[2] + '-' + fechaT[1] + '-' + fechaT[0];
                    cmbFechaPlantacion.Items.Add(new Item(fechaF, Convert.ToInt32(dr[1])));
                }
                Conexion.cerrarConexion();
            }
            
        }

        private ArrayList traerFechasHileras()
        {
            Item it = (Item)(cmbHilerasPlantacion.SelectedItem);
            ArrayList fechas = new ArrayList();
            cmbFechaPlantacion.Items.Clear();
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "select COD_HILERA from DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE =" + it.Value + ";";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                fechas.Add(dr[0]);
            }
            Conexion.cerrarConexion();
            return fechas;
        }



        private void cmbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarPlantacion_Click(object sender, EventArgs e)
        {
            if (btnAgregarPlantacion.Text == "Agregar")
            {
                if (cmbHilerasPlantacion.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una hilera.");
                }
                else
                {
                    Regex regex = new Regex("^([1-9][0-9]+|[1-9])$");
                    Match match = regex.Match(txtHileras.Text);
                    match = regex.Match(txtPlantasEnterradas.Text);
                    if (match.Success)
                    {
                        regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                        match = regex.Match(txtKgRaiz.Text);
                        if (match.Success)
                        {
                            regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                            match = regex.Match(txtFactor.Text);
                            if (match.Success)
                            {
                                //todo validado correcto
                                if (radioNuevaPlantacion.Checked)
                                {
                                    int codigoHilera = traerCodigoHileraPlantacion();
                                    agregarPlantacion(codigoHilera);
                                    MessageBox.Show("Plantacion agregada correctamente.");
                                    limpiarCamposHilera();
                                }
                            }
                            else
                            {
                                MessageBox.Show("En el campo factor solo pueden ser numeros mayores a 0.0.");
                                txtFactor.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("En el campo Kg. de raiz solo pueden ser numeros mayores a 0.0.");
                            txtKgRaiz.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("En el campo Nro. plantas enterradas solo pueden ser numeros mayores a 0.");
                        txtPlantasEnterradas.Focus();
                    }
                }
            }
            else if(btnAgregarPlantacion.Text == "Modificar")
            {
                if (cmbHilerasPlantacion.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una hilera.");
                }
                else
                {
                    Regex regex = new Regex("^([1-9][0-9]+|[1-9])$");
                    Match match = regex.Match(txtHileras.Text);
                    match = regex.Match(txtPlantasEnterradas.Text);
                    if (match.Success)
                    {
                        regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                        match = regex.Match(txtKgRaiz.Text);
                        if (match.Success)
                        {
                            regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                            match = regex.Match(txtFactor.Text);
                            if (match.Success)
                            {
                                //todo validado correcto
                                if (radioModificarPlantacion.Checked)
                                {
                                    Item it = (Item)cmbFechaPlantacion.SelectedItem;
                                    modificarPlantacion(it.Value);
                                    MessageBox.Show("Plantacion modificada correctamente.");
                                    limpiarCamposPlantacion();
                                }
                            }
                            else
                            {
                                MessageBox.Show("En el campo factor solo pueden ser numeros mayores a 0.0.");
                                txtFactor.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("En el campo Kg. de raiz solo pueden ser numeros mayores a 0.0.");
                            txtKgRaiz.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("En el campo Nro. plantas enterradas solo pueden ser numeros mayores a 0.");
                        txtPlantasEnterradas.Focus();
                    }
                }
            }
        }

        private void modificarPlantacion(int codigoHilera)
        {
            Item itemTipo = (Item)(cmbTipoPlanta.SelectedItem);
            Item itemVariedad = (Item)(cmbVariedad.SelectedItem);
            Item itemOrigen = (Item)(cmbOrigen.SelectedItem);
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            string factor = txtFactor.Text;
            string kgRaiz = txtKgRaiz.Text;
            cmd.CommandText = "update DATAFRUT_PLANTACIONES set VARIEDAD = " + itemVariedad.Value
                + ", TIPO_PLANTA = " + itemTipo.Value
                + ", ORIGEN = " + itemOrigen.Value
                + ", PLANTAS_ENTERRADAS = " + txtPlantasEnterradas.Text
                + ", KG_RAIZ = " + kgRaiz.Replace(',', '.')
                + ", FACTOR = " + factor.Replace(',', '.')
                + " where COD_PLANTACION = " + codigoHilera + ";";
            cmd.ExecuteNonQuery();
            Conexion.cerrarConexion();
        }

        private void btnEliminarPlantacion_Click(object sender, EventArgs e)
        {
            Item it = (Item)cmbFechaPlantacion.SelectedItem;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "DELETE FROM DATAFRUT_PLANTACIONES WHERE COD_PLANTACION = " + it.Value + ";";
            DialogResult dialogResult = MessageBox.Show("¿Desea eliminar esta plantacion?", "Confirmación", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                limpiarCamposPlantacion();
                MessageBox.Show("Plantacion eliminada.");
            }
            Conexion.cerrarConexion();

        }


        private void btnAgregarHilera_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHileras.Text))
            {
                Regex regex = new Regex("^([1-9][0-9]+|[1-9])$");
                Match match = regex.Match(txtHileras.Text);
                match = regex.Match(txtPlantasEnterradas.Text);
                if (match.Success)
                {
                    regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                    match = regex.Match(txtKgRaiz.Text);
                    if (match.Success)
                    {
                        regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                        match = regex.Match(txtFactor.Text);
                        if (match.Success)
                        {
                            //todo validado correcto
                            if (!validarHileraExiste() && radioModificarPlantacion.Checked)
                            {
                                agregarHilera();
                                codigoUltimaHilera = buscarUltimaHilera();
                                //agregarPlantacion();
                                MessageBox.Show("Hilera agregada correctamente");
                                limpiarCamposHilera();
                            }
                            else if (validarHileraExiste() && radioNuevaPlantacion.Checked)
                            {
                                codigoUltimaHilera = traerCodigoHilera();
                                //agregarPlantacion();
                                MessageBox.Show("Hilera agregada correctamente");
                                limpiarCamposHilera();
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show("El numero de hilera ya existe, ¿Desea agregar los datos \nde la plantacion?", "Confirmación", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    codigoUltimaHilera = buscarUltimaHilera();
                                    //agregarPlantacion();
                                    MessageBox.Show("Hilera agregada correctamente");
                                    limpiarCamposHilera();
                                }

                                txtHileras.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("En el campo factor solo pueden ser numeros mayores a 0.0");
                            txtFactor.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("En el campo Kg. de raiz solo pueden ser numeros mayores a 0.0");
                        txtKgRaiz.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("En el campo Nro. plantas enterradas solo pueden ser numeros mayores a 0");
                    txtPlantasEnterradas.Focus();
                }
            }
        }

        private void limpiarCamposHilera()
        {
            txtHileras.Text = "";
            txtLargoHilera.Text = "";
            txtSupHa.Text = "";
        }

        private void limpiarCamposPlantacion()
        {
            cmbHilerasPlantacion.SelectedIndex = -1;
            txtPlantasEnterradas.Text = "";
            txtKgRaiz.Text = "";
            txtFactor.Text = "";
            cmbTipoPlanta.SelectedIndex = 0;
            cmbVariedad.SelectedIndex = 0;
            cmbOrigen.SelectedIndex = 0;
        }

        private void agregarHilera()
        {
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            //agregar hilera
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "INSERT INTO DATAFRUT_HILERAS VALUES (DEFAULT, "
                + codigoBloque + ", "
                + txtHileras.Text + ", "
                + txtLargoHilera.Text + ", "
                + txtSupHa.Text + ");";
            cmd.ExecuteNonQuery();
            Conexion.cerrarConexion();
        }

        private void agregarPlantacion(int codigoHilera)
        {
            //agregar plantacion
            Item itemTipo = (Item)(cmbTipoPlanta.SelectedItem);
            Item itemVariedad = (Item)(cmbVariedad.SelectedItem);
            Item itemOrigen = (Item)(cmbOrigen.SelectedItem);
            Conexion.abrirConexion();
            MySqlCommand cmd2 = Conexion.con.CreateCommand();
            cmd2.CommandText = "INSERT INTO DATAFRUT_PLANTACIONES VALUES (DEFAULT, "
                + codigoHilera + ", "
                + itemTipo.Value + ", '"
                + dtpFechaPlantacion.Value.ToString("yyyy-MM-dd") + "', "
                + itemVariedad.Value + ", "
                + itemOrigen.Value + ", "
                + txtPlantasEnterradas.Text + ", "
                + txtKgRaiz.Text + ", "
                + txtFactor.Text + ");";
            cmd2.ExecuteNonQuery();
            Conexion.cerrarConexion();
        }

        private bool validarHileraExiste()
        {
            bool existe = true;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            Conexion.abrirConexion();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string queryOrigenes = "select NUMERO_HILERA FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = "
                + codigoBloque + " and NUMERO_HILERA = " + txtHileras.Text + ";";
            MyDA.SelectCommand = new MySqlCommand(queryOrigenes, Conexion.con);
            DataTable tablaOrigenes = new DataTable();
            MyDA.Fill(tablaOrigenes);
            //¿HAY DATOS?
            if (tablaOrigenes.Rows.Count > 0)
            {
                for (int i = 0; i < tablaOrigenes.Rows.Count; i++)
                {
                    if (Convert.ToInt32(txtHileras.Text) == Convert.ToInt32(tablaOrigenes.Rows[i][0]))
                    {
                        existe = true;
                    }
                    else
                    {
                        existe = false;
                    }
                }
                return existe;
            }
            else if (tablaOrigenes.Rows.Count == 0)
            {
                existe = false;
            }
            Conexion.cerrarConexion();
            return existe;
        }

        private int traerCodigoHilera()
        {
            int codHilera = 0;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            Item it = (Item)cmbHileras.SelectedItem;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "select COD_HILERA FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = "
                + codigoBloque + " and NUMERO_HILERA = " + it.Name + ";";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                codHilera = Convert.ToInt32(dr[0]);
            }
            Conexion.cerrarConexion();
            return codHilera;
        }

        private int traerCodigoHileraPlantacion()
        {
            int codHilera = 0;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            Item it = (Item)cmbHilerasPlantacion.SelectedItem;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "select COD_HILERA FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = "
                + codigoBloque + " and NUMERO_HILERA = " + it.Name + ";";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                codHilera = Convert.ToInt32(dr[0]);
            }

            Conexion.cerrarConexion();
            return codHilera;
        }

        private int buscarUltimaHilera()
        {
            int ultimaHilera = 0;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "select COD_HILERA FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = "
                + codigoBloque + " and NUMERO_HILERA = " + txtHileras.Text + ";";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ultimaHilera = Convert.ToInt32(dr[0]);
            }
            Conexion.cerrarConexion();
            return ultimaHilera;
        }

        private void cargarVariedades()
        {
            cmbVariedad.Items.Clear();
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DATAFRUT_VARIEDADES;";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbVariedad.Items.Add(new Item(dr[1].ToString(), Convert.ToInt32(dr[0])));
            }
            Conexion.cerrarConexion();
            cmbVariedad.SelectedIndex = 0;
        }

        private void cargarOrigen()
        {
            cmbOrigen.Items.Clear();
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DATAFRUT_ORIGEN;";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbOrigen.Items.Add(new Item(dr[1].ToString(), Convert.ToInt32(dr[0])));
            }
            Conexion.cerrarConexion();
            cmbOrigen.SelectedIndex = 0;
        }

        private void cargarTipo()
        {
            cmbTipoPlanta.Items.Clear();
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DATAFRUT_TIPOS;";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbTipoPlanta.Items.Add(new Item(dr[1].ToString(), Convert.ToInt32(dr[0])));
            }
            Conexion.cerrarConexion();
            cmbTipoPlanta.SelectedIndex = 0;
        }

        private void radioNuevaPlantacion_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaPlantacion.Visible = true;
            cmbFechaPlantacion.Visible = false;
            btnAgregarPlantacion.Text = "Agregar";
            btnEliminarPlantacion.Enabled = false;
            /*
            cmbHileras.Visible = true;
            cmbFechaPlantacion.Visible = true;
            txtHileras.Visible = false;
            dtpFechaPlantacion.Visible = false;
            btnAgregarHilera.Enabled = false;
            btnActualizarHilera.Enabled = true;
            btnEliminarHilera.Enabled = true;
            */
        }
        
        private void radioModificarPlantacion_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaPlantacion.Visible = false;
            cmbFechaPlantacion.Visible = true;
            btnAgregarPlantacion.Text = "Modificar";
            btnEliminarPlantacion.Enabled = true;
            /*
            cmbHileras.Visible = false;
            cmbFechaPlantacion.Visible = false;
            txtHileras.Visible = true;
            dtpFechaPlantacion.Visible = true;
            btnAgregarHilera.Enabled = true;
            btnActualizarHilera.Enabled = false;
            btnEliminarHilera.Enabled = false;
            */
        }

        private void radioCrearHilera_CheckedChanged(object sender, EventArgs e)
        {
            cmbHileras.Visible = false;
            txtHileras.Visible = true;
            btnAgrHilera.Text = "Agregar";
            btnEliHilera.Enabled = false;
        }

        private void radioModificarHilera_CheckedChanged(object sender, EventArgs e)
        {
            cmbHileras.Visible = true;
            txtHileras.Visible = false;
            btnAgrHilera.Text = "Modificar";
            btnEliHilera.Enabled = true;
        }

        private void btnAgrHilera_Click(object sender, EventArgs e)
        {
            if (btnAgrHilera.Text == "Agregar")
            {
                if (!string.IsNullOrEmpty(txtHileras.Text))
                {
                    Regex regex = new Regex("^([1-9][0-9]+|[1-9])$");
                    Match match = regex.Match(txtHileras.Text);
                    if (match.Success)
                    {
                        regex = new Regex("^([1-9][0-9]+|[1-9])$");
                        match = regex.Match(txtLargoHilera.Text);
                        if (match.Success)
                        {
                            regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                            match = regex.Match(txtSupHa.Text);
                            if (match.Success)
                            {
                                if (!validarHileraExiste() && radioCrearHilera.Checked)
                                {
                                    agregarHilera();
                                    MessageBox.Show("Hilera agregada correctamente.");
                                    limpiarCamposHilera();
                                }
                                else
                                {
                                    MessageBox.Show("El numero de hilera ya esta utilizado, por favor seleccionar otro.");
                                }
                            }

                            else
                            {
                                MessageBox.Show("En el campo Superficia Ha. solo pueden ser numeros mayores a 0.0.");
                                txtSupHa.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("En el campo largo de hilera solo pueden ser numeros mayores a 0.");
                            txtLargoHilera.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("En el campo numero de hilera solo pueden ser numeros mayores a 0.");
                        txtHileras.Focus();
                    }
                }
            }

            else if (btnAgrHilera.Text == "Modificar")
            {
                if (cmbHileras.SelectedIndex >= 0 && cmbFechaPlantacion.SelectedIndex >= 0)
                {
                    Item it = (Item)cmbHileras.SelectedItem;
                    Regex regex = new Regex("^([1-9][0-9]+|[1-9])$");
                    Match match = regex.Match(it.Name);
                    if (match.Success)
                    {
                        regex = new Regex("^([1-9][0-9]+|[1-9])$");
                        match = regex.Match(txtLargoHilera.Text);
                        if (match.Success)
                        {
                            regex = new Regex("^[0-9]+([,.][0-9]+)?$");
                            match = regex.Match(txtSupHa.Text);
                            if (match.Success)
                            {
                                if (radioModificarHilera.Checked)
                                {
                                    int codigoHilera = traerCodigoHilera();
                                    modificarHilera(codigoHilera);
                                    MessageBox.Show("Hilera modificada correctamente.");
                                    limpiarCamposHilera();
                                }
                                else
                                {
                                    MessageBox.Show("El numero de hilera ya esta utilizado, por favor seleccionar otro.");
                                }
                            }

                            else
                            {
                                MessageBox.Show("En el campo Superficia Ha. solo pueden ser numeros mayores a 0.0.");
                                txtSupHa.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("En el campo largo de hilera solo pueden ser numeros mayores a 0.");
                            txtLargoHilera.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("En el campo numero de hilera solo pueden ser numeros mayores a 0.");
                        txtHileras.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una hilera y una fecha.");
                }
            }
        }
        

        private void modificarHilera(int codigoHilera)
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "update DATAFRUT_HILERAS set LARGO = " + txtLargoHilera.Text
                + ", SUPERFICIE = " + txtSupHa.Text 
                + " where COD_HILERA = " + codigoHilera + ";";
            cmd.ExecuteNonQuery();
            Conexion.cerrarConexion();
        }

        private void btnEliHilera_Click(object sender, EventArgs e)
        {
            if(cmbHileras.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una hilera.");
            }
            else
            {
                //agregar funcion para revisar que no la hilera no tenga plantaciones
                //si tiene plantaciones, no se puede borrar la hilera hasta eliminar todas las plantaciones
                //manualmente una por una

                int codigoHilera = traerCodigoHilera();
                if (revisarHileraTienePlantacion(codigoHilera))
                {
                    MessageBox.Show("No se puede eliminar una hilera con plantaciones, por favor elimine las plantaciones primero.");
                }
                else
                {
                    eliminarHilera(codigoHilera);
                    Item it = (Item)cmbHileras.SelectedItem;
                    cmbHileras.Items.Remove(it);
                    MessageBox.Show("Hilera eliminada correctamente.");
                    limpiarCamposHilera();
                }
            }
        }

        private void eliminarHilera(int codigoHilera)
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "DELETE FROM DATAFRUT_HILERAS WHERE COD_HILERA = " + codigoHilera + ";";
            cmd.ExecuteNonQuery();
            Conexion.cerrarConexion();
        }

        private bool revisarHileraTienePlantacion(int codigoHilera)
        {
            bool tiene = true;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            Conexion.abrirConexion();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string queryOrigenes = "SELECT * FROM datafrut.DATAFRUT_PLANTACIONES where DATAFRUT_HILERAS_COD_HILERA = "
                + codigoHilera + ";";
            MyDA.SelectCommand = new MySqlCommand(queryOrigenes, Conexion.con);
            DataTable tablaOrigenes = new DataTable();
            MyDA.Fill(tablaOrigenes);
            //¿HAY DATOS?
            if (tablaOrigenes.Rows.Count > 0)
            {
                tiene = true;
            }
            else if (tablaOrigenes.Rows.Count == 0)
            {
                tiene = false;
            }
            Conexion.cerrarConexion();
            return tiene;
        }

        private void cmbHilerasPlantacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radioModificarPlantacion.Checked)
                traerFechasPorHilera();
        }

        private void cmbFechaPlantacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            traerPlantacionSegunFecha();
        }

        private void traerPlantacionSegunFecha()
        {
            Item it = (Item)cmbFechaPlantacion.SelectedItem;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            Conexion.abrirConexion();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string queryOrigenes = "select TIPO_PLANTA, VARIEDAD, ORIGEN, PLANTAS_ENTERRADAS, KG_RAIZ, FACTOR FROM DATAFRUT_PLANTACIONES where COD_PLANTACION = "
                + it.Value + ";";
            MyDA.SelectCommand = new MySqlCommand(queryOrigenes, Conexion.con);
            DataTable tablaOrigenes = new DataTable();
            MyDA.Fill(tablaOrigenes);
            //¿HAY DATOS?
            if (tablaOrigenes.Rows.Count > 0)
            {
                seleccionarCmbTipo(Convert.ToInt32(tablaOrigenes.Rows[0][0]));
                seleccionarCmbVariedad(Convert.ToInt32(tablaOrigenes.Rows[0][1]));
                seleccionarCmbOrigen(Convert.ToInt32(tablaOrigenes.Rows[0][2]));
                txtPlantasEnterradas.Text = tablaOrigenes.Rows[0][3].ToString();
                txtKgRaiz.Text = tablaOrigenes.Rows[0][4].ToString();
                txtFactor.Text = tablaOrigenes.Rows[0][5].ToString();
            }
            else if (tablaOrigenes.Rows.Count == 0)
            {
                MessageBox.Show("No se encontro la plantación seleccionada");
            }
            Conexion.cerrarConexion();
        }

        private void seleccionarCmbTipo(int codigoTipo)
        {
            for(int i = 0; i < cmbTipoPlanta.Items.Count; i++)
            {
                cmbTipoPlanta.SelectedIndex = i;
                Item it = (Item)cmbTipoPlanta.SelectedItem;
                if (codigoTipo == Convert.ToInt32(it.Value))
                {
                    i = cmbTipoPlanta.Items.Count;
                }
            }
            /*
            foreach(Item it in (Items)cmbTipoPlanta.SelectedItem)
            {
                if (codigoTipo == Convert.ToInt32(it.Name))
                {
                    
                }
            }*/
        }

        private void seleccionarCmbVariedad(int codigoVariedad)
        {
            for (int i = 0; i < cmbVariedad.Items.Count; i++)
            {
                cmbVariedad.SelectedIndex = i;
                Item it = (Item)cmbVariedad.SelectedItem;
                if (codigoVariedad == Convert.ToInt32(it.Value))
                {
                    i = cmbVariedad.Items.Count;
                }
            }
        }

        private void seleccionarCmbOrigen(int codigoOrigen)
        {
            for (int i = 0; i < cmbOrigen.Items.Count; i++)
            {
                cmbOrigen.SelectedIndex = i;
                Item it = (Item)cmbOrigen.SelectedItem;
                if (codigoOrigen == Convert.ToInt32(it.Value))
                {
                    i = cmbOrigen.Items.Count;
                }
            }
        }
    } 

}

