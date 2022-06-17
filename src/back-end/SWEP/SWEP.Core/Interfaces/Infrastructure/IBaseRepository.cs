using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEP.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface quy định các thao tác lấy dữ liệu cơ bản
    /// </summary>
    /// <typeparam name="MyEntity">Lớp thực thể</typeparam>
    /// CreatedBy: PTHIEU 17.6.2022
    public interface IBaseRepository<MyEntity> where MyEntity: class
    {
        /// <summary>
        /// Lấy ra toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        IEnumerable<MyEntity> GetAll();

        /// <summary>
        /// Lấy ra đối tượng thực thể theo khóa chính(id)
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Đối tượng thực thể</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        MyEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm mới đối tượng thực thể
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Số bản ghi được thêm</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        int Insert(MyEntity entity);

        /// <summary>
        /// Chỉnh sửa đối tượng thực thể
        /// </summary>
        /// <param name="entity">Đối tượng cần chỉnh sửa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        int Update(MyEntity entity);

        /// <summary>
        /// Xóa đối tượng thực thể theo khóa chính(id)
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        int Delete(Guid entityId);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách khóa chính</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// CreatedBy: PTHIEU (10/09/2021)
        int DeleteMany(List<Guid> entityIds);

        /// <summary>
        /// Hàm kiểm tra trùng lặp dữ liệu
        /// </summary>
        /// <param name="propName">Tên thuộc tính (tương ứng với tên trường trong CSDL)</param>
        /// <param name="value">Giá trị muốn kiểm tra</param>
        /// <returns>true - giá trị bị trùng, false - giá trị không bị trùng</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        bool CheckDuplicate(string propName, object value);

        /// <summary>
        /// Hàm kiểm tra trùng lặp dữ liệu trước khi update bản ghi
        /// </summary>
        /// <param name="propName">Tên trường dữ liệu</param>
        /// <param name="value">Giá trị cần kiểm tra</param>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <returns>true - giá trị bị trùng, false - giá trị không bị trùng</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        bool CheckDuplicateBeforeUpdate(string propName, object value, MyEntity entity);
    }
}
