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
        SqlConnection con = null;
        SqlDataAdapter daAluno;
        DataTable dtAluno;
        SqlCommandBuilder builder;

        Font f = new Font("Arial", 14);

        public Form1()
        {

            InitializeComponent();
            // Criando a conexão  

            carregaDataGrid();

            //Configura o DataGridView
            dgvAlunos.Font = f;
            dgvAlunos.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //seleciona a linha inteira
            dgvAlunos.MultiSelect = false; //impede seleção de várias células ou linhas

            dgvAlunos.Columns[0].Visible = false;
            dgvAlunos.Columns[1].Visible = false;

            dgvAlunos.Columns[0].DataPropertyName = "idaluno";
            dgvAlunos.Columns[1].DataPropertyName = "idcurso";
            dgvAlunos.Columns[2].DataPropertyName = "Nome";
            dgvAlunos.Columns[3].DataPropertyName = "Email";
            dgvAlunos.Columns[4].DataPropertyName = "Sexo";
            dgvAlunos.Columns[5].DataPropertyName = "Celular";

            dgvAlunos.Columns[2].HeaderText = "Nome";
            dgvAlunos.Columns[2].Width = 300;
            dgvAlunos.Columns[3].HeaderText = "E-mail";
            dgvAlunos.Columns[3].Width = 300;
            dgvAlunos.Columns[4].HeaderText = "Sexo";
            dgvAlunos.Columns[4].Width = 60;
            dgvAlunos.Columns[5].HeaderText = "Celular";
            dgvAlunos.Columns[5].Width = 150;

            //bloqueio acesso aos campos do formulário. 
            //só poderá acessar quando for inserir um novo ou editar
            grpDados.Enabled = false;

        }

        private void carregaDataGrid()
        {
             try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                //cria a instância da classe SqlDataAdapter
                daAluno = new SqlDataAdapter("SELECT idaluno,idcurso,Nome,Email,Sexo,Celular FROM Aluno", con);
                //Cria uma instância de Data Table
                dtAluno = new DataTable();
                //Preenche o DataTable com os dados vindos do Data Source, atavés do Data Adapter
                int v = daAluno.Fill(dtAluno);

                builder = new SqlCommandBuilder(daAluno);
                daAluno.InsertCommand = builder.GetInsertCommand();
                daAluno.UpdateCommand = builder.GetUpdateCommand();
                daAluno.DeleteCommand = builder.GetDeleteCommand();

                dgvAlunos.DataSource = dtAluno;  //vincula o DataGriView com a Fonte de Dados
            }
            catch (Exception)
            {
                MessageBox.Show("Falha ao acessar o banco de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 
        }

        private void dgvAlunos_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView row = (DataGridView)sender;
            if (row.CurrentRow == null)
                return;

            //preenche os Textboxes com os valores da linha selecionada no DataGrid
            txtNome.Text = dgvAlunos.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = dgvAlunos.CurrentRow.Cells[3].Value.ToString();
            txtCelular.Text = dgvAlunos.CurrentRow.Cells[5].Value.ToString();

            string sx = dgvAlunos.CurrentRow.Cells[4].Value.ToString();
            if (sx.Equals("M")) rdMasc.Checked = true;
            else if (sx.Equals("F")) rdFem.Checked = true;
            else rdOutro.Checked = true;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (daAluno == null)
                throw new Exception("Parametro adapter está null");
            if (con.State == ConnectionState.Closed)
                con.Open();

            if(daAluno.Update(dtAluno)>0)
            {
                MessageBox.Show("Banco de Dados atualizado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }

}
