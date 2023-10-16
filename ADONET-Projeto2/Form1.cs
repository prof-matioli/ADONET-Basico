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
        SqlDataAdapter daCurso;
        DataTable dtCurso;
        SqlCommandBuilder builder;

        Font f = new Font("Arial", 14);

        private int modo = 0; //0=neutro, 1=inserir, 2=editar

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

            carregaComboCurso();
            cboCurso.Font = f;
            cboCurso.ValueMember = "idCurso";
            cboCurso.DisplayMember = "descricao";

            habilitar();

        }

        private void habilitar()
        {
            switch(modo)
            {
                case 0:
                    grpDados.Enabled = false;
                    btnNovo.Enabled = true;
                    btnEditar.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnCancelar.Enabled = false;
                    btnSalvar.Enabled = false;
                    
                    break;
                case 1:
                    grpDados.Enabled = true;
                    btnNovo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnSalvar.Enabled = true;
                    txtNome.Focus();
                    break;
                case 2:
                    grpDados.Enabled = true;
                    btnNovo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnSalvar.Enabled = true;
                    txtNome.Focus();
                    txtNome.SelectionLength=0;
                    break;
            }
        }

        private void limpaForm()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtCelular.Clear();
            rdFem.Checked = rdMasc.Checked = rdOutro.Checked = false;
        }

        private void carregaComboCurso()
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                //cria a instância da classe SqlDataAdapter
                daCurso = new SqlDataAdapter("SELECT * FROM Curso", con);
                //Cria uma instância de Data Table
                dtCurso = new DataTable();
                //Preenche o DataTable com os dados vindos do Data Source, atavés do Data Adapter
                int v = daCurso.Fill(dtCurso);
                cboCurso.DataSource = dtCurso;
             }
            catch (Exception)
            {
                MessageBox.Show("Falha ao acessar o banco de dados!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

            //posiciona o combobox
            cboCurso.SelectedItem = dgvAlunos.CurrentRow.Cells[1].Value;
            cboCurso.Refresh();

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            gravaDados();
            modo = 0;
            habilitar();
            carregaDataGrid();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            modo = 1;
            limpaForm();
            habilitar();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (daAluno == null)
                throw new Exception("Parametro adapter está null");
            if (con.State == ConnectionState.Closed)
                con.Open();


            if (daAluno.Update(dtAluno) > 0)
            {
                MessageBox.Show("Banco de Dados atualizado com sucesso!", "Aviso do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void gravaDados()
        {
            string sql="";
            string sx = "O";
            if (modo == 1 || modo == 2)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (rdFem.Checked) sx = "F";
                else if (rdMasc.Checked) sx = "M";
                else if (rdOutro.Checked) sx = "O";

                if (modo == 1)
                {
                    sql = "INSERT INTO aluno (idcurso, nome, email, sexo, celular) VALUES (@curso, @nome, @email, @sexo, @celular)";
                }
                else if (modo == 2)
                {
                    sql = "UPDATE aluno SET idcurso=@curso, nome=@nome, email=@email, sexo=@sexo, celular=@celular WHERE idaluno=@idAluno";
                }
                SqlCommand cmd = new SqlCommand(sql, con);

                if(modo==2)
                    cmd.Parameters.AddWithValue("idaluno", dgvAlunos.CurrentRow.Cells[0].Value.ToString());

                cmd.Parameters.AddWithValue("curso", cboCurso.SelectedValue);
                cmd.Parameters.AddWithValue("nome", txtNome.Text);
                cmd.Parameters.AddWithValue("celular", txtCelular.Text);
                cmd.Parameters.AddWithValue("email", txtEmail.Text);
                cmd.Parameters.AddWithValue("sexo", sx);

                if (cmd.ExecuteNonQuery() > 0)
                    MessageBox.Show("Dados atualizados com sucesso!", "Aviso do sisema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Falha ao atualizar dados!", "Aviso do sisema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            modo = 2;
            habilitar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            modo =0;
            habilitar();

        }

        private void dgvAlunos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView row = (DataGridView)sender;
            if (row.CurrentRow == null)
                return;

            if (dgvAlunos.CurrentRow.Cells[1].Value != null)
                cboCurso.SelectedValue = dgvAlunos.CurrentRow.Cells[1].Value;
  
        }
    }

}
