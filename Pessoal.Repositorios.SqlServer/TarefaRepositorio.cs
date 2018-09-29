using Pessoal.Dominio;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Pessoal.Repositorios.SqlServer
{
    public class TarefaRepositorio
    {
        private string stringConexao;

        public TarefaRepositorio()
        {
            stringConexao =
            ConfigurationManager.ConnectionStrings["pessoalConnectionString"].ConnectionString;
        }

        public TarefaRepositorio(string stringConexao)
        {
            this.stringConexao = stringConexao;
        }

        public int Inserir(Tarefa tarefa)
        {
            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TarefaInserir", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddRange(Mapear(tarefa).ToArray());

                    return (int)comando.ExecuteScalar();
                }

                //conexao.Close();
            }
        }

        public void Atualizar(Tarefa tarefa)
        {
            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TarefaAtualizar", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddRange(Mapear(tarefa).ToArray());

                    comando.ExecuteNonQuery();
                }

                //conexao.Close();
            }
        }

        public List<Tarefa> Selecionar()
        {
            var tarefas = new List<Tarefa>();

            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TarefaSelecionar", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var registro = comando.ExecuteReader())
                    {
                        while (registro.Read())
                        {
                            tarefas.Add(Mapear(registro));
                        }
                    }                    
                }

                //conexao.Close();
            }

            return tarefas;
        }

        public Tarefa Selecionar(int id)
        {
            Tarefa tarefa = null;

            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TarefaSelecionar", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id", id);

                    using (var registro = comando.ExecuteReader())
                    {
                        if (registro.Read())
                        {
                            tarefa = Mapear(registro);
                        }
                    }
                }

                //conexao.Close();
            }

            return tarefa;
        }

        public void Excluir(int id)
        {
            using (var conexao = new SqlConnection(stringConexao))
            {
                conexao.Open();

                using (var comando = new SqlCommand("TarefaExcluir", conexao))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@id", id);

                    comando.ExecuteNonQuery();
                }

                //conexao.Close();
            }
        }

        private Tarefa Mapear(SqlDataReader registro)
        {
            var tarefa = new Tarefa();

            tarefa.Concluida = Convert.ToBoolean(registro["Concluida"]);
            tarefa.Id = Convert.ToInt32(registro["Id"]);
            tarefa.Nome = registro["Nome"].ToString();
            //tarefa.Observacoes = registro["Observacoes"].ToString();
            //tarefa.Observacoes = registro["Observacoes"]?.ToString();
            tarefa.Observacoes = Convert.ToString(registro["Observacoes"]);
            tarefa.Prioridade = (Prioridade)registro["Prioridade"];

            return tarefa;
        }

        private List<SqlParameter> Mapear(Tarefa tarefa)
        {
            var parametros = new List<SqlParameter>();

            if (tarefa.Id > 0)
            {
                parametros.Add(new SqlParameter("@Id", tarefa.Id));
            }

            parametros.Add(new SqlParameter("@Nome", tarefa.Nome));
            parametros.Add(new SqlParameter("prioridade", tarefa.Prioridade));
            parametros.Add(new SqlParameter("@concluida", tarefa.Concluida));
            parametros.Add(new SqlParameter("@Observacoes", tarefa.Observacoes));

            return parametros;
        }
    }
}
