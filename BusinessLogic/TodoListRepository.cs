using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToDoList.API.Model;

namespace ToDoList.API.BusinessLogic
{
    public class TodoListRepository : ITodoListRepository
    {
        #region Constructor
        private IConfiguration _config;

        public TodoListRepository(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region GetAll
        public ApiResult<List<TodoItem>> GetAll()
        {
            ApiResult<List<TodoItem>> result = new ApiResult<List<TodoItem>>()
            {
                Data = new List<TodoItem>()
            };

            using (SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                string sql = @"select id, name, date
                               from todolist";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader != null)
                            {
                                while (reader.Read())
                                {
                                    result.Data.Add(new TodoItem
                                    {
                                        Id = Convert.ToInt32(reader[0].ToString()),
                                        Name = Convert.ToString(reader[1].ToString().Trim()),
                                        DateTime = Convert.ToString(reader[2].ToString().Trim())
                                    });
                                }
                            }
                        }

                        result.StatusCode = (int)HttpStatusCode.OK;//200
                        result.Message = "SUCCESS";
                    }
                    catch (Exception e)
                    {
                        result.Message = $"Error Occured: {e.Message}";
                        result.StatusCode = (int)HttpStatusCode.BadRequest;

                    }

                }
            }

            return result;
        }
        #endregion

        #region GetById
        public ApiResult<TodoItem> GetById(int todoId)
        {
            ApiResult<TodoItem> result = new ApiResult<TodoItem>();

            using (SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                string sql = @"select id, name, date
                               from todolist
                               where id = @id";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = todoId;
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                while (reader.Read())
                                {
                                    result.Data = new TodoItem
                                    {
                                        Id = Convert.ToInt32(reader[0].ToString()),
                                        Name = Convert.ToString(reader[1].ToString().Trim()),
                                        DateTime = Convert.ToString(reader[2].ToString().Trim())
                                    };
                                }
                            }
                        }

                        result.StatusCode = (int)HttpStatusCode.OK;//200
                        result.Message = "SUCCESS";
                    }
                    catch (Exception e)
                    {
                        result.Message = $"Error Occured: {e.Message}";
                        result.StatusCode = (int)HttpStatusCode.BadRequest;

                    }

                }
            }

            return result;
        }
        #endregion

        #region Create
        public ApiResult<TodoItem> Create(TodoItem item)
        {
            ApiResult<TodoItem> result = new ApiResult<TodoItem>();

            using (SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                string sql = @"insert into todolist(name, date)
                               values
                               (@name, @date)";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = item.Name;
                    cmd.Parameters.Add("@date", System.Data.SqlDbType.Date).Value = DateTime.ParseExact(item.DateTime, "yyyyMMdd h:mm tt", CultureInfo.InvariantCulture);
                    //cmd.Parameters.Add("@time", System.Data.SqlDbType.Time).Value = DateTime.ParseExact(item.Time, "HH:mm:ss t", CultureInfo.InvariantCulture);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        result.StatusCode = (int)HttpStatusCode.OK;//200
                        result.Message = "SUCCESS";
                    }
                    catch (Exception e)
                    {
                        result.Message = $"Error Occured: {e.Message}";
                        result.StatusCode = (int)HttpStatusCode.BadRequest;

                    }

                }
            }
            return result;
        }
        #endregion

        #region Update
        public ApiResult<TodoItem> Update(TodoItem item)
        {
            ApiResult<TodoItem> result = new ApiResult<TodoItem>();

            using (SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                string sql = @"update todolist
                               set name = @name,
                               date = @date,
                               time = @time
                               where id = @id";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = item.Id;
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.Int).Value = item.Name;
                    cmd.Parameters.Add("@date", System.Data.SqlDbType.Int).Value = item.DateTime;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        result.StatusCode = (int)HttpStatusCode.OK;//200
                        result.Message = "SUCCESS";
                    }
                    catch (Exception e)
                    {
                        result.Message = $"Error Occured: {e.Message}";
                        result.StatusCode = (int)HttpStatusCode.BadRequest;

                    }

                }
            }
            return result;
        }
        #endregion

        #region Delete
        public ApiResult<TodoItem> Delete(TodoItem item)
        {
            ApiResult<TodoItem> result = new ApiResult<TodoItem>();

            using (SqlConnection conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]))
            {
                string sql = @"delete from todolist where id = @id";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = item.Id;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        result.StatusCode = (int)HttpStatusCode.OK;//200
                        result.Message = "SUCCESS";
                    }
                    catch (Exception e)
                    {
                        result.Message = $"Error Occured: {e.Message}";
                        result.StatusCode = (int)HttpStatusCode.BadRequest;

                    }

                }
            }
            return result;
        }
        #endregion
    }
}
