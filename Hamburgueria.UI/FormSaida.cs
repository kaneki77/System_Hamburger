
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class FormSaida : Form
    {
        private readonly ProdutoService _produtoService;
        private readonly SaidaProdutoService _saidaService;
        private readonly UsuarioService _usuarioService; // Para simular o usuário logado

        // Construtor
        public FormSaida()
        {
            InitializeComponent(); // Método gerado pelo designer do VS
            _produtoService = new ProdutoService();
            _saidaService = new SaidaProdutoService();
            _usuarioService = new UsuarioService(); // Assumindo que você tem um serviço de usuário
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            try
            {
                // Carrega Combobox de Produtos
                List<Produto> produtos = _produtoService.BuscarTodos();
                // Assumindo que você tem um ComboBox chamado cmbProduto
                cmbProduto.DataSource = produtos;
                cmbProduto.DisplayMember = "Nome";
                cmbProduto.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarSaida_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduto.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um Produto para registrar a saída.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProduto = (int)cmbProduto.SelectedValue;
                // Assumindo que você tem um NumericUpDown chamado numQuantidadeSaida
                decimal quantidade = numQuantidadeSaida.Value;

                // ** SIMULAÇÃO DE USUÁRIO LOGADO **
                // Em um sistema real, o ID do usuário viria da sessão de login.
                // Aqui, vamos buscar um usuário de exemplo (ID 1 = Admin)
                Usuario usuarioLogado = _usuarioService.BuscarPorId(1); 
                if (usuarioLogado == null)
                {
                    throw new Exception("Usuário logado não encontrado. Verifique a tabela Usuario.");
                }

                SaidaProduto saida = new SaidaProduto
                {
                    IdProduto = idProduto,
                    QuantidadeSaida = quantidade,
                    IdUsuario = usuarioLogado.Id,
                    DataSaida = DateTime.Now
                };

                // A chamada a Adicionar() insere no banco e a TRIGGER faz a baixa de estoque
                _saidaService.Adicionar(saida);

                MessageBox.Show($"Saída de {quantidade} unidade(s) de {cmbProduto.Text} registrada com sucesso! O estoque de ingredientes foi atualizado automaticamente.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Limpa os campos após o registro
                numQuantidadeSaida.Value = 1;
                cmbProduto.SelectedIndex = -1;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Erro de Validação: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar saída: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // **ATENÇÃO:** Você deve criar os seguintes componentes no designer:
        // 1. ComboBox: cmbProduto (para selecionar o produto que saiu)
        // 2. NumericUpDown: numQuantidadeSaida (para a quantidade que saiu)
        // 3. Button: btnRegistrarSaida
        // E vincular o evento Click no designer.
    }
}
