using System;
using Microsoft.Data.SqlClient;
using Dapper;
using acessoDadosC.NETDapperSQL.Models;

namespace acessoDadosC.NETDapperSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=tucci;Integrated Security=SSPI"; //String de conexão com o servidor do banco de dados
            //Microsoft.Data.SqlClient -> Pacote de acesso

            /*var connection = new SqlConnection(); //Como boa prática, após utilizar a conexão aberta, devemos fechar.
            connection.Open(); //Abrindo conexão
            //Insert
            //Update
            connection.Close(); //Fechando conexão*/

            using (var connection = new SqlConnection(connectionString)) //Método de conexão optimizado
            {

                
                //Módulo 2

                var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
                foreach(var category in categories)
                {
                    Console.WriteLine($"{category.Id} - {category.Title}");
                }

                /*Módulo 1 Console.WriteLine("Conectado..."); 
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT [Id], [Title] FROM [Category]";

                    var reader = command.ExecuteReader(); //Comando para executar o CommandText
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
                    }
                } */
            }
        }

        //Integrated Security = SSPI
    }
}