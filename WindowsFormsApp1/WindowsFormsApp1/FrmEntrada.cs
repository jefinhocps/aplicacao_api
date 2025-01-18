using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class FrmEntrada : Form
    {

        string connectionString = "Server=localhost;Database=prod;Uid=root;Pwd=;";

        // Carregar os dados
        private void CarregarDados()
        {
            try
            {
                Conexao conexao = new Conexao("localhost", "prod", "root", "");

                string consultaSQL = "select * from produtos";

                // Executa a consulta
                MySqlDataReader leitor = conexao.ExecutarConsulta(consultaSQL);

                // Preenche o DataGrid
                DataTable dt = new DataTable();

                dt.Load(leitor);
                dataGridView1.DataSource = dt;

                leitor.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
            }
        }

        // Método para adicionar um novo produto
        private void AdicionarProduto()
        {
            try
            {
                // Converter os valores de peso e custo para decimal
                string produto = txtProduto.Text;
                decimal peso = Convert.ToDecimal(txtPeso.Text);
                decimal custo = Convert.ToDecimal(txtCusto.Text);

                // Adiciona 30% ao valor do produto de custo
                decimal valorVenda = custo * 1.35m;
                int lucro = 33;

                string query = $"INSERT INTO produtos (produto, peso, custo, lucro, preco_venda) VALUES ('{produto}',{peso},'{custo}','{lucro}','{valorVenda}')";

                Conexao conexao = new Conexao("localhost", "prod", "root", "");
                conexao.ExecutarComando(query);

                MessageBox.Show("Produto adicionado com sucesso!");
                CarregarDados();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar produto: " + ex.Message);
            }
        }

        // Método para carregar dados na DataGridView
        private void CarregarDadosComProgresso()
        {
            try
            {
                // Inicializa o BackgroundWorker para carregar os dados
                backgroundWorker1.RunWorkerAsync(); // Inicia o processamento assíncrono
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        // Método que será executado no BackgroundWorker
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                // Conectar ao banco de dados e obter os dados
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Consulta SQL para carregar os dados
                    string query = "SELECT * FROM PRODUTOS";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt); // Carrega todos os dados da tabela

                    // Simular um processo de carregamento, atualizando a barra de progresso
                    for (int i = 0; i < 100; i++)
                    {
                        System.Threading.Thread.Sleep(20); // Simula o processamento
                        backgroundWorker1.ReportProgress(i); // Relata o progresso
                    }

                    // Passa a DataTable como resultado final
                    e.Result = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro durante o processamento: " + ex.Message);
            }
        }

        // Método para excluir um registro
        private void DeletarProduto()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int idProduto = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                    // Confirmação antes de excluir
                    var confirmResult = MessageBox.Show("Tem certeza que deseja excluir este produto? A Operação não pode ser desfeita.", "Confirmar Exclusão", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes) 
                    {
                        using (MySqlConnection con = new MySqlConnection(connectionString))
                        {
                            con.Open();

                            // Consulta SQL para deletar o produto
                            string query = "DELETE FROM PRODUTOS WHERE id = @id";

                            // Cria o comando SQL
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@id", idProduto);

                            // Executa o comando
                            cmd.ExecuteNonQuery();

                            // Exibe mensagem de exclusão
                            MessageBox.Show("Produto excluído com sucesso.");

                            // Atualiza o grid
                            CarregarDados();
                        }
                    } else
                    {
                        MessageBox.Show("Selecione um produto para exclusão.");
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Produto: " + ex.Message);
            }
        }

        // Evento que é disparado quando a barra de progresso precisa ser atualizada
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage; // Atualiza o valor da ProgressBar
        }

        // Evento que é disparado quando o BackgroundWorker termina
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Atualiza o DataGridView com os dados carregados
            if (e.Result != null)
            {
                DataTable dt = (DataTable)e.Result;
                dataGridView1.DataSource = dt;
            }

            // Finaliza a barra de progresso
            progressBar1.Value = 100;
            MessageBox.Show("Dados carregados com sucesso!");
        }

        public FrmEntrada()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmEntrada_Load(object sender, EventArgs e)
        {
            // Inicialize o BackgroundWorker para permitir que o progresso seja reportado
            backgroundWorker1.WorkerReportsProgress = true;
            CarregarDados();
            CarregarDadosComProgresso();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            AdicionarProduto();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void backgroundWorker1_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            // Atualiza o valor da ProgressBar
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            // Atualiza o DataGridView com os dados carregados
            if (e.Result != null)
            {
                DataTable dt = (DataTable)e.Result;
                dataGridView1.DataSource = dt;
            }

            // Finaliza a barra de progresso
            progressBar1.Value = 100;
            MessageBox.Show("Dados carregados com sucesso!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeletarProduto();
        }
    }
}
