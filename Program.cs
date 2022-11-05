using System;
using Microsoft.Data.SqlClient;

namespace acessoDadosC.NETDapperSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "localhost,1433;Database=tucci;User ID=sa;Password=1q2w3e4r@#$"; //String de conexão com o servidor do banco de dados
            //Microsoft.Data.SqlClient -> Pacote de acesso
            Console.WriteLine("Hello World!");
        }

        //Integrated Security = SSPI
    }
}