using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADONET_Projeto2
{
    public partial class Form1 : Form
    {
        Font f = new Font("Arial", 14);

        public Form1()
        {

            InitializeComponent();
            carregaDataGrid();

            dgvAlunos.Font = f;
            dgvAlunos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvAlunos.Columns[0].Visible = false;
            dgvAlunos.Columns[1].Visible = false;
            dgvAlunos.Columns[2].DataPropertyName = "nome";
            dgvAlunos.Columns[3].DataPropertyName = "email";
            dgvAlunos.Columns[4].DataPropertyName = "sexo";
            dgvAlunos.Columns[5].DataPropertyName = "celular";
            dgvAlunos.Columns[2].HeaderText = "Nome";
            dgvAlunos.Columns[2].Width = 300;
            dgvAlunos.Columns[3].HeaderText = "E-mail";
            dgvAlunos.Columns[3].Width = 300;
            dgvAlunos.Columns[4].HeaderText = "SEXO";
            dgvAlunos.Columns[4].Width = 60;
            dgvAlunos.Columns[5].HeaderText = "Celular";
            dgvAlunos.Columns[5].Width = 150;


        }

        private void carregaDataGrid()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                //cria a instância da classe SqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM aluno", con);
                //Cria uma instância de Data Table
                DataTable dt = new DataTable();
                //Preenche o DataTable com os dados vindos do Data Source, atavés do Data Adapter
                da.Fill(dt);
                dgvAlunos.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Falha ao acessar o banco de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }
    }

}
