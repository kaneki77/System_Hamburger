
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class FormIngrediente : Form
    {
        private readonly IngredienteService _ingredienteService;
        private Ingrediente _ingredienteSelecionado;

        // Construtor
        public FormIngrediente()
        {
            InitializeComponent(); // Método gerado pelo designer do VS
            _ingredienteService = new IngredienteService();
            CarregarIngredientes();
        }

        // Método para carregar os dados no DataGridView
        private void CarregarIngredientes()
        {
            try
            {
                List<Ingrediente> lista = _ingredienteService.BuscarTodos();
                // Assumindo que você tem um DataGridView chamado dgvIngredientes
                dgvIngredientes.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar ingredientes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lógica para o botão Salvar/Atualizar
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Assumindo que você tem TextBoxes e NumericUpDowns para os campos
                string nome = txtNome.Text;
                string unidade = txtUnidadeMedida.Text;
                decimal estoqueMinimo = numEstoqueMinimo.Value;

                if (_ingredienteSelecionado == null)
                {
                    // Novo Ingrediente (CREATE)
                    Ingrediente novo = new Ingrediente
                    {
                        Nome = nome,
                        UnidadeMedida = unidade,
                        EstoqueMinimo = estoqueMinimo
                    };
                    _ingredienteService.Adicionar(novo);
                    MessageBox.Show("Ingrediente adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Atualizar Ingrediente (UPDATE)
                    _ingredienteSelecionado.Nome = nome;
                    _ingredienteSelecionado.UnidadeMedida = unidade;
                    _ingredienteSelecionado.EstoqueMinimo = estoqueMinimo;
                    _ingredienteService.Atualizar(_ingredienteSelecionado);
                    MessageBox.Show("Ingrediente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparCampos();
                CarregarIngredientes();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Erro de Validação: " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lógica para selecionar um item no DataGridView para edição
        private void dgvIngredientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Recupera o objeto Ingrediente da linha selecionada
                _ingredienteSelecionado = dgvIngredientes.Rows[e.RowIndex].DataBoundItem as Ingrediente;

                if (_ingredienteSelecionado != null)
                {
                    // Preenche os campos para edição
                    txtNome.Text = _ingredienteSelecionado.Nome;
                    txtUnidadeMedida.Text = _ingredienteSelecionado.UnidadeMedida;
                    numEstoqueMinimo.Value = _ingredienteSelecionado.EstoqueMinimo;
                }
            }
        }

        // Lógica para o botão Novo/Limpar
        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        // Lógica para o botão Excluir
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_ingredienteSelecionado != null && MessageBox.Show($"Deseja realmente excluir {_ingredienteSelecionado.Nome}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _ingredienteService.Remover(_ingredienteSelecionado.Id);
                    MessageBox.Show("Ingrediente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarIngredientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimparCampos()
        {
            _ingredienteSelecionado = null;
            txtNome.Clear();
            txtUnidadeMedida.Clear();
            numEstoqueMinimo.Value = 0;
            txtNome.Focus();
        }

        // **ATENÇÃO:** Você deve criar os seguintes componentes no designer:
        // 1. DataGridView: dgvIngredientes
        // 2. TextBox: txtNome
        // 3. TextBox: txtUnidadeMedida
        // 4. NumericUpDown: numEstoqueMinimo
        // 5. Button: btnSalvar, btnNovo, btnExcluir
        // E vincular os eventos (Click e CellClick) no designer.
    }
}
