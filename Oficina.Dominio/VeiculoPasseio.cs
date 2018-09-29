using System;
using System.Collections.Generic;

namespace Oficina.Dominio
{
    //ToDo: OO - classe ou abstração.
    //ToDo: OO : Veiculo - herança.
    public class VeiculoPasseio : Veiculo
    {
        public TipoCarroceria Carroceria { get; set; }

        //ToDo: OO - polimorfismo por substituição.
        public override List<string> Validar()
        {
            var erros = base.ValidarBase();

            if (!Enum.IsDefined(typeof(TipoCarroceria), Carroceria))
            {
                erros.Add($"A Carroceria informada ({Carroceria}) não é válida.");
            }

            return erros;
        }
    }
}
