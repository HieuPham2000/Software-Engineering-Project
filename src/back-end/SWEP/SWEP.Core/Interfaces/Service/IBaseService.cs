using SWEP.Core.Entities;
using System;
using System.Collections.Generic;

namespace SWEP.Core.Interfaces.Service
{
    /// <summary>
    /// Interface quy định các service xử lý nghiệp vụ khi thao tác dữ liệu
    /// </summary>
    /// <typeparam name="MyEntity">Lớp thực thể</typeparam>
    /// CreatedBy: PTHIEU 17.6.2022
    public interface IBaseService<MyEntity> where MyEntity : class
    {
        /// <summary>
        /// Service xử lý khi lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        ServiceResult GetAll();

        /// <summary>
        /// Service xử lý khi lấy dữ liệu theo Khóa chính (id)
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        ServiceResult GetById(Guid entityId);

        /// <summary>
        /// Service xử lý khi thêm dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng muốn thêm</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022 
        ServiceResult Insert(MyEntity entity);

        /// <summary>
        /// Service xử lý khi cập nhật/chỉnh sửa dữ liệu
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <param name="entity">Đối tượng muốn cập nhật</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        ServiceResult Update(Guid entityId, MyEntity entity);

        /// <summary>
        /// Service xử lý khi xóa dữ liệu theo Khóa chính (id)
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        ServiceResult Delete(Guid entityId);

        /// <summary>
        /// Service xử lý khi xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách khóa chính</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU (10/09/2021) 
        ServiceResult DeleteMany(List<Guid> entityIds);
    }
}
