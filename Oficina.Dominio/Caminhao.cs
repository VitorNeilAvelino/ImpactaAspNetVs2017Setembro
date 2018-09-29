using System.Collections.Generic;

namespace Oficina.Dominio
{
    public class Caminhao : Veiculo
    {
        public QuantidadeEixo QuantidadeEixo { get; set; }

        public override List<string> Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
