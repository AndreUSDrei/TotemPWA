using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TotemPWA.ViewModels
{
    public class PedidoViewModel
    {
        public PedidoViewModel()
        {
            NomeCliente = string.Empty;
            ItensSelecionados = new List<int>();
            MetodoPagamento = string.Empty;
        }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Selecione pelo menos um item.")]
        public List<int> ItensSelecionados { get; set; }

        [Required(ErrorMessage = "Informe o método de pagamento.")]
        public string MetodoPagamento { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
} 