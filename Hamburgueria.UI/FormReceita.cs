
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class FormReceita : Form
    {
        private readonly ProdutoService _produtoService;
        private readonly IngredienteService _ingredienteService;
        private readonly ProdutoIngredienteService _piService;

        // Construtor
        public FormReceita()
        {
            InitializeComponent(); // Método gerado pelo designer do VS
            _produtoService = new ProdutoService();
            _ingredienteService = new IngredienteService();
            _piService = new ProdutoIngredienteService();
            CarregarCombos();
        }

        private void CarregarCombos()
        {
            try
            {
                // Carrega Combobox de Produtos
                List<Produto> produtos = _produtoService.BuscarTodos();
                // Assumindo que você tem um ComboBox chamado cmbProduto
                cmbProduto.DataSource = produtos;
                cmbProduto.DisplayMember = "Nome";
                cmbProduto.ValueMember = "Id";

                // Carrega Combobox de Ingredientes
                List<Ingrediente> ingredientes = _ingredienteService.BuscarTodos();
                // Assumindo que você tem um ComboBox chamado cmbIngrediente
                cmbIngrediente.DataSource = ingredientes;
                cmbIngrediente.DisplayMember = "Nome";
                cmbIngrediente.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduto.SelectedValue != null && cmbProduto.SelectedValue is int idProduto)
            {
                CarregarReceita(idProduto);
            }
        }

        private void CarregarReceita(int idProduto)
        {
            try
            {
                List<ProdutoIngrediente> receita = _piService.BuscarReceitaPorProduto(idProduto);
                // Assumindo que você tem um DataGridView chamado dgvReceita
                dgvReceita.DataSource = receita;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar receita: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionarIngrediente_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduto.SelectedValue == null || cmbIngrediente.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um Produto e um Ingrediente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProduto = (int)cmbProduto.SelectedValue;
                int idIngrediente = (int)cmbIngrediente.SelectedValue;
                // Assumindo que você tem um NumericUpDown chamado numQuantidade
                decimal quantidade = numQuantidade.Value;

                ProdutoIngrediente pi = new ProdutoIngrediente
                {
                    IdProduto = idProduto,
                    IdIngrediente = idIngrediente,
                    QuantidadeNecessaria = quantidade
                };

                _piService.AdicionarIngrediente(pi);
                MessageBox.Show("Ingrediente adicionado à receita com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarReceita(idProduto);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Erro de Validação: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar ingrediente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoverIngrediente_Click(object sender, EventArgs e)
        {
            if (dgvReceita.SelectedRows.Count > 0)
            {
                // Pega o item selecionado no DataGridView
                ProdutoIngrediente piSelecionado = dgvReceita.SelectedRows[0].DataBoundItem as ProdutoIngrediente;

                if (piSelecionado != null && MessageBox.Show($"Deseja remover {piSelecionado.NomeIngrediente} da receita?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _piService.RemoverIngrediente(piSelecionado.IdProduto, piSelecionado.IdIngrediente);
                        MessageBox.Show("Ingrediente removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarReceita(piSelecionado.IdProduto);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao remover ingrediente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // **ATENÇÃO:** Você deve criar os seguintes componentes no designer:
        // 1. ComboBox: cmbProduto (para selecionar o produto cuja receita será editada)
        // 2. ComboBox: cmbIngrediente (para selecionar o ingrediente a ser adicionado)
        // 3. NumericUpDown: numQuantidade (para a quantidade necessária)
        // 4. Button: btnAdicionarIngrediente, btnRemoverIngrediente
        // 5. DataGridView: dgvReceita (para exibir a receita atual)
        // E vincular os eventos (SelectedIndexChanged e Click) no designer.
    }
}
