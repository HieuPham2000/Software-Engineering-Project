using Dapper;
using SWEP.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace SWEP.Infrastructure.Repositories
{
    /// <summary>
    /// Lớp cơ sở cho việc thao tác với CSDL (truy vấn, thêm, sửa, xóa... dữ liệu)
    /// </summary>
    /// <typeparam name="MyEntity">Lớp thực thể</typeparam>
    /// CreatedBy: PTHIEU (17/08/2021)
    public class BaseRepository<MyEntity>: IBaseRepository<MyEntity> where MyEntity: class
    {
        #region Fields

        /// <summary>
        /// Thông tin về kết nối
        /// </summary>
        string _connectionString = string.Empty;

        /// <summary>
        /// Tên lớp entity
        /// </summary>
        protected string TableName = string.Empty;

        /// <summary>
        /// Kết nối tới cơ sở dữ liệu
        /// </summary>
        protected IDbConnection DbConnection = null;

        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            // lấy ra thông tin kết nối:
            _connectionString = configuration.GetConnectionString("LocalConnectionString2");

            // Khởi tạo kết nối:
            DbConnection = new MySqlConnection(_connectionString);

            // Lấy ra tên lớp entity (tên bảng)
            TableName = typeof(MyEntity).Name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy ra danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách đối tượng thực thể (List object)</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public IEnumerable<MyEntity> GetAll()
        {
            return DbConnection.Query<MyEntity>(
                sql: $"Proc_Get{TableName}s",
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy thông tin đối tượng theo khóa chính (id)
        /// </summary>
        /// <param name="entityId">Khóa chính (id)</param>
        /// <returns>Đối tượng</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public MyEntity GetById(Guid entityId)
        {
            // Thiết lập tham số
            var parameters = new DynamicParameters();
            parameters.Add($"{TableName}Id", entityId);

            // Thực hiện truy vấn dữ liệu với Dapper
            return DbConnection.QueryFirstOrDefault<MyEntity>(
                sql: $"Proc_Get{TableName}ById", 
                param: parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Thêm đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thêm</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public int Insert(MyEntity entity)
        {
            return DbConnection.Execute(
                sql: $"Proc_Insert{TableName}", 
                param: entity, 
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Chỉnh sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật thông tin</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public int Update(MyEntity entity)
        {
            return DbConnection.Execute(
               sql: $"Proc_Update{TableName}",
               param: entity, 
               commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xóa đối tượng dựa theo khóa chính
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public int Delete(Guid entityId)
        {
            // Thiết lập tham số
            var parameters = new DynamicParameters();
            parameters.Add($"{TableName}Id", entityId);

            // Thực hiện câu lệnh sql
            return DbConnection.Execute(
                sql: $"Proc_Delete{TableName}ById", 
                param: parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách khóa chính</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// CreatedBy: PTHIEU (10/09/2021)
        public int DeleteMany(List<Guid> entityIds)
        {
            // Khai báo câu lệnh sql
            var sqlCommand = $"DELETE FROM {TableName} WHERE {TableName}Id in @EntityIds";
            // Thiết lập tham số
            var parameters = new DynamicParameters();
            parameters.Add("@EntityIds", entityIds);

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                // Tạo và sử dụng transaction
                using (var transaction = connection.BeginTransaction())
                {
                    var affectedRows = connection.Execute(
                        sql: sqlCommand,
                        param: parameters,
                        commandType: CommandType.Text,
                        transaction: transaction);

                    transaction.Commit();

                    return affectedRows;
                }

            }
        }

        /// <summary>
        /// Hàm kiểm tra trùng lặp dữ liệu
        /// </summary>
        /// <param name="propName">Tên thuộc tính (tương ứng với tên trường trong CSDL)</param>
        /// <param name="value">Giá trị muốn kiểm tra</param>
        /// <returns>true - giá trị bị trùng, false - giá trị không bị trùng</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public bool CheckDuplicate(string propName, object value)
        {
            // Khai báo câu lệnh sql
            var sqlCommand = $"SELECT {propName} FROM {TableName} WHERE {propName} = @{propName}";

            // Thiết lập tham số
            var parameters = new DynamicParameters();
            parameters.Add($"@{propName}", value);

            // Thực hiện truy vấn
            var result = DbConnection.QueryFirstOrDefault<object>(
                sql: sqlCommand, 
                param: parameters, 
                commandType: CommandType.Text);

            // Nếu có dữ liệu trả về (khác null) => giá trị prop bị trùng
            if(result != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Hàm kiểm tra trùng lặp dữ liệu trước khi update bản ghi
        /// </summary>
        /// <param name="propName">Tên trường dữ liệu</param>
        /// <param name="value">Giá trị cần kiểm tra</param>
        /// <param name="entityId">Đối tượng thực thể</param>
        /// <returns>true - giá trị bị trùng, false - giá trị không bị trùng</returns>
        /// CreatedBy: PTHIEU (17/08/2021)
        public bool CheckDuplicateBeforeUpdate(string propName, object value, MyEntity entity)
        {
            var entityId = typeof(MyEntity).GetProperty($"{TableName}Id").GetValue(entity);

            // Khai báo câu lệnh sql
            // Có thể dùng: AND hoặc &&, <> hoặc != 
            var sqlCommand = $"SELECT {propName} FROM {TableName} WHERE {TableName}Id <> @{TableName}Id AND {propName} = @{propName}";

            // Thiết lập tham số
            var parameters = new DynamicParameters();
            parameters.Add($"@{TableName}Id", entityId);
            parameters.Add($"@{propName}", value);

            // Thực hiện truy vấn
            var result = DbConnection.QueryFirstOrDefault<object>(
                sql: sqlCommand,
                param: parameters,
                commandType: CommandType.Text);


            // Nếu có dữ liệu trả về (khác null) => giá trị prop bị trùng
            if (result != null)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
