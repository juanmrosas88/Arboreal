using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using GeoreferenciasAD;


using System.Data.SqlClient;


namespace MapasGoogle
{
    public partial class Form1 : Form
    {
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;

        int filaseleccionada = 0;
        double LatInicial = -31.5555087;
        double LngInicial = -63.5364147;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            dt.Columns.Add(new DataColumn("Latitud",typeof(double)));
            dt.Columns.Add(new DataColumn("Longitud", typeof(double)));

            //Insertando datos al dt para mostrar en la lista
         //   dt.Rows.Add("Ubicacion 1", LatInicial, LngInicial);
            dataGridView1.DataSource = dt;

            // desactivar las columnas de lat y long
         //   dataGridView1.Columns[1].Visible = false;
         //   dataGridView1.Columns[2].Visible = false;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 16;
            gMapControl1.AutoScroll = true;

            // Marcador
            markerOverlay = new GMapOverlay("Marcador") ;
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.green_dot);
            markerOverlay.Markers.Add(marker); //Agregamoss al mapa 

            //agregamos un tooltip de texto a los marcadores. 
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud: {1}", LatInicial, LngInicial);

            //Agregamos el mapa y el marcador al map control
            gMapControl1.Overlays.Add(markerOverlay);

        }

        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaseleccionada = e.RowIndex; // Fila seleccionmada 
                                           //Recuperamos los datos del grid y los asignamos a los textbox
            txtDescripcion.Text = dataGridView1.Rows[filaseleccionada].Cells[0].Value.ToString();
            txtLatitud.Text = dataGridView1.Rows[filaseleccionada].Cells[1].Value.ToString();
            TxtLongitud.Text = dataGridView1.Rows[filaseleccionada].Cells[2].Value.ToString();

            //se asignan los valores del grid al marcador 
            marker.Position = new PointLatLng(Convert.ToDouble(txtLatitud.Text), Convert.ToDouble(TxtLongitud.Text));
            //se posiciona el foco del mapa en ese punto
            gMapControl1.Position = marker.Position;


        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // se obtiene los datos de lat y long del mapa donde el usuario presiono

            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            //se posicionan en el text de la latitud y la long 
           txtLatitud.Text = lat.ToString();
           TxtLongitud.Text = lng.ToString();
            
            //Creamos el marcador para moverlo al lugar indicado 
            marker.Position = new PointLatLng(lat, lng);
            // Tambien se agrega el mensaje al marcador (tooltip) 
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud: {0} \n Longitud: {1}", lat, lng);

            dt.Rows.Add(txtDescripcion.Text, lat, lng); //agregar a la tabla

            //***************************************************
        


            //*************************************
            //Debo investigar sobre la publicacion de los datos en el mapa. 



        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Arbol nuevoRecurso = new Arbol();

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                nuevoRecurso.Descripcion = "Arbol";
                nuevoRecurso.Especie = "Especie Generica";
                nuevoRecurso.Latitud = Convert.ToDouble(fila.Cells[1].Value);
                nuevoRecurso.Longitud = Convert.ToDouble(fila.Cells[2].Value);

                AD_Recursos.AgregarArbol(nuevoRecurso);
               

            }
            if (dataGridView1.DataSource is DataTable)
            {
                ((DataTable)dataGridView1.DataSource).Rows.Clear();
                dataGridView1.Refresh();
            }
            //dataGridView1.DataSource = null;
            //procedimiento para ingresar en una base de datos
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(filaseleccionada);
            //remover de la tabla un registro
            //procedimiento paraa eliminar de una base de datos
        }
        #region BotonesImagenSatelital 
        private void BtnSat_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
        }
        private void BtnHibrid_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
        }
        private void BtnOriginal_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }
        private void BtnRelieve_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleTerrainMap;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackZoom.Value = Convert.ToInt32(gMapControl1.Zoom);
        }
        private void trackZoom_Scroll(object sender, EventArgs e)
        {

        }
        private void trackZoom_ValueChanged(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackZoom.Value;

        }

       
    }
}
