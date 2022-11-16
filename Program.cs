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

            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;


            var insertSql = @$"INSERT INTO
                    [Category] 
                    VALUES(
                        @Id, 
                        @Title, 
                        @Url, 
                        @Summary, 
                        @Order, 
                        @Description, 
                        @Featured)"; // Nunca concatenar string, sempre optar pela passagem de valores por parâmetro

            using (var connection = new SqlConnection(connectionString)) //Método de conexão optimizado
            {

                var rowS = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });

                /*connection.Execute(insertSql, new{
                    category.Id, category.Title, category.Url, category.Summary, category.Order, category.Description,category.Featured
                }); // Para executar o insertSql*/

                //Módulo 2

                var categories = connection.Query<Category>("SELECT [Id], [Title] AS FROM [Category]");
                foreach (var item in categories)
                {
                    Console.WriteLine($"{item.Id} - {item.Title}");
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

        static void ListCategories (SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] AS FROM [Category]");
                foreach (var item in categories)
                {
                    Console.WriteLine($"{item.Id} - {item.Title}");
                }
        }

        static void CreateCategory (SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;


            var insertSql = @$"INSERT INTO
                    [Category] 
                    VALUES(
                        @Id, 
                        @Title, 
                        @Url, 
                        @Summary, 
                        @Order, 
                        @Description, 
                        @Featured)";
            {

                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });
                Console.WriteLine($"{rows} linhas inseridas");
        }
        }

        static void UpdateCategory (SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
            var rows = connection.Execute(updateQuery, new Object
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2021"
            });
        }
    }
}