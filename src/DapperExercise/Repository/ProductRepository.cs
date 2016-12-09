using Dapper;
using DapperExercise.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LogEntry;

namespace DapperExercise.Repository
{
    public class ProductRepository
    {
        private string connectionString;
        public ProductRepository()
        {
           // Data Source = (local)\SQLExpress; Initial Catalog = DapperDemo; Integrated Security = True
            connectionString = @"Data Source = (local)\SQLExpress; Initial Catalog = DapperDemo; Integrated Security = True;";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Add(Product prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO Products (Name, Quantity, Price)"
                                + " VALUES(@Name, @Quantity, @Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, prod);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                using (var multipleResult = dbConnection.QueryMultiple("spGetProducts", commandType: CommandType.StoredProcedure))
                {
                    var product = multipleResult.Read<Product>();

                    //return IEnumerable<product>;
                    //return dbConnection.Query<Product>(product.ToString());
                    return product;
                }

                //return dbConnection.Query<Product>("SELECT * FROM Products");
            }
        }

        public Product GetByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                //    string sQuery = "SELECT * FROM Products"
                //                   + " WHERE ProductId = @Id";
                //    dbConnection.Open();
                //    return dbConnection.Query<Product>(sQuery, new { Id = id }).FirstOrDefault();
                //}
                //Product p = new Product();
                dbConnection.Open();
                //   

                using (var multipleResult = dbConnection.QueryMultiple("spGetProductById", new { Id = id }, commandType: CommandType.StoredProcedure))
                {
                    var product = multipleResult.Read<Product>().SingleOrDefault();

                    return product;
                }
               
                //try
                //{
                //    dbConnection.Open();
                //    var p = new DynamicParameters();
                //    p.Add("@ProductId", id);
                //    p.Add("@Name", dbType: DbType.String, direction: ParameterDirection.Output);
                //    p.Add("@Quantity", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //    p.Add("@Price", dbType: DbType.Decimal, direction: ParameterDirection.Output);

                //    dbConnection.Execute("spGetProductById", p, commandType: CommandType.StoredProcedure);
                //    Product p1 = new Product();
                //    p1.Name = p.Get<string>("@Name");
                //    p1.Quantity = p.Get<int>("@Quantity");
                //    p1.Price = p.Get<float>("@Price");
                //    return p1;
                //}
                //catch (Exception e)
                //{
                //    string errorMessage = LogEntry.ExceptionLog.CreateErrorMessage(e);
                //    LogEntry.ExceptionLog.LogFileWrite(errorMessage);

                //}
             
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM Products"
                             + " WHERE ProductId = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Product prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE Products SET Name = @Name,"
                               + " Quantity = @Quantity, Price= @Price"
                               + " WHERE ProductId = @ProductId";
                dbConnection.Open();
                dbConnection.Query(sQuery, prod);
            }
        }
    }
}
