using System;
using Microsoft.Data.SqlClient;
using Dapper;
using acessoDadosC.NETDapperSQL.Models;
using System.Data;

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

            /*var category = new Category();
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
                        @Featured)"; // Nunca concatenar string, sempre optar pela passagem de valores por parâmetro */

            using (var connection = new SqlConnection(connectionString)) //Método de conexão optimizado
            {
                //UpdateCategory(connection);
                //CreateManyCategory(connection);
                //ListCategories(connection);
                //ExecuteProcedure(connection);
                //CreateCategory(connection);
                //ExecuteReadProcedure(connection);
                //ExecuteScalar(connection);
                //ReadView(connection);
                //OneToOne(connection);
                //OneToMany(connection);
                //QueryMultiple(connection);
                //Like(connection);
                Transactions(connection);

                /*var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });*/

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

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] AS FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreateCategory(SqlConnection connection)
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

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2021"
            });

            Console.WriteLine($"{rows} registros atualizados");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria nova";
            category2.Url = "Categoria nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

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

                var rows = connection.Execute(insertSql, new[]{
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured
                }
            });
                Console.WriteLine($"{rows} linhas inseridas");
            }
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "spDeleteStudent";
            var pars = new { StudentId = "6bd552ea-7187-4bae-abb6-54e8f8b9f530" };

            var affectedRows = connection.Execute(procedure, pars, commandType: CommandType.StoredProcedure);

            Console.WriteLine($"{affectedRows} linhas afetadas");
        }
        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "spGetCoursesByCategory";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };

            var courses = connection.Query(procedure, pars, commandType: CommandType.StoredProcedure);

            foreach (var item in courses)
            {
                Console.WriteLine(item.Title);

            }
        }

        static void ExecuteScalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;


            var insertSql = @"
            INSERT INTO
                    [Category] 
                    OUTPUT inserted.[Id]
                    VALUES(
                        NEWID(), 
                        @Title, 
                        @Url, 
                        @Summary, 
                        @Order, 
                        @Description, 
                        @Featured) 
                        SELECT SCOPE_IDENTITY()";
            {

                var id = connection.ExecuteScalar<Guid>(insertSql, new
                {
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });
                Console.WriteLine($"A categoria inserida foi: {id} linhas.");
            }
        }

        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM [vwCourses]";
            var courses = connection.Query(sql);
            foreach (var item in courses)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"
            SELECT
                *
            FROM
                [CareerItem]
                INNER JOIN
                    [Course] ON [CareerItem].[CourseId] = [Course].[Id]";

            var items = connection.Query<CareerItem, Courses, CareerItem>(
                sql,
            (careerItem, courses) =>
            {
                careerItem.Courses = courses;
                return careerItem;
            }, splitOn: "[Id]");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Title} - Curso: {item.Courses.Title}");
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = @"
                            SELECT
                    [Career].[Id,
                    [Career].[Title],
                    [Career].[CareerId] AS [Id],
                    [CareerItem].[Title]
                FROM
                    [Career]
                INNER JOIN
                    [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                ORDER BY
                    [Career].[Title]";

            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(
                sql,
            (career, item) =>
            {
                var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                if (car == null)
                {
                    car = career;
                    car.Items.Add(item);
                    careers.Add(car);
                }
                else
                {
                    car.Items.Add(item);
                }
                return career;
            }, splitOn: "[CareerId]");

            foreach (var career in careers)
            {
                Console.WriteLine($"{career.Title}");
                foreach (var item in career.Items)
                {
                    Console.WriteLine($" - {item.Title}");
                }
            }
        }

        static void QueryMultiple(SqlConnection connection)
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

            using (var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Courses>();

                foreach (var item in categories)
                {
                    Console.WriteLine(item.Title);
                }
                foreach (var item in courses)
                {
                    Console.WriteLine(item.Title);
                }
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            var query = @"
                        SELECT TOP 10 * 
                        FROM
                            Career
                        WHERE
                            [Id] IN @[Id]";
            var items = connection.Query<Career>(query, new
            {
                Id = new[]{
                    "4327ac7e-963b-4893-9f31-9a3b28a4e72b",
                    "e6730d1c-6870-4df3-ae68-438624e04c72"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Like(SqlConnection connection)
        {
            var query = @"
                        SELECT * FROM
                            [Courses]
                        WHERE
                            [Title] LIKE @exp";
            var items = connection.Query<Courses>(query, new
            {
                exp = "%backend"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Transactions(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Minha categoria que não";
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
            using (var transaction = connection.BeginTransaction())
            {
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
                    }, transaction);

                    // transaction.Commit();
                    transaction.Rollback();
                    Console.WriteLine($"{rows} linhas inseridas");
                }
            }
        }
    }
}