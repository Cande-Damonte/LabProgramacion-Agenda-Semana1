using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabProgramacion_Agenda_Semana1
{
    public partial class Eliminar_Contacto : Form
    {
        public Eliminar_Contacto()
        {
            InitializeComponent();
        }

        private void Eliminar_Contacto_Load(object sender, EventArgs e)
        {
            // Abre un formulario para eliminar un contacto
            Eliminar_Contacto frm = new Eliminar_Contacto();
            frm.ShowDialog();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se crea una nueva instancia del formulario Agregar_Contacto
            Agregar_Contacto frm = new Agregar_Contacto();
            frm.ShowDialog();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se crea una nueva instancia del formulario Agregar_Contacto
            Agregar_Contacto frm = new Agregar_Contacto();
            frm.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            // Verificar que se haya ingresado un ID válido
            int id;
            if (!int.TryParse(txtid.Text, out id))
            {
                MessageBox.Show("Por favor, ingrese un ID de Contacto válido.");
                return;
            }

            ConexionBD conexion = new ConexionBD();

            try
            {
                CargarTreeView();
                conexion.EliminarContacto(id);
                conexion.listarContactos(dgvContactos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el Contacto: " + ex.Message);
            }
        }

        public void CargarTreeView()
        {
            ConexionBD conexion = new ConexionBD();

            // Limpiar el TreeView antes de volver a llenarlo
            treeView.Nodes.Clear();

            // Obtener los datos de los contactos desde la base de datos
            DataTable tablaContactos = conexion.ObtenerContactosPorCategoria();

            // Diccionario para almacenar los nodos de las categorías
            Dictionary<string, TreeNode> nodosPorCategoria = new Dictionary<string, TreeNode>();

            // Crear los nodos del TreeView
            foreach (DataRow row in tablaContactos.Rows)
            {
                string categoria = row["Categoria"].ToString();
                string nombre = row["Nombre"].ToString();
                string apellido = row["Apellido"].ToString();

                // Obtener o crear el nodo de la categoría
                TreeNode nodoCategoria;
                if (!nodosPorCategoria.TryGetValue(categoria, out nodoCategoria))
                {
                    nodoCategoria = new TreeNode(categoria);
                    treeView.Nodes.Add(nodoCategoria);
                    nodosPorCategoria[categoria] = nodoCategoria;
                }

                // Crear el nodo del contacto y agregarlo al nodo de la categoría
                TreeNode nodoContacto = new TreeNode($"{nombre}, {apellido}");
                nodoCategoria.Nodes.Add(nodoContacto);
            }

        }

    }
}
