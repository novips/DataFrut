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
using System.Collections;

namespace DataFrut
{
    public partial class MainDataFrut : Form
    {
        #region login
        //private String tUsuario = "none";

        #endregion

        public MainDataFrut()
        {
            InitializeComponent();
            gmap.Overlays.Add(polygons);
            gmap.Overlays.Add(polygonsBloques);
            cargarDGVRH();
            cargarDGVbodega();
            Grafico.ChartAreas[0].AxisX.Title = "Fecha";
            Grafico.ChartAreas[0].AxisY.Title = "Altura";
            Grafico.ChartAreas[0].AxisX.Minimum = 1;
            Grafico.ChartAreas[0].AxisX.Maximum = 12;
            Grafico.ChartAreas[0].AxisY.Minimum = 0;
            Grafico.ChartAreas[0].AxisY.Maximum = 300;
            Grafico.Series[0].Points.Add();
            Grafico.Series[0].Points[0].IsEmpty = true;
            
        }

        #region Mapa
        //variables
        GMapOverlay polygons = new GMapOverlay("polygons");
        GMapOverlay polygonsBloques = new GMapOverlay("polygons");
        GMapOverlay markers = new GMapOverlay("markers");
        //List<GMap.NET.PointLatLng> points;
        List<Vertice> lisVer;
        GMapPolygon sector;
        GMapPolygon bloque;
        int codigoBloque = 0;

        //metodos
        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.Position = new GMap.NET.PointLatLng(-37.1992883, -72.3719246);
            gmap.Zoom = 16;
            cargarSectores();
            cargarBloques();
            iniciarCombosPlantaciones();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.polygons.Clear();
            EditorMapa eMapa = new EditorMapa();
            eMapa.FormClosing += new FormClosingEventHandler(this.EditorMapa_FormClosing);
            eMapa.ShowDialog();
        }

        private void EditorMapa_FormClosing(object sender, FormClosingEventArgs e)
        {
            cargarSectores();
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
                List<GMap.NET.PointLatLng> pointsTemp = new List<PointLatLng>();
                int codigoTemp = -100;
                foreach (Vertice ver in lisVer)
                {
                    pointsTemp.Add(new GMap.NET.PointLatLng(ver.latitud, ver.longitud));
                    codigoTemp = ver.codigoSector;
                }
                sector = new GMapPolygon(pointsTemp, "sector");
                sector.codigo = codigoTemp;
                sector.tipo = "sector";
                sector.IsHitTestVisible = true;
                sector.Fill = new SolidBrush(Color.FromArgb(50, Color.Blue));
                sector.Stroke = new Pen(Color.Blue, 1);
                polygons.Polygons.Add(sector);
            }

        }




        private void gmap_OnPolygonClick(GMap.NET.WindowsForms.GMapPolygon item, MouseEventArgs e)
        {
            if (item.tipo == "bloque")
            {
                cmbHileras.Enabled = true;
                lblBloque.Text = item.codigo.ToString();
                cmbHileras.Items.Clear();
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                int codBloque = traerCodigoBloquePorNumero(Convert.ToInt32(item.codigo));
                codigoBloque = codBloque;
                cmd.CommandText = "SELECT * FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = " + codBloque + ";";
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbHileras.Items.Add(new Item(dr["NUMERO_HILERA"].ToString(), codBloque));
                }
                Conexion.cerrarConexion();
            }
        }

        private void cmbHileras_SelectedIndexChanged(object sender, EventArgs e)
        {
            //llamar a todas las fechas de plantacion de la hilera escogida
            ArrayList fechas = traerFechasHileras();
            Conexion.abrirConexion();
            String query = "SELECT FECHA FROM DATAFRUT_PLANTACIONES WHERE DATAFRUT_HILERAS_COD_HILERA = " + fechas[cmbHileras.SelectedIndex];
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = query;
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbFechaPlantacion.Items.Add(dr[0].ToString());
            }
            Conexion.cerrarConexion();

        }

        private ArrayList traerFechasHileras()
        {
            Item it = (Item)(cmbHileras.SelectedItem);
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

        private void mostrarInfoHilera()
        {
            Item item = (Item)cmbHileras.SelectedItem;
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DATAFRUT_HILERAS where DATAFRUT_BLOQUES_COD_BLOQUE = " + item.Value + " and NUMERO_HILERA = " + item.Name + ";";
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblBloque.Text = item.Value.ToString();
                lblLargo.Text = dr["LARGO"].ToString();
                lblSup.Text = dr["SUPERFICIE"].ToString();
            }
            Conexion.cerrarConexion();
        }

        private void cargarDatosHilera(int codigoHilera)
        {
            Conexion.abrirConexion();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string queryOrigenes = "select * from DATAFRUT_HILERAS where COD_HILERA = " + codigoHilera + ";";
            MyDA.SelectCommand = new MySqlCommand(queryOrigenes, Conexion.con);
            DataTable tablaOrigenes = new DataTable();
            MyDA.Fill(tablaOrigenes);

            //¿HAY DATOS?
            if (tablaOrigenes.Rows.Count > 0)
            {
                for (int indice = 0; indice < tablaOrigenes.Rows.Count; indice++)
                {
                    lblLargo.Text = tablaOrigenes.Rows[indice]["LARGO"].ToString();
                    lblSup.Text = tablaOrigenes.Rows[indice]["SUPERFICIE"].ToString();
                }
            }
            Conexion.cerrarConexion();
        }

        private void cargarDatosPlantacion(int codigoHilera)
        {

            String[] fecha = cmbFechaPlantacion.SelectedItem.ToString().Split(' ');
            String[] bienFecha = fecha[0].Split('-');
            String fechaFinal = bienFecha[2] + '-' + bienFecha[1] + '-' + bienFecha[0];
            Conexion.abrirConexion();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string queryOrigenes = "select * from DATAFRUT_PLANTACIONES where DATAFRUT_HILERAS_COD_HILERA = " + codigoHilera + " and FECHA = '" + fechaFinal + "';";
            MyDA.SelectCommand = new MySqlCommand(queryOrigenes, Conexion.con);
            DataTable tablaOrigenes = new DataTable();
            MyDA.Fill(tablaOrigenes);

            //¿HAY DATOS?
            if (tablaOrigenes.Rows.Count > 0)
            {
                for (int indice = 0; indice < tablaOrigenes.Rows.Count; indice++)
                {
                    lblTipo.Text = tablaOrigenes.Rows[indice]["TIPO_PLANTA"].ToString();
                    lblVariedad.Text = tablaOrigenes.Rows[indice]["VARIEDAD"].ToString();
                    lblOrigen.Text = tablaOrigenes.Rows[indice]["ORIGEN"].ToString();
                    lblEnterradas.Text = tablaOrigenes.Rows[indice]["PLANTAS_ENTERRADAS"].ToString();
                    lblFactor.Text = tablaOrigenes.Rows[indice]["FACTOR"].ToString();
                }
            }
            Conexion.cerrarConexion();
        }

        private void cmbFechaPlantacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codigoHilera = traerCodigoHilera();
            cargarDatosHilera(codigoHilera);
            cargarDatosPlantacion(codigoHilera);
        }

        private int traerCodigoHilera()
        {
            int codHilera = 0;
            Item it = (Item)cmbHileras.SelectedItem;
            int codigoBloque = traerCodigoBloquePorNumero(Convert.ToInt32(lblBloque.Text));
            String[] fecha = cmbFechaPlantacion.SelectedText.Split(' ');
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

        public int traerCodigoBloquePorNumero(int numero)
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "SELECT COD_BLOQUE FROM DATAFRUT_BLOQUES WHERE NUMERO_BLOQUE = " + numero + ";";
            int codigoBloque = Convert.ToInt32(cmd.ExecuteScalar());
            return codigoBloque;
        }

        public void actualizarOVerlay()
        {
            polygons = new GMapOverlay("polygons");
            cargarSectores();
        }

        private void cBoxBloque_CheckedChanged(object sender, EventArgs e)
        {
            if(cBoxBloque.Checked)
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

        private void cargarPlantacionesInicio()
        {
            Conexion.abrirConexion();


            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT HILERAS.NUMERO_HILERA AS 'HILERA', BLOQUES.NUMERO_BLOQUE AS 'BLOQUE', ORIGENES.NOMBRE AS 'ORIGEN', TIPOS.NOMBRE AS 'TIPO PLANTA', VARIEDADES.NOMBRE AS 'VARIEDAD DE PLANTA', YEAR(PLANTACIONES.FECHA) AS 'AÑO PLANTACION', PLANTACIONES.PLANTAS_ENTERRADAS AS 'CANTIDAD PLANTAS' "
            + "FROM DATAFRUT_PLANTACIONES AS PLANTACIONES "
            + "LEFT JOIN DATAFRUT_HILERAS AS HILERAS "
            + "ON PLANTACIONES.DATAFRUT_HILERAS_COD_HILERA = HILERAS.COD_HILERA "
            + "LEFT JOIN DATAFRUT_BLOQUES AS BLOQUES "
            + "ON HILERAS.DATAFRUT_BLOQUES_COD_BLOQUE = BLOQUES.NUMERO_BLOQUE "
            + "LEFT JOIN DATAFRUT_ORIGEN AS ORIGENES "
            + "ON PLANTACIONES.ORIGEN = ORIGENES.CODIGO "
            + "LEFT JOIN DATAFRUT_TIPOS AS TIPOS "
            + "ON PLANTACIONES.TIPO_PLANTA = TIPOS.CODIGO "
            + "LEFT JOIN DATAFRUT_VARIEDADES AS VARIEDADES "
            + "ON PLANTACIONES.VARIEDAD = VARIEDADES.CODIGO;";

            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, Conexion.con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dgvPlantaciones.DataSource = bSource;
            Conexion.cerrarConexion();
            dgvPlantaciones.Columns[5].HeaderText = "Año Plantacion";
        }

        private void iniciarCombosPlantaciones()
        {
            //Preparar los CBO
            cboOrigen.Items.Add("Seleccione");
            cboOrigen.SelectedItem = "Seleccione";
            cboOrigen.Items.Add("Todos");
            cboEdad.Enabled = false;

            //POBLAR CBO ORIGEN
            Conexion.abrirConexion();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string queryOrigenes = "select * from DATAFRUT_ORIGEN";
            MyDA.SelectCommand = new MySqlCommand(queryOrigenes, Conexion.con);
            DataTable tablaOrigenes = new DataTable();
            MyDA.Fill(tablaOrigenes);

            //¿HAY DATOS?
            if (tablaOrigenes.Rows.Count > 0)
            {
                for (int indice = 0; indice < tablaOrigenes.Rows.Count; indice++)
                {
                    cboOrigen.Items.Add(tablaOrigenes.Rows[indice]["NOMBRE"].ToString());
                }
            }
            Conexion.cerrarConexion();
        }

        private void cboEdad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreOrigen = cboOrigen.SelectedItem.ToString();
            string anoPlanta = cboEdad.SelectedItem.ToString();

            Conexion.abrirConexion();

            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT HILERAS.NUMERO_HILERA AS 'Hilera', BLOQUES.NUMERO_BLOQUE AS 'Bloque', ORIGENES.NOMBRE AS 'Origen', TIPOS.NOMBRE AS 'Tipo', VARIEDADES.NOMBRE AS 'Variedad', YEAR(PLANTACIONES.FECHA) AS 'Año plantacion', PLANTACIONES.PLANTAS_ENTERRADAS AS 'Cantidad de plantas' "
            + "FROM DATAFRUT_PLANTACIONES AS PLANTACIONES "
            + "LEFT JOIN DATAFRUT_HILERAS AS HILERAS "
            + "ON PLANTACIONES.DATAFRUT_HILERAS_COD_HILERA = HILERAS.COD_HILERA "
            + "LEFT JOIN DATAFRUT_BLOQUES AS BLOQUES "
            + "ON HILERAS.DATAFRUT_BLOQUES_COD_BLOQUE = BLOQUES.NUMERO_BLOQUE "
            + "LEFT JOIN DATAFRUT_ORIGEN AS ORIGENES "
            + "ON PLANTACIONES.ORIGEN = ORIGENES.CODIGO "
            + "LEFT JOIN DATAFRUT_TIPOS AS TIPOS "
            + "ON PLANTACIONES.TIPO_PLANTA = TIPOS.CODIGO "
            + "LEFT JOIN DATAFRUT_VARIEDADES AS VARIEDADES "
            + "ON PLANTACIONES.VARIEDAD = VARIEDADES.CODIGO "
            + "WHERE  '" + nombreOrigen + "' = ORIGENES.NOMBRE AND '" + anoPlanta + "' = YEAR(PLANTACIONES.FECHA);";

            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, Conexion.con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dgvPlantaciones.DataSource = bSource;
            Conexion.cerrarConexion();

            dgvPlantaciones.Columns[5].HeaderText = "Año plantacion";
        }

        private void cboOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEdad.Items.Clear();
            cboEdad.Text = "";
            string nombreOrigen = cboOrigen.SelectedItem.ToString();

            if (!cboOrigen.SelectedItem.ToString().Equals("Seleccione") && !cboOrigen.SelectedItem.ToString().Equals("Todos"))
            {
                cboEdad.Enabled = false;
                //POBLAR CBO EDAD
                Conexion.abrirConexion();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string queryEdad = "SELECT DISTINCT(YEAR(fecha)) "
                + "FROM DATAFRUT_PLANTACIONES as plantaciones "
                + "LEFT JOIN DATAFRUT_ORIGEN as origenes "
                + "on plantaciones.ORIGEN = origenes.CODIGO "
                + "where '" + nombreOrigen + "' = origenes.NOMBRE";
                MyDA.SelectCommand = new MySqlCommand(queryEdad, Conexion.con);
                DataTable tablaEdades = new DataTable();
                MyDA.Fill(tablaEdades);

                //¿HAY DATOS?
                if (tablaEdades.Rows.Count > 0)
                {
                    for (int indice = 0; indice < tablaEdades.Rows.Count; indice++)
                    {
                        cboEdad.Items.Add(tablaEdades.Rows[indice][0].ToString());
                    }

                    cboEdad.Enabled = true;
                }
                else
                {
                    this.dgvPlantaciones.DataSource = null;
                    this.dgvPlantaciones.Rows.Clear();
                    MessageBox.Show("No Existen Datos Para la Ubicación Seleccionada");
                }
                Conexion.cerrarConexion();
            }
            else if (cboOrigen.SelectedItem.ToString().Equals("Todos"))
            {
                cargarPlantacionesInicio();
                cboEdad.Enabled = false;
            }
            else
            {
                cboEdad.Enabled = false;
            }
        }

        #endregion

        #region FormPrincipal
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainDataFrut_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Grafico
        private void btnCrear_Click(object sender, EventArgs e)
        {
            Grafico.ChartAreas[0].AxisX.Title = txtNombreX.Text;
            Grafico.ChartAreas[0].AxisY.Title = txtNombreY.Text;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Grafico.Series[0].Points.Clear();
            double temperatura = Convert.ToDouble(txtTemperatura.Text);
            double humedad = Convert.ToDouble(txtHumedad.Text);
            double tensiometro = Convert.ToDouble(txtTensiometro.Text);
            double factor = calcularFactor(temperatura, humedad, tensiometro);
            //MessageBox.Show(factor.ToString());
            double alturaAnterior = 1;
            for (int i = 1; i < 13; i++)
            {
                double AlturaNueva = alturaAnterior + 14 * factor;
                Grafico.Series[0].Points.AddXY(i, AlturaNueva);
                alturaAnterior = AlturaNueva;
            }
        }

        private double calcularFactor(double temperatura, double humedad, double tenciometro)
        {
            double factor = temperatura * 0.45 + humedad * 0.35 + tenciometro * 0.20;
            if (factor > 1 && factor <= 100)
            {
                factor = 1 + (factor / 100);
            }
            return factor;
        }

        #endregion

        #region RRHH
        private String oldRut = "";
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtRut.Text != "")
            {
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "insert into DATAFRUT_EMPLEADOS values ('" + txtRut.Text + "','" + txtNombre.Text + "','" + txtApellido.Text + "','" + txtCargo.Text + "'," + txtSueldo.Text + ");";
                cmd.ExecuteNonQuery();
                Conexion.cerrarConexion();
                limpiarRH();
                cargarDGVRH();
            }
            else
            {
                MessageBox.Show("El campo Rut no puede estar vacio");
                txtRut.Focus();
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtRut.Text != "")
            {
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "update DATAFRUT_EMPLEADOS set RUT = '" + txtRut.Text
                    + "', NOMBRE ='" + txtNombre.Text
                    + "', APELLIDO ='" + txtApellido.Text
                    + "', CARGO='" + txtCargo.Text
                    + "', SUELDO =" + txtSueldo.Text
                    + " where RUT ='" + oldRut + "';";
                cmd.ExecuteNonQuery();
                Conexion.cerrarConexion();
                limpiarRH();
                cargarDGVRH();
            }
            else
            {
                MessageBox.Show("El campo Rut no puede estar vacio");
                txtRut.Focus();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtRut.Text != "")
            {
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "delete from DATAFRUT_EMPLEADOS where RUT ='" + txtRut.Text + "';";
                cmd.ExecuteNonQuery();
                Conexion.cerrarConexion();
                limpiarRH();
                cargarDGVRH();
            }
            else
            {
                MessageBox.Show("El campo Rut no puede estar vacio");
                txtRut.Focus();
            }
        }

        private void limpiarRH()
        {
            txtSueldo.Text = "";
            txtRut.Text = "";
            txtNombre.Text = "";
            txtCargo.Text = "";
            txtApellido.Text = "";
        }

        private void cargarDGVRH()
        {
            Conexion.abrirConexion();


            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from DATAFRUT_EMPLEADOS";
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, Conexion.con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dgvEmpleados.DataSource = bSource;
            Conexion.cerrarConexion();
        }
        private void dgvEmpleados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            oldRut = dgvEmpleados.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtRut.Text = dgvEmpleados.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNombre.Text = dgvEmpleados.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtApellido.Text = dgvEmpleados.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCargo.Text = dgvEmpleados.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSueldo.Text = dgvEmpleados.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        #endregion

        #region sensores
        private void btnEjecutarReporte_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Bodega
        private String oldCodigoDeBodega = "";
        private void btnAgregarBod_Click(object sender, EventArgs e)
        {
            if (txtCodBod.Text != "")
            {
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "INSERT INTO DATAFRUT_INVENTARIO VALUES ("
                    + txtCodBod.Text
                    + "," + txtCodBodSag.Text
                    + ",'" + txtProductoBod.Text
                    + "','" + txtMarcaBod.Text
                    + "','" + txtProcedenciaBod.Text
                    + "','" + txtUbicacionBod.Text + "');";
                cmd.ExecuteNonQuery();
                Conexion.cerrarConexion();
                limpiarControlBodega();
                cargarDGVbodega();
            }
            else
            {
                MessageBox.Show("El campo codigo no puede estar vacio");
                txtCodBod.Focus();
            }
        }

        private void btnEliminarBod_Click(object sender, EventArgs e)
        {
            if (txtCodBod.Text != "")
            {
                Conexion.abrirConexion();
                MySqlCommand cmd = Conexion.con.CreateCommand();
                cmd.CommandText = "delete from DATAFRUT_INVENTARIO where CODIGO ='" + txtCodBod.Text + "';";
                cmd.ExecuteNonQuery();
                Conexion.cerrarConexion();
                limpiarControlBodega();
                cargarDGVbodega();
            }
            else
            {
                MessageBox.Show("El campo codigo no puede estar vacio, por favor seleccione una celda");
            }
        }

        private void btnModificarBod_Click(object sender, EventArgs e)
        {
            Conexion.abrirConexion();
            MySqlCommand cmd = Conexion.con.CreateCommand();
            cmd.CommandText = "UPDATE DATAFRUT_INVENTARIO SET CODIGO ='" + txtCodBod.Text
                + "', CODIGO_SAG =" + txtCodBodSag.Text
                + ", PRODUCTO ='" + txtProductoBod.Text
                + "', MARCA ='" + txtMarcaBod.Text
                + "', PROCEDENCIA ='" + txtProcedenciaBod.Text
                + "', UBICACION ='" + txtUbicacionBod.Text
                + "' WHERE codigo ='" + oldCodigoDeBodega + "';";
            cmd.ExecuteNonQuery();
            Conexion.cerrarConexion();
            limpiarControlBodega();
            cargarDGVbodega();
        }

        private void cargarDGVbodega()
        {
            Conexion.abrirConexion();


            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "SELECT * from DATAFRUT_INVENTARIO";
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, Conexion.con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dgvStock.DataSource = bSource;
            Conexion.cerrarConexion();
        }

        private void limpiarControlBodega()
        {
            txtCodBod.Text = "";
            txtCodBodSag.Text = "";
            txtMarcaBod.Text = "";
            txtProcedenciaBod.Text = "";
            txtProductoBod.Text = "";
            txtUbicacionBod.Text = "";
        }

        private void dgvStock_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            oldCodigoDeBodega = dgvStock.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCodBod.Text = dgvStock.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCodBodSag.Text = dgvStock.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMarcaBod.Text = dgvStock.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtProcedenciaBod.Text = dgvStock.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtProductoBod.Text = dgvStock.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtUbicacionBod.Text = dgvStock.Rows[e.RowIndex].Cells[5].Value.ToString();
        }







        #endregion

        #region MantenedorUsuarios

        #endregion

        #region ControlPestañas
        //sirve para definir si se puede acceder a una pestaña dependiendo del tipo de usuario, en caso de de entrar a una pestaña de otro nivel
        //sera necesario iniciar sesion temporalmente con la otra cuenta, una vez finalizado el uso de la pestaña, se desconectara automaticamente y volvera
        //a la anterior
        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}


