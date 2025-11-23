
using System;
using System.Windows.Forms;

namespace Hamburgueria.UI
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent(); // Método gerado pelo designer do VS
        }

        // Método para abrir um formulário dentro do painel principal (MDI ou Panel)
        private void AbrirFormulario(Form form)
        {
            // Se estiver usando MDI (Multiple Document Interface)
            // form.MdiParent = this;
            // form.Show();

            // Se estiver usando um Panel (Single Document Interface)
            // Assumindo que você tem um Panel chamado pnlPrincipal
            pnlPrincipal.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlPrincipal.Controls.Add(form);
            form.Show();
        }

        // Lógica para o botão de Cadastro de Ingredientes
        private void btnIngredientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormIngrediente());
        }

        // Lógica para o botão de Cadastro de Produtos/Receitas
        private void btnReceitas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormReceita());
        }

        // Lógica para o botão de Registro de Saída
        private void btnSaida_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormSaida());
        }

        // Lógica para o botão de Sair
        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair do sistema?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // **ATENÇÃO:** Você deve criar os seguintes componentes no designer:
        // 1. Panel: pnlPrincipal (onde os formulários serão carregados)
        // 2. Button: btnIngredientes, btnReceitas, btnSaida, btnSair
        // E vincular os eventos Click no designer.
    }
}
