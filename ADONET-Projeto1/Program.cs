using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace ADONET_Projeto1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().testaDataView_Five_DeleteRow();
            Console.WriteLine("\n\nFIM. Pressione qualquer tecla...");
            Console.ReadKey();
        }

        public void testaConexao()
        {
            try
            {
                String conString = @"Data Source=(LocalDB)\MSSQLLocalDB; 
                                AttachDbFilename=|DataDirectory|\APP_DATA\Database.mdf; 
                                Integrated Security = True";
                SqlConnection connection = new SqlConnection(conString);
                connection.Open();
                Console.WriteLine("\nConexão aberta com sucesso!\n");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"\nOcorreu uma exceção : {ex.Message}\n");
            }
        }

        public void testaDataView_Five_DeleteRow()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria instância de SqlDataAdapter e especifica o texto do comando SQL e
                //a o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter(
                                    "SELECT idaluno, idcurso, Nome, Sexo, Email, Celular " +
                                    "FROM Aluno", connection);
                //Cria Objeto DataTable
                DataTable AlunoDataTable = new DataTable();
                //Preenche a DataTable usando o Método Fill do objeto SqlDataAdapter
                dataAdapter.Fill(AlunoDataTable);
                //Cria instância de DataView usando a propriedade DefaultView 
                //de DataTable
                DataView dataView1 = new DataView(AlunoDataTable);
                //Você pode ver as linha no DataView
                Console.WriteLine($"Dados na DataView Antes do Delete:");
                foreach (DataRowView rowView in dataView1)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Email: {row["Email"]}");
                }
                //se quiser restringir a operação de exclusao em um dataview
                //então defina a propriedade AllowDelete para false
                dataView1.AllowDelete = true;
                //Deletando Alunos do Curso com ID=2 do DataView
                foreach (DataRowView rowView in dataView1)
                {
                    if (Convert.ToInt32(rowView["idcurso"]) == 2)
                    {
                        rowView.Delete();
                    }
                }
                //Você pode também deletar uma Linha Individual do DataView
                //como a seguir.
                //Aqui, o Indice da posição 0 especifica a primeira linha
                //dataView1[0].Delete();
                Console.WriteLine($"\nDados no DataView Após o Delete:");
                foreach (DataRowView row in dataView1)
                {
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                         $"IdCurso: {row["idcurso"]}," +
                         $"Nome: {row["Nome"]}, " +
                         $"Email: {row["Email"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }

        public void testaDataView_Five_UpdateRow()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria instância de SqlDataAdapter e especifica o texto do comando SQL e
                //a o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter(
                        "SELECT idaluno, idcurso, Nome, Sexo, Email, Celular " +
                        "FROM Aluno", connection);
                //Cria Objeto DataTable
                DataTable AlunoDataTable = new DataTable();
                //Preenche a DataTable usando o Método Fill do objeto SqlDataAdapter
                dataAdapter.Fill(AlunoDataTable);
                //Cria instância de DataView usando a propriedade DefaultView 
                //de DataTable
                DataView dataView1 = new DataView(AlunoDataTable);
                //Você pode ver as linha no DataView
                Console.WriteLine($"Dados do DataView antes da Atualização");
                foreach (DataRowView rowView in dataView1)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Email: {row["Email"]}");
                }
                //Se quisermos restringir a atualização do DataView 
                //sete a propriedade AllowEdit para false
                dataView1.AllowEdit = true;
                //Atualizando o Email para 'enf'+idaluno@fac.edu.br apenas para os cursos
                //cujos idcurso=9
                foreach (DataRowView rowView in dataView1)
                {
                    if (Convert.ToInt32(rowView["idcurso"]) == 9)
                    {
                        //podemos acessar as colunas usando o nome como chave
                        rowView["Email"] = "enf" + Convert.ToInt32(rowView["idaluno"]) + "@fac.edu.br";
                    }
                }
                Console.WriteLine($"Dados do DataView após a Atualização");
                foreach (DataRow row in AlunoDataTable.Rows)
                {
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                         $"IdCurso: {row["idcurso"]}," +
                         $"Nome: {row["Nome"]}, " +
                         $"Email: {row["Email"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }


        public void testaDataView_Four_AddNewRow()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria instância de SqlDataAdapter e especifica o texto do comando SQL e
                //a o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter(
                        "SELECT idaluno, idcurso, Nome, Sexo, Email, Celular " +
                        "FROM Aluno", connection);
                //Cria Objeto DataTable
                DataTable AlunoDataTable = new DataTable();
                //Preenche a DataTable usando o Método Fill do objeto SqlDataAdapter
                dataAdapter.Fill(AlunoDataTable);
                //Cria instância de DataView usando a propriedade DefaultView 
                //de DataTable
                DataView dataView1 = new DataView(AlunoDataTable);
                dataView1.AllowNew = true;
                DataRowView newRow = dataView1.AddNew();
                //Define os valores da coluna newRow
                newRow["idaluno"] = 22999;
                newRow["idcurso"] = 9;
                newRow["Nome"] = "Maria Felisbina";
                newRow["Sexo"] = "F";
                newRow["Email"] = "mf@gugou.kom.br";
                newRow["Celular"] = "(77)2332-9119-3443";
                //Podemos ver a nova linha no DataView
                Console.WriteLine($"\nDados presentes na DataView");
                foreach (DataRowView rowView in dataView1)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Sexo: {row["Sexo"]}");
                }
                //Não podemos ver a nova linha na DataTable
                Console.WriteLine($"\nDados presentes na DataTable:");
                foreach (DataRow row in AlunoDataTable.Rows)
                {
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                         $"IdCurso: {row["idcurso"]}," +
                         $"Nome: {row["Nome"]}, " +
                         $"Sexo: {row["Sexo"]}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }

        public void testaDataView_Three_Filter()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria instância de SqlDataAdapter e especifica o texto do comando SQL e
                //a o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter(
                        "SELECT idaluno, idcurso, Nome, Sexo, Email, Celular " +
                        "FROM Aluno", connection);
                //Cria Objeto DataTable
                DataTable AlunoDataTable = new DataTable();
                //Preenche a DataTable usando o Método Fill do objeto SqlDataAdapter
                dataAdapter.Fill(AlunoDataTable);
                //Cria instância de DataView usando a propriedade DefaultView 
                //de DataTable
                DataView dataView1 = AlunoDataTable.DefaultView;
                //Aplica um único filtro
                dataView1.RowFilter = "Sexo='F'";
                Console.WriteLine($"DataView com Filtro: {dataView1.RowFilter}");
                foreach (DataRowView rowView in dataView1)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Sexo: {row["Sexo"]}");
                }
                //Cria instância de DataView usando o Construtor de DataView
                //Inicializa uma nova instância da classe DataView com a DataTable especificada
                DataView dataView2 = new DataView(AlunoDataTable);
                //Aplica múltiplos filtros
                dataView2.RowFilter = "Sexo='F' AND idcurso=2";
                Console.WriteLine($"\nDataView with Filter: {dataView2.RowFilter}");
                foreach (DataRowView rowView in dataView2)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Sexo: {row["Sexo"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }


        public void testaDataView_Two_Sorting()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria instância de SqlDataAdapter e especifica o texto do comando SQL e
                //a o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter(
                        "SELECT idaluno, idcurso, Nome, Sexo, Email, Celular " +
                        "FROM Aluno WHERE idcurso=9", connection);
                //Cria Objeto DataTable
                DataTable AlunoDataTable = new DataTable();
                //Preenche a DataTable usando o Método Fill do objeto SqlDataAdapter
                dataAdapter.Fill(AlunoDataTable);
                //Cria instância de DataView usando o Construtor de DataView
                //Inicializa uma nova instância da classe DataView com a DataTable especificada
                DataView dataView1 = new DataView(AlunoDataTable);
                dataView1.Sort = "Nome ASC";
                Console.WriteLine("DataView Sorted By: Nome ASC");
                foreach (DataRowView rowView in dataView1)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Sexo: {row["Sexo"]}");
                }
                //Cria instância de DataView usando o Construtor de DataView
                //Inicializa uma nova instância da classe DataView com a DataTable especificada
                DataView dataView2 = new DataView(AlunoDataTable);
                dataView2.Sort = "Sexo DESC, Nome ASC";
                Console.WriteLine("\nDataView Sorted By: Sexo DESC e Nome ASC");
                foreach (DataRowView rowView in dataView2)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Sexo: {row["Sexo"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }


        public void testaDataView_One()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria instância de SqlDataAdapter e especifica o texto do comando SQL e
                //a o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT idaluno, idcurso, Nome, Sexo, Email, Celular " +
                                                                "FROM Aluno", connection);
                //Cria Objeto DataTable
                DataTable AlunoDataTable = new DataTable();
                //Preenche a DataTable usando o Método Fill do objeto SqlDataAdapter
                dataAdapter.Fill(AlunoDataTable);
                //Cria instância de DataView usando o Construtor de DataView
                //Inicializa uma nova instância da classe DataView com a DataTable especificada
                DataView dataView1 = new DataView(AlunoDataTable);
                Console.WriteLine("Accessando DataView usando Loop For:");
                for (int i = 0; i < dataView1.Count; i++)
                {
                    Console.WriteLine($"IdCurso:{dataView1[i]["idcurso"]}, " +
                        $"Nome: {dataView1[i]["Nome"]}, " +
                        $"Sexo: {dataView1[i]["Sexo"]}, " +
                        $"Email: {dataView1[i]["Email"]}, " +
                        $"Celular: {dataView1[i]["Celular"]}");
                }
                //Creating DataView instance using DefaultView property of Data Table
                DataView dataView2 = AlunoDataTable.DefaultView;
                Console.WriteLine("\nAcessando a DataView usando o Loop Foreach:");
                foreach (DataRowView rowView in dataView2)
                {
                    DataRow row = rowView.Row;
                    Console.WriteLine($"IdAluno: {row["idaluno"]}," +
                        $"IdCurso: {row["idcurso"]}," +
                        $"Nome: {row["Nome"]}, " +
                        $"Sexo: {row["Sexo"]}, " +
                        $"Email: {row["Email"]}," +
                        $"Celular: {row["Celular"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }


        public void testaGenericExecutaStoredProcedure()
        {
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Executando o Stored Procedure spGetAlunosByCursoSexo
                //Cria SqlParameter, Requerido pelo Stored Procedure spGetAlunosByCursoSexo
                SqlParameter[] paramterList = new SqlParameter[]
                {
                    new SqlParameter("@Sexo", 'F'),
                    new SqlParameter("@IdCurso", 2)
                };
                //Chama o Método Genérico para Executar o Stored Procedure que retorna um DataSet
                DataSet dataSet1 = ExecuteStoredProcedureReturnDataSet(ConString,
                                                                        "spGetAlunosByCursoSexo",
                                                                        paramterList);
                Console.WriteLine("spGetAlunosByCursoSexo - Resultado:");
                foreach (DataRow row in dataSet1.Tables[0].Rows)
                {
                    //Acessando os Dados usando o nome da coluna como chave
                    Console.WriteLine(row["idaluno"] + ",  " + row["idcurso"] + ",  " +
                                      row["Nome"] + ",  " + row["Sexo"] + ",  " +
                                      row["Email"] + ",  " + row["Celular"]);
                }
                //Executando o Stored Procedure spGetAlunos, que não exige parametros
                DataSet dataSet2 = ExecuteStoredProcedureReturnDataSet(ConString, "spGetAlunos");
                Console.WriteLine("\nspGetAlunos - Resultados:");
                foreach (DataRow row in dataSet2.Tables[0].Rows)
                {
                    //Acessando os Dados usando o nome da coluna como chave
                    Console.WriteLine(row["idaluno"] + ",  " + row["idcurso"] + ",  " +
                                      row["Nome"] + ",  " + row["Sexo"] + ",  " +
                                      row["Email"] + ",  " + row["Celular"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
        }

        public static DataSet ExecuteStoredProcedureReturnDataSet(string connectionString,
                                 string procedureName, params SqlParameter[] paramterList)
        {
            //Create DataSet Object
            //Cria Objeto DataSet
            DataSet dataSet = new DataSet();
            //Cria o objeto connection usando o parametro connectionString que
            //recebeu como parametro de entrada
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                //Create the command object
                //Cria o objeto command
                using (var command = sqlConnection.CreateCommand())
                {
                    //Cria o objeto SqlDataAdapter passando o objeto command como um
                    //parametro para o construtor
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        //Define o tipo do command como StoredProcedure
                        command.CommandType = CommandType.StoredProcedure;
                        //Define o command text como o nome do stored procedure que se deseja
                        //executar. O nome do stored procedure foi recebido como um parametro nete método
                        //genérico.
                        command.CommandText = procedureName;
                        //Se a lista de Parameters não for null, adiciona cada item da parameterlist
                        //na coleção Parameters do objeto Command.
                        if (paramterList != null)
                        {
                            command.Parameters.AddRange(paramterList);
                        }
                        //Preenche o DataSet
                        sda.Fill(dataSet);
                    }
                }
            }
            //Retorna o DataSet para quem chamou o Método Genérico.
            return dataSet;
        }


        public void testaADOUsingStoredProcedure2()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria um Objeto DataSet
                DataSet ds = new DataSet();
                //Cria o objeto SqlCommand passando o nome do stored procedure
                //e o objeto connection como parametros
                SqlCommand cmd = new SqlCommand("spGetAlunosByCursoSexo", connection)
                {
                    //Especifica o command type como Stored Procedure
                    CommandType = CommandType.StoredProcedure
                };
                //Adiciona os parêmtros diretamente ao objeto Sqlcommand usando 
                //o método AddWithValue
                cmd.Parameters.AddWithValue("@IdCurso", 2);
                cmd.Parameters.AddWithValue("@Sexo", "M");
                //Cria Objeto SqlDataAdapter passando o objeto Command como parâmetro
                //para o construtor
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //Chama o Método Fill para preencher o DataSet
                da.Fill(ds);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Accessing the Data using the string column name as key
                    //Acessando os Dados usando o nome da coluna como chave
                    Console.WriteLine(row["idaluno"] + ",  " + row["idcurso"] + ",  "
                                    + row["Nome"] + ",  " + row["Sexo"] + ",  "
                                    + row["Email"] + ",  " + row["Celular"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }

        }

        public void testaADOUsingStoredProcedure()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                //Cria um objeto SqlConnection
                connection = new SqlConnection(ConString);
                //Cria um Objeto DataSet
                DataSet ds = new DataSet();
                //Cria o objeto SqlCommand passando o nome do stored procedure
                //e o objeto connection como parametros
                SqlCommand cmd = new SqlCommand("spGetAlunosByCursoSexo", connection)
                {
                    //Especifica o command type como Stored Procedure
                    CommandType = CommandType.StoredProcedure
                };
                //Define parâmetros de Entrada
                SqlParameter param1 = new SqlParameter //matricula do professor
                {
                    ParameterName = "@IdCurso", //Nome do parametro definido no Stored Procedure
                    SqlDbType = SqlDbType.NVarChar, //Tipo de dado do parâmetro
                    Value = "9", //Valor passado para o parâmetro
                    Direction = ParameterDirection.Input //Define o parâmetro como Entrada
                };
                cmd.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter //matricula do professor
                {
                    ParameterName = "@Sexo", //Nome do parametro definido no Stored Procedure
                    SqlDbType = SqlDbType.Char, //Tipo de dado do parâmetro
                    Value = "M", //Valor passado para o parâmetro
                    Direction = ParameterDirection.Input //Define o parâmetro como Entrada
                };
                cmd.Parameters.Add(param2);
                //Cria Objeto SqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter
                {
                    //Especifica o Select Command como o objeto command que criamos
                    SelectCommand = cmd
                };
                //Chama o Método Fill para preencher o DataSet
                da.Fill(ds);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //Accessing the Data using the string column name as key
                    //Acessando os Dados usando o nome da coluna como chave
                    Console.WriteLine(row["idaluno"] + ",  " + row["idcurso"] + ",  "
                                    + row["Nome"] + ",  " + row["Sexo"] + ",  "
                                    + row["Email"] + ",  " + row["Celular"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu uma exceção : {ex.Message}");
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }

        }

        public void testaStoreProcedureComParametroEntradaSaida()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                //Cria o objeto SqlCommand passando o nome do stored procedure
                //e o objeto connection como parametros
                SqlCommand cmd = new SqlCommand("spCriaProfessor", connection)
                {
                    //Especifica o command type como Stored Procedure
                    CommandType = CommandType.StoredProcedure
                };
                //Define parâmetros de Entrada
                SqlParameter param1 = new SqlParameter //matricula do professor
                {
                    ParameterName = "@matricula", //Nome do parametro definido no Stored Procedure
                    SqlDbType = SqlDbType.NVarChar, //Tipo de dado do parâmetro
                    Value = "555555", //Valor passado para o parâmetro
                    Direction = ParameterDirection.Input //Define o parâmetro como Entrada
                };
                cmd.Parameters.Add(param1);
                //Outra forma possível de adicionar Input Parameter
                cmd.Parameters.AddWithValue("@nome", "Marcus Aéreo");
                //Define parâmetro de Saída
                SqlParameter outParameter = new SqlParameter
                {
                    ParameterName = "@Id", //Nome do parametro definido no Stored Procedure
                    SqlDbType = SqlDbType.Int, //Tipo de dado do parâmetro
                    Direction = ParameterDirection.Output //Define o parâmetro como Saída
                };
                //Adiciona o parâmetro ao objeto SqlCommand
                cmd.Parameters.Add(outParameter);
                //Abre a conexão
                connection.Open();
                //executa a query
                cmd.ExecuteNonQuery();
                //Now you can access the output parameter using the value propery of outParameter object
                //Agora podemos acessar o parâmetro de saída usando a Propriedade Value do objeto 
                //outParameter.
                Console.WriteLine("ID do novo professor criado : " + outParameter.Value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaStoreProcedureComParametroEntrada()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);

                //Cria o objeto SqlCommand passando o nome do stored procedure
                //e o objeto connection como parametros
                SqlCommand cmd = new SqlCommand("spGetProfessorById", connection)
                {
                    //Especifica o command type como Stored Procedure
                    CommandType = CommandType.StoredProcedure
                };
                //Define SqlParameter
                SqlParameter param1 = new SqlParameter
                {
                    ParameterName = "@Id", //Nome do parametro definido no Stored Procedure
                    SqlDbType = SqlDbType.Int, //Tipo de dado do parâmetro
                    Value = 4, //Valor passado para o parâmetro
                    Direction = ParameterDirection.Input //Define o parâmetro como Entrada
                };
                //Adiciona o parâmetro ao objeto SqlCommand
                cmd.Parameters.Add(param1);
                //Abre a conexão
                connection.Open();
                //Executa o commando i.e. Executando o Stored Procedure
                //usando o método ExecuteReader
                //SqlDataReader requirer conexão ativa e aberta
                SqlDataReader sdr = cmd.ExecuteReader();
                //Lê o dato do SqlDataReader 
                //O método Read() retornará true enquanto houver dado em SqlDataReader
                while (sdr.Read())
                {
                    //Acessando os dados pela chave nome da coluna
                    Console.WriteLine(sdr["idprofessor"] + ",  " +
                                      sdr["matricula"] + ",  " +
                                      sdr["nome"] + ",  ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }


        public void testaStoreProcedureSemParametro()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                //Cria o objeto SqlCommand passando o nome do stored procedure
                //e o objeto connection como parametros
                SqlCommand cmd = new SqlCommand("spGetProfessores", connection)
                {
                    //Especifica o command type como Stored Procedure
                    CommandType = CommandType.StoredProcedure
                };
                //Abre a conexão
                connection.Open();
                //Executa o commando i.e. Executando o Stored Procedure
                //usando o método ExecuteReader
                //SqlDataReader requirer conexão ativa e aberta
                SqlDataReader sdr = cmd.ExecuteReader();
                //Lê o dato do SqlDataReader 
                //O método Read() retornará true enquanto houver dado em SqlDataReader
                while (sdr.Read())
                {
                    //Acessando os dados pela chave nome da coluna
                    Console.WriteLine(sdr["idprofessor"] + ",  " +
                                      sdr["matricula"] + ",  " +
                                      sdr["nome"] + ",  ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaRemoveDataTableDataSet()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                // Escrevemos duas instruçoes Select para retornar dados das
                // tabelas de professores e de cursos
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from professor ;" +
                    "select * from curso", connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                dataSet.Tables[0].TableName = "Professores";
                dataSet.Tables[1].TableName = "Cursos";
                // Buscando dados da Primeira Tabela, Professor
                Console.WriteLine("Data Table Professores");
                //Acessando a DataTable do DataSet usando o nome customizado
                foreach (DataRow row in dataSet.Tables["Professores"].Rows)
                {
                    //Acessando as colunas usando os nomes das colunas
                    Console.WriteLine(row["idprofessor"] + ",  "
                                    + row["matricula"] + ",  "
                                    + row["nome"]);
                }
                Console.WriteLine();
                // Buscando dados da Segunda Tabela, Curso
                Console.WriteLine("Data Table Cursos");
                //Acessando a DataTable do DataSet usando o nome customizado
                foreach (DataRow row in dataSet.Tables["Cursos"].Rows)
                {
                    //Acessando as colunas através o índice da posição como chave
                    Console.WriteLine(row[0] + ",  " +
                                        row[1] + ",  " +
                                        row[2] + ",  " +
                                        row[3]);
                }
                Console.WriteLine();
                //Agora, vamos deletar a DataTable Cursos do DataSet
                if (dataSet.Tables.Contains("Cursos") && dataSet.Tables.CanRemove(dataSet.Tables["Cursos"]))
                {
                    Console.WriteLine("Deletando a DataTable Cursos ...");
                    dataSet.Tables.Remove(dataSet.Tables["Cursos"]);
                    //dataSet.Tables.Remove(dataSet.Tables[1]);
                }
                //Agora verifica se a DataTable existe ou não
                if (dataSet.Tables.Contains("Cursos"))
                {
                    Console.WriteLine("DataTable Cursos Existe!");
                }
                else
                {
                    Console.WriteLine("DataTable Cursos Não Existe Mais!!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaMetodosDataSetCopyCloneClear()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from professor", connection);
                DataSet dataSetOriginal = new DataSet();
                dataAdapter.Fill(dataSetOriginal);

                Console.WriteLine("DataSet Original");
                foreach (DataRow row in dataSetOriginal.Tables[0].Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  "
                                    + row["matricula"] + ",  "
                                    + row["nome"]);
                }
                Console.WriteLine();
                Console.WriteLine("Cópia do DataSet");
                //Copia ambos, estrutura e dados, para este DataSet
                DataSet dataSetCopia = dataSetOriginal.Copy();
                if (dataSetCopia.Tables != null)
                {

                    foreach (DataRow row in dataSetCopia.Tables[0].Rows)
                    {
                        Console.WriteLine(row["idprofessor"] + ",  "
                                        + row["matricula"] + ",  "
                                        + row["nome"]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Clone do DataSet");
                // Copia a estrutura do DataSet, incluindo tudo DataTable, schemas,
                // relations e constraints. Não copia qualquer dado.
                DataSet dataSetClone = dataSetOriginal.Clone();
                if (dataSetClone.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSetClone.Tables[0].Rows)
                    {
                        Console.WriteLine(row["idprofessor"] + ",  "
                                        + row["matricula"] + ",  "
                                        + row["nome"]);
                    }
                }
                else
                {
                    Console.WriteLine("DataSet Clone está Vazio");
                    Console.WriteLine("Adicionando Dados na Tabela do DataSet Clone");
                    dataSetClone.Tables[0].Rows.Add(101, "11111", "Professor Clone UM");
                    dataSetClone.Tables[0].Rows.Add(101, "22222", "Professor Clone DOIS");
                    foreach (DataRow row in dataSetClone.Tables[0].Rows)
                    {
                        Console.WriteLine(row["idprofessor"] + ",  "
                                        + row["matricula"] + ",  "
                                        + row["nome"]);
                    }
                }
                Console.WriteLine();
                // Limpa qualquer dados do DataSet removendo todas as linhas 
                // em todas as tabelas
                dataSetCopia.Clear();
                if (dataSetCopia.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSetCopia.Tables[0].Rows)
                    {
                        Console.WriteLine(row["idprofessor"] + ",  "
                                        + row["matricula"] + ",  "
                                        + row["nome"]);
                    }
                }
                else
                {
                    Console.WriteLine("Depois de limpo não há nenhum dado...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaDataSetSqlServerMultiTableTableNameExplicito()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                //Observe que temos uma query com dois Selects, que retorna dados
                //de duas tabelas, professor e curso
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from professor;" +
                    "select * from curso", connection);
                //Criação do Objeto DataSet
                DataSet dataSet = new DataSet();
                //Data Table 1 terá dados de professores e estará na Posição de Índice 0
                //Data Table 2 terá dados de cursos e estará na Posição de Índice 1
                dataAdapter.Fill(dataSet);
                //Define explicitamente o nome das tabelas
                dataSet.Tables[0].TableName = "Professores";
                dataSet.Tables[1].TableName = "Cursos";
                // Obtendo primeira tabela de dados - Professores
                Console.WriteLine("Data Table Professores");
                //Acessando Data Table do DataSet usando o 'name' explicito
                foreach (DataRow row in dataSet.Tables["Professores"].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    Console.WriteLine(row["idprofessor"] + ",  "
                                    + row["matricula"] + ",  "
                                    + row["nome"]);
                    //Acessado os Dados usando o índice da posição como chave
                    //Console.WriteLine(row[0] + ",  " + row[1] + ",  " + row[2]);
                }
                Console.WriteLine();
                // Obtendo a segunda tabela de dados - Cursos
                Console.WriteLine("Data Table Cursos");
                //Acessando Data Table do DataSet usando o 'name' explicito
                foreach (DataRow row in dataSet.Tables["Cursos"].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    //Console.WriteLine(row["idcurso"] + ", " 
                    //                + row["idprofessor"] + ",  "
                    //                + row["descricao"] + ",  "
                    //                + row["qtd_alunos"]);

                    //Acessado os Dados usando o índice da posição como chave
                    Console.WriteLine(row[0] + ",  " +
                                        row[1] + ",  " +
                                        row[2] + ",  " +
                                        row[3]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }
        public void testaDataSetSqlServerMultiTableTableNameDefault()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                //Observe que temos uma query com dois Selects, que retorna dados
                //de duas tabelas, professor e curso
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from professor;" +
                    "select * from curso", connection);
                //Criação do Objeto DataSet
                DataSet dataSet = new DataSet();
                //Data Table 1 terá dados de professores e estará na Posição de Índice 0
                //Data Table 2 terá dados de cursos e estará na Posição de Índice 1
                dataAdapter.Fill(dataSet);
                // Obtendo primeira tabela de dados - Professores
                Console.WriteLine("Data Table 1 - Professor");
                //Acessando Data Table do DataSet usando o 'name' default
                //Por padrão, a primeira o 'name' da primeira tabela é Table
                foreach (DataRow row in dataSet.Tables["Table1"].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    Console.WriteLine(row["idprofessor"] + ",  "
                                    + row["matricula"] + ",  "
                                    + row["nome"]);
                    //Acessado os Dados usando o índice da posição como chave
                    //Console.WriteLine(row[0] + ",  " + row[1] + ",  " + row[2]);
                }
                Console.WriteLine();
                // Obtendo a segunda tabela de dados - Cursos
                Console.WriteLine("Data Table 2 - Cursos");
                //Acessando Data Table do DataSet usando o 'name' default
                //Por padrão, a primeira o 'name' da primeira tabela é Table1
                foreach (DataRow row in dataSet.Tables["Table2"].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    //Console.WriteLine(row["idcurso"] + ", " 
                    //                + row["idprofessor"] + ",  "
                    //                + row["descricao"] + ",  "
                    //                + row["qtd_alunos"]);

                    //Acessado os Dados usando o índice da posição como chave
                    Console.WriteLine(row[0] + ",  " +
                                        row[1] + ",  " +
                                        row[2] + ",  " +
                                        row[3]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }


        public void testaDataSetSqlServerMultiTableIndexPosition()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                //Observe que temos uma query com dois Selects, que retorna dados
                //de duas tabelas, professor e curso
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from professor;" +
                    "select * from curso", connection);
                //Criação do Objeto DataSet
                DataSet dataSet = new DataSet();
                //Data Table 1 terá dados de professores e estará na Posição de Índice 0
                //Data Table 2 terá dados de cursos e estará na Posição de Índice 1
                dataAdapter.Fill(dataSet);

                // Obtendo primeira tabela de dados - Professores
                Console.WriteLine("Data Table 1 - Professor");
                //Acessando Data Table do DataSet usando o Índice da Posição
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    Console.WriteLine(row["idprofessor"] + ",  "
                                    + row["matricula"] + ",  "
                                    + row["nome"]);
                    //Acessado os Dados usando o índice da posição como chave
                    //Console.WriteLine(row[0] + ",  " + row[1] + ",  " + row[2]);
                }
                Console.WriteLine();
                // Obtendo a segunda tabela de dados - Cursos
                Console.WriteLine("Data Table 2 - Cursos");
                //Acessando Data Table do DataSet usando o Índice da Posição
                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    //Console.WriteLine(row["idcurso"] + ", " 
                    //                + row["idprofessor"] + ",  "
                    //                + row["descricao"] + ",  "
                    //                + row["qtd_alunos"]);

                    //Acessado os Dados usando o índice da posição como chave
                    Console.WriteLine(row[0] + ",  " +
                                        row[1] + ",  " +
                                        row[2] + ",  " +
                                        row[3]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }


        public void testaDataSetSqlServer()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.
                                        ConnectionStrings["ConnectionString"].
                                        ConnectionString;
                connection = new SqlConnection(ConString);
                //Cria a instância de SqlDataAdapter especificando o Command Text
                //e o objeto connection
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from professor",
                                                    connection);
                //Criação do Objeto DataSet
                DataSet dataSet = new DataSet();
                //Preenchimento do DataSet usando o método Fill do objeto SqlDataAdapter
                //Aqui, nos não especificamos o nome do DataTable e o DataTable será
                //será criado na posição de índice 0.
                dataAdapter.Fill(dataSet);

                //Iteração através do DataSet
                //Primeiro obtém o DataTable do dataset e então obtém as linhas (rows)
                //usando a propriedade Rows da DataTable
                foreach (DataRow row in dataSet.Tables["Table"].Rows)
                {
                    //Acessando os Dados usando a chave column name
                    Console.WriteLine(row["idprofessor"] + ",  "
                                    + row["matricula"] + ",  "
                                    + row["nome"]);
                    //Acessado os Dados usando o índice da posição como chave
                    //Console.WriteLine(row[0] + ",  " + row[1] + ",  " + row[2]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }


        public void testaClasseDataSet()
        {
            //Criando a tabela Cliente
            DataTable Cliente = new DataTable("Cliente");
            //Criando Columns e Schema
            DataColumn ClienteId = new DataColumn("ID", typeof(Int32));
            Cliente.Columns.Add(ClienteId);
            DataColumn Nome = new DataColumn("Nome", typeof(string));
            Cliente.Columns.Add(Nome);
            DataColumn Fone = new DataColumn("Fone", typeof(string));
            Cliente.Columns.Add(Fone);
            //Adicionando Data Rows na tabela Cliente
            Cliente.Rows.Add(101, "Markus Angnius", "98 9837-8372");
            Cliente.Rows.Add(102, "Aktana Fantany", "69 9822-1253");

            //Criando a tabela Pedidos
            DataTable Pedidos = new DataTable("Pedidos");
            //Criando Columns e Schema
            DataColumn PedidoID = new DataColumn("ID", typeof(Int32));
            Pedidos.Columns.Add(PedidoID);
            DataColumn Cliente_Id = new DataColumn("ClienteID", typeof(string));
            Pedidos.Columns.Add(Cliente_Id);
            DataColumn Quantidade = new DataColumn("Quantidade", typeof(Int32));
            Pedidos.Columns.Add(Quantidade);
            //Adicionando Data Rows na tabela Pedidos
            Pedidos.Rows.Add(1001, 101, 20000);
            Pedidos.Rows.Add(1002, 102, 30000);

            //Criando objeto DataSet
            DataSet datSet = new DataSet();
            //Adicionando as tabelas ao DataSet
            datSet.Tables.Add(Cliente);
            datSet.Tables.Add(Pedidos);

            //Acessando os dados da Tabela Clientes pelo Índice da tabela
            Console.WriteLine("Tabela de Clientes: ");
            foreach (DataRow row in datSet.Tables[0].Rows)
            {
                Console.WriteLine(row["ID"] + ", " + row["Nome"] + ", " + row["Fone"]);
            }
            Console.WriteLine();

            //Acessando os dados da Tabela Pedidos pelo Nome da tabela
            Console.WriteLine("Tabela de Pedidos: ");
            foreach (DataRow row in datSet.Tables["Pedidos"].Rows)
            {
                Console.WriteLine(row["ID"] + ", " +
                    row["ClienteID"] + ", " +
                    row["Quantidade"]);
            }
        }

        public void testaMetodoRejectChanges()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connection = new SqlConnection(ConString);
                SqlDataAdapter da = new SqlDataAdapter("select * from professor", connection);
                DataTable originalDT = new DataTable();
                da.Fill(originalDT);
                Console.WriteLine("Antes da exclusão");
                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  " + row["matricula"] + ",  " + row["nome"]);
                }
                Console.WriteLine();
                foreach (DataRow row in originalDT.Rows)
                {
                    if (Convert.ToInt32(row["idprofessor"]) == 3)
                    {
                        row.Delete();
                        Console.WriteLine("Linha com professorid=3 foi Deletada");
                    }
                }
                //Dando um Rollback nos Dados
                originalDT.RejectChanges();
                Console.WriteLine();
                Console.WriteLine("Dando Rollback na exclusão");
                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  " + row["matricula"] + ",  " + row["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaMetodoRemoveDataRow()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connection = new SqlConnection(ConString);
                SqlDataAdapter da = new SqlDataAdapter("select * from professor", connection);
                DataTable originalDT = new DataTable();
                da.Fill(originalDT);
                Console.WriteLine("Antes da exclusão");
                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  " + row["matricula"] + ",  " + row["nome"]);
                }
                Console.WriteLine();
                foreach (DataRow row in originalDT.Select())
                {
                    if (Convert.ToInt32(row["idprofessor"]) == 3)
                    {
                        originalDT.Rows.Remove(row);
                        Console.WriteLine("Linha com idprofessor 3 Deletada");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Depois da exclusão");
                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  " + row["matricula"] + ",  " + row["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }


        public void testaMetodoDeleteDataRow()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connection = new SqlConnection(ConString);
                SqlDataAdapter da = new SqlDataAdapter("select * from professor", connection);
                DataTable originalDT = new DataTable();
                da.Fill(originalDT);
                Console.WriteLine("Antes da exclusão");
                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  " + row["matricula"] + ",  " + row["nome"]);
                }
                Console.WriteLine();
                foreach (DataRow row in originalDT.Rows)
                {
                    if (Convert.ToInt32(row["idprofessor"]) == 3)
                    {
                        row.Delete();
                        Console.WriteLine("Linha com professorid=3 foi Deletada");
                    }
                }
                originalDT.AcceptChanges();
                Console.WriteLine();
                Console.WriteLine("Depois da exclusão");
                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["idprofessor"] + ",  " + row["matricula"] + ",  " + row["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaMetodosDataTableCopyClone()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connection = new SqlConnection(ConString);
                SqlDataAdapter da = new SqlDataAdapter("select * from professor", connection);
                DataTable originalDT = new DataTable();
                da.Fill(originalDT);

                foreach (DataRow row in originalDT.Rows)
                {
                    Console.WriteLine(row["matricula"] + ",  " + row["nome"]);
                }

                Console.WriteLine();
                Console.WriteLine("Copia da DataTable: copyDataTable");
                DataTable copyDataTable = originalDT.Copy();
                if (copyDataTable != null)
                {
                    foreach (DataRow row in copyDataTable.Rows)
                    {
                        Console.WriteLine(row["matricula"] + ",  " + row["nome"]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Clone da  DataTable : cloneDataTable");
                DataTable cloneDataTable = originalDT.Clone();
                if (cloneDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in cloneDataTable.Rows)
                    {
                        Console.WriteLine(row["matricula"] + ",  " + row["nome"]);
                    }
                }
                else
                {
                    Console.WriteLine("cloneDataTable está vazia");
                    Console.WriteLine("Adicionando dados em  cloneDataTable");
                    cloneDataTable.Rows.Add(1001, "112233", "Zé da Silva");
                    cloneDataTable.Rows.Add(10011, "332211", "Maria Xikinha");
                    foreach (DataRow row in cloneDataTable.Rows)
                    {
                        Console.WriteLine(row["matricula"] + ",  " + row["nome"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }

        public void testaMetodosDataTableRows()
        {
            SqlConnection connection = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                connection = new SqlConnection(ConString);
                SqlDataAdapter da = new SqlDataAdapter("select * from professor", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["matricula"] + ",  " + row["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                connection.Close();
            }
        }


        public void testaSqlDataTableExemplo1()
        {

            DataTable dataTable = new DataTable("professor");

            DataColumn idprofessor = new DataColumn("idprofessor");
            idprofessor.DataType = typeof(int);
            idprofessor.Unique = true;
            idprofessor.AllowDBNull = false;
            idprofessor.Caption = "Id Prodessor";
            dataTable.Columns.Add(idprofessor);

            DataColumn matricula = new DataColumn("matricula");
            matricula.MaxLength = 10;
            matricula.AllowDBNull = false;
            dataTable.Columns.Add(matricula);

            DataColumn nome = new DataColumn("nome");
            matricula.MaxLength = 50;
            matricula.AllowDBNull = false;
            dataTable.Columns.Add(nome);

            DataRow row1 = dataTable.NewRow();
            row1["idprofessor"] = 1001;
            row1["matricula"] = "987654";
            row1["nome"] = "Karlla Luzzena";
            dataTable.Rows.Add(row1);

            dataTable.Rows.Add(1002, "123456", "Francisco Xipriano");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["idprofessor"] + " - " +
                                  row["matricula"] + " - " +
                                  row["nome"]);
            }
        }


        public void testaSqlDataTableExemplo2()
        {
            DataTable dataTable = new DataTable("professor");

            DataColumn idprofessor = new DataColumn
            {
                ColumnName = "idprofessor",
                DataType = System.Type.GetType("System.Int32"),
                AutoIncrement = true,
                AutoIncrementSeed = 1000,
                AutoIncrementStep = 10,
                Unique = true,
                AllowDBNull = false,
                Caption = "Id Prodessor",
            };

            dataTable.Columns.Add(idprofessor);

            DataColumn matricula = new DataColumn("matricula");
            matricula.MaxLength = 10;
            matricula.AllowDBNull = false;
            dataTable.Columns.Add(matricula);

            DataColumn nome = new DataColumn("nome");
            matricula.MaxLength = 50;
            matricula.AllowDBNull = false;
            dataTable.Columns.Add(nome);

            DataRow row1 = dataTable.NewRow();
            row1["matricula"] = "987654";
            row1["nome"] = "Karlla Luzzena";
            dataTable.Rows.Add(row1);

            dataTable.Rows.Add(null, "123456", "Francisco Xipriano");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["idprofessor"] + " - " +
                                  row["matricula"] + " - " +
                                  row["nome"]);
            }


        }


        public void testaSqlDataAdapterStoredProcedure()
        {
            SqlConnection con = null;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                SqlDataAdapter da = new SqlDataAdapter("spProfessores", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["matricula"] + " - " + row["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }

        public void testaSqlDataAdapter()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                //cria a instância da classe SqlDataAdapter
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM professor", con);
                //Cria uma instância de Data Table
                DataTable dt = new DataTable();
                //Preenche o DataTable com os dados vindos do Data Source, atavés do Data Adapter
                da.Fill(dt);
                //As seguintes ações são realizadas pelo método Fill
                /*
                 * 1) Abre a Connection
                 * 2) Executa o Command
                 * 3) Recupera o ResultSet
                 * 4) Fill/Store o ResultSet no DataTable
                 * 5) Fecha a Connection
                 */
                Console.WriteLine("Usando a Data Table");
                /*Não é requerida uma Connection Ativa e Aberta
                 * dt.Rows: Obtém a Collection de linhas que pertencem a esta Table
                 * DataRow: Representa uma linha de dados em uma DataTable
                 */
                foreach (DataRow row in dt.Rows)
                {
                    //Acesso usando como Chave o nome do Campo
                    Console.WriteLine(row["matricula"] + " - " + row["nome"]);
                    //Acesso usando o Índice da posição da coluna
                    //Console.WriteLine(row[1] + " - " + row[2]);
                }
                Console.WriteLine("-----------------------------------");

                //Usando DataSet
                DataSet ds = new DataSet();
                //a tabela professor será armazenada na posição 0 (zero) do DataSet
                da.Fill(ds, "professor");
                Console.WriteLine("Usando DataSet");
                //Tables: Obtem a collection de Tables contida em System.Data.DataSet
                //Acessando o DataTable do DataSet usando o nome da DataTable
                foreach (DataRow row in ds.Tables["professor"].Rows)
                {
                    //Acesso usando como Chave o nome do Campo
                    Console.WriteLine(row["matricula"] + " - " + row["nome"]);
                    //Acesso usando o Índice da posição da coluna
                    //Console.WriteLine(row[1] + " - " + row[2]);
                }

                //Acessando o DataTable do DataSet usando o índice da posição
                //da DataTable desejada
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    Console.WriteLine(row["matricula"] + ",  " + row["nome"] );
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }



        public void testaExecuteNonQuery()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO professor (matricula,nome) 
                                                VALUES('112233','Prof. Substituto')", con);
                int linhasAfetadas = cmd.ExecuteNonQuery();
                Console.WriteLine("Linhas INSERIDAS = " + linhasAfetadas);
                //Define o CommandText para atualizar. Vamos reutilizar o mesmo objeto command ao invez de criar um novo
                cmd.CommandText = "UPDATE professor set nome = 'Prof. Titular' WHERE matricula = '112233'";
                linhasAfetadas = cmd.ExecuteNonQuery();
                Console.WriteLine("Linhas ATUALIZADAS = " + linhasAfetadas);
                //Define o CommandText para deletar. Vamos reutilizar o mesmo objeto command ao invez de criar um novo
                cmd.CommandText = "DELETE FROM professor WHERE matricula='112233'";
                linhasAfetadas = cmd.ExecuteNonQuery();
                Console.WriteLine("Linhas DELETADAS = " + linhasAfetadas);
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }


        public void testaExecuteScalar()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                // Cria objeto SqlCommand
                SqlCommand cm = new SqlCommand("SELECT COUNT(idprofessor) FROM professor", con);
                // Executa a query SQL
                // Uma vez que o tipo de retorno de ExecuteScalar() é object,
                // estamos fazendo um type casting para o tipo int
                int totalProfs = (int)cm.ExecuteScalar();
                Console.WriteLine("Número de professores cadastrados :  " + totalProfs);
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }


        public void testaExecuteReader()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                // Cria objeto SqlCommand
                SqlCommand cm = new SqlCommand("select * from professor", con);
                // Executa a query SQL  
                SqlDataReader sDR = cm.ExecuteReader();
                while (sDR.Read())
                {
                    Console.WriteLine(sDR["idprofessor"] + ") " + sDR["matricula"] + " - " + sDR["nome"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }

        public void testaExecuteReaderIndex()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                // Cria objeto SqlCommand
                SqlCommand cm = new SqlCommand("select * from professor", con);
                // Executa a query SQL  
                SqlDataReader sDR = cm.ExecuteReader();
                //fecha a conexão
                con.Close();
                while (sDR.Read())
                {
                    Console.WriteLine(sDR[0] + ") " + sDR[1] + " - " + sDR[2]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }

        public void testaExecuteReaderMultiResultSets()
        {
            SqlConnection con = null;
            try
            {
                // Criando a conexão  
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConString);
                con.Open();
                // Cria objeto SqlCommand
                SqlCommand cm = new SqlCommand("SELECT * FROM professor; SELECT * FROM curso ", con);
                // Executa a query SQL  
                SqlDataReader sDR = cm.ExecuteReader();
                //Laço para percorrer o primeiro Result Set (de professores)
                Console.WriteLine("PRIMEIRO Result Set:");
                while (sDR.Read())
                {
                    Console.WriteLine(sDR[0] + ") " + sDR[1] + " - " + sDR[2]);
                }
                //Para recuperar o segundo Result Set do objeto DataReader usamos o metodo NextResult()
                //NextResult() retorna True se conseguir avançar para um novo Result Set
                if (sDR.NextResult())
                {
                    Console.WriteLine("\n\nSEGUNDO Result Set:");
                    //Laço para percorrer o segundo Result Set (de cursos)
                    while (sDR.Read())
                    {
                        Console.WriteLine(sDR[0] + ") " + sDR[1] + " - " + sDR[2] + " - " + sDR[3]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, algo deu errado.\n" + e);
            }
            finally
            {   // Fechando a conexão  
                con.Close();
            }
        }

    }
}



