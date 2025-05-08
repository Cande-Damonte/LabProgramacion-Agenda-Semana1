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
    public partial class Modificar_Contacto : Form
    {
        public Modificar_Contacto()
        {
            InitializeComponent();
        }

        private void Modificar_Contacto_Load(object sender, EventArgs e)
        {

            ConexionBD conexion = new ConexionBD();
            CargarTreeView();
            conexion.listarContactos(dgvContactos);
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se crea una nueva instancia del formulario Agregar_Contacto
            Agregar_Contacto frm = new Agregar_Contacto();
            frm.Show();
            this.Hide();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //permite eliminar contacto
            Eliminar_Contacto frm = new Eliminar_Contacto();
            frm.Show();
            this.Hide();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Se guardan los contactos modificados en el objeto ContactosModificados para guardarlos en la clase Contactos
            Contactos ContactoModificado = new Contactos();
            ConexionBD conexion = new ConexionBD();

            // Obtener el ID del producto a modificar (suponiendo que está en txtCodigo)
            //Creamos la variable codigo
            int codigo;
            //Convertimos el texto ingresado en txtid a un numero entero y lo guarda en la variable "codigo"
            if (!int.TryParse(txtid.Text, out codigo))
            {
                MessageBox.Show("Por favor, ingrese un ID de producto válido.");
                return;
            }

            //Tomamos los valores escritos en el txt y los guardamos en el objeto ContactoModificado para luego guardarla en la clase contactos
            ContactoModificado.id = codigo;
            ContactoModificado.Nombre = txtNombre.Text;
            ContactoModificado.Apellido = txtApellido.Text;
            ContactoModificado.Telefono = int.Parse(txtTelefono.Text);
            ContactoModificado.Correo = txtCorreo.Text;
            ContactoModificado.Categoria = cmbCategorias.Text;

            try
            {
                conexion.ModificarContacto(ContactoModificado);
                CargarTreeView();
                conexion.listarContactos(dgvContactos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el Contacto: " + ex.Message);
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

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inicio inicio = new Inicio();
            inicio.Show();
            this.Hide();
        }
    }
}
