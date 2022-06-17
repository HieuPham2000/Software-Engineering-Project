using SWEP.Core.Constants;
using SWEP.Core.Entities;
using SWEP.Core.Enums;
using SWEP.Core.Interfaces.Infrastructure;
using SWEP.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SWEP.Core.Services
{
    /// <summary>
    /// Lớp cơ sở cho việc xử lý nghiệp vụ
    /// </summary>
    /// <typeparam name="MyEntity">Lớp thực thể</typeparam>
    /// CreatedBy: PTHIEU 17.6.2022
    public class BaseService<MyEntity> : IBaseService<MyEntity> where MyEntity : class
    {
        #region Fields

        IBaseRepository<MyEntity> _baseRepository;

        protected ServiceResult ServiceResult;

        #endregion


        #region Constructors

        public BaseService(IBaseRepository<MyEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            ServiceResult = new ServiceResult();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Service xử lý khi lấy tất cả dữ liệu
        /// </summary>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        public ServiceResult GetAll()
        {
            ServiceResult.IsSuccess = true;
            // Thực hiện lấy dữ liệu 
            ServiceResult.Data = _baseRepository.GetAll();
            return ServiceResult;
        }

        /// <summary>
        /// Service xử lý khi lấy dữ liệu theo Khóa chính (id)
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        public ServiceResult GetById(Guid entityId)
        {
            ServiceResult.IsSuccess = true;
            // Thực hiện lấy dữ liệu
            ServiceResult.Data = _baseRepository.GetById(entityId);
            // Trường hợp dữ liệu trống
            if (ServiceResult.Data == null)
            {
                ServiceResult.UserMsg = Properties.Resources.Msg_GetByIdNoData;
            }
            return ServiceResult;
        }

        /// <summary>
        /// Service xử lý khi xóa dữ liệu theo Khóa chính (id)
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        public ServiceResult Delete(Guid entityId)
        {
            ServiceResult.IsSuccess = true;
            // Thực hiện xóa dữ liệu
            ServiceResult.Data = _baseRepository.Delete(entityId);
            // Trường hợp không có bản ghi nào bị xóa
            if (ServiceResult.Data == null || (int)ServiceResult.Data <= 0)
            {
                ServiceResult.UserMsg = Properties.Resources.Msg_DeleteNoData;
            }
            else
            {
                ServiceResult.UserMsg = Properties.Resources.Msg_DeleteSuccess;
            }
            return ServiceResult;
        }

        /// <summary>
        /// Service xử lý khi xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách khóa chính</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU (10/09/2021) 
        public ServiceResult DeleteMany(List<Guid> entityIds)
        {
            ServiceResult.IsSuccess = true;
            // Thực hiện xóa dữ liệu
            ServiceResult.Data = _baseRepository.DeleteMany(entityIds);
            // Trường hợp không có bản ghi nào bị xóa
            if (ServiceResult.Data == null || (int)ServiceResult.Data <= 0)
            {
                ServiceResult.UserMsg = Properties.Resources.Msg_DeleteManyFail;
            }
            else
            {
                ServiceResult.UserMsg = string.Format(Properties.Resources.Msg_DeleteManySuccess, entityIds.Count);
            }
            return ServiceResult;
        }

        /// <summary>
        /// Service xử lý khi thêm dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng muốn thêm</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        public ServiceResult Insert(MyEntity entity)
        {
            // Validate dữ liệu
            // Truyền cờ action Insert
            var isValid = ValidateData(entity, SWEPAction.Insert);

            // Validate thành công (dữ liệu hợp lệ)
            if (isValid)
            {
                ServiceResult.IsSuccess = true;
                // Thêm dữ liệu và lấy kết quả trả về (số bản ghi được thêm)
                ServiceResult.Data = _baseRepository.Insert(entity);
                if (ServiceResult.Data == null || (int)ServiceResult.Data <= 0)
                {
                    ServiceResult.UserMsg = Properties.Resources.Msg_InsertNoData;
                }
                else
                {
                    ServiceResult.UserMsg = Properties.Resources.Msg_InsertSuccess;
                }

                return ServiceResult;
            }

            // Validate thất bại (dữ liệu không hợp lệ)
            // Các thông tin như UserMsg đã được gán trong hàm validate
            return ServiceResult;
        }

        /// <summary>
        /// Service xử lý khi cập nhật/chỉnh sửa dữ liệu
        /// </summary>
        /// <param name="entityId">Khóa chính</param>
        /// <param name="entity">Đối tượng muốn cập nhật</param>
        /// <returns>Đối tượng ServiceResult chứa kết quả thực hiện</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        public ServiceResult Update(Guid entityId, MyEntity entity)
        {
            // Set id cho object entity bằng id truyền vào
            var propId = typeof(MyEntity).GetProperty($"{typeof(MyEntity).Name}Id");
            propId.SetValue(entity, entityId);

            // Validate dữ liệu
            // Truyền cờ action Update
            var isValid = ValidateData(entity, SWEPAction.Update);

            // Validate thành công (dữ liệu hợp lệ)
            if (isValid)
            {
                ServiceResult.IsSuccess = true;
                // Cập nhật dữ liệu và lấy kết quả trả về (số bản ghi bị ảnh hưởng)
                ServiceResult.Data = _baseRepository.Update(entity);
                if (ServiceResult.Data == null || (int)ServiceResult.Data <= 0)
                {
                    ServiceResult.UserMsg = Properties.Resources.Msg_UpdateNoData;
                }
                else
                {
                    ServiceResult.UserMsg = Properties.Resources.Msg_UpdateSuccess;
                }
                return ServiceResult;
            }

            // Validate thất bại (dữ liệu không hợp lệ)
            // Các thông tin như UserMsg đã được gán trong hàm validate
            return ServiceResult;
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng thực thể cần validate</param>
        /// <param name="action">Hành động (Insert/Update...)</param>
        /// <returns>true - validate thành công, false - validate thất bại</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        private bool ValidateData(MyEntity entity, SWEPAction action)
        {
            // Cờ xác định trạng thái hợp lệ/không hợp lệ của dữ liệu entity
            var isValid = true;

            // Validate chung

            // Lấy ra tất cả thuộc tính
            var properties = typeof(MyEntity).GetProperties();

            // Kiểm tra từng thuộc tính
            foreach (var prop in properties)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                var propValueStr = (propValue?.ToString() ?? string.Empty);
                var propType = prop.PropertyType;

                // Lấy ra tên hiển thị - nếu có (dùng trong câu thông báo lỗi với người dùng)
                var displayName = string.Empty;
                var hasDisplayName = prop.GetCustomAttributes(typeof(MyDisplayName), true);
                // Kiểm tra nếu prop có attribute MyDisplayName thì:
                if (hasDisplayName.Length > 0)
                {
                    // Lấy ra tên hiển thị
                    displayName = (hasDisplayName[0] as MyDisplayName).DisplayName;
                }

                // Kiểm tra trường bắt buộc
                if (prop.IsDefined(typeof(MyRequired)))
                {
                    if (propValue == null || (propType == typeof(string) && string.IsNullOrEmpty(propValueStr.Trim())))
                    {
                        ServiceResult.IsSuccess = false;
                        ServiceResult.UserMsg = string.Format(Properties.Resources.ErrorValidate_Required, displayName);
                        ServiceResult.ErrorCode = ErrorCode.ErrorCodeValidateRequired;
                        ServiceResult.Data = propName;
                        return false;
                    }
                }

                // Nếu giá trị null, trống thì không cần kiểm tra nữa
                // Chuyển sang kiểm tra prop tiếp theo
                if (propValue == null || (propType == typeof(string) && string.IsNullOrEmpty(propValueStr)))
                {
                    continue;
                }


                // Kiểm tra độ dài tối đa
                if (prop.IsDefined(typeof(MyMaxLength)))
                {
                    var maxLengthAttr = prop.GetCustomAttributes(typeof(MyMaxLength), true)[0];
                    var maxLength = (maxLengthAttr as MyMaxLength).MaxLength;

                    if (propValueStr.Trim().Length > maxLength)
                    {
                        ServiceResult.IsSuccess = false;
                        ServiceResult.UserMsg = string.Format(Properties.Resources.ErrorValidate_MaxLength, displayName, maxLength);
                        ServiceResult.ErrorCode = ErrorCode.ErrorCodeValidateMaxLength;
                        ServiceResult.Data = propName;
                        return false;
                    }
                }

                // Kiểm tra định dạng email
                if (prop.IsDefined(typeof(MyEmail)))
                {
                    var pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                + "@"
                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

                    if (Regex.IsMatch(propValueStr, pattern) == false)
                    {
                        ServiceResult.IsSuccess = false;
                        ServiceResult.UserMsg = string.Format(Properties.Resources.ErrorValidate_Format, displayName, propValue);
                        ServiceResult.ErrorCode = ErrorCode.ErrorCodeValidateFormat;
                        ServiceResult.Data = propName;
                        return false;
                    }
                }

                // Kiểm tra chỉ chứa số
                if (prop.IsDefined(typeof(MyNumber)))
                {
                    if (propValueStr.All(char.IsDigit))
                    {
                        ServiceResult.IsSuccess = false;
                        ServiceResult.UserMsg = string.Format(Properties.Resources.ErrorValidate_Format, displayName, propValue);
                        ServiceResult.ErrorCode = ErrorCode.ErrorCodeValidateFormat;
                        ServiceResult.Data = propName;
                        return false;
                    }
                }

                // Kiểm tra định dạng số điện thoại
                if (prop.IsDefined(typeof(MyPhoneNumber)))
                {
                    var pattern = @"^[+\d]?(?:[\d-.\s()]*)$";

                    if (Regex.IsMatch(propValueStr, pattern) == false)
                    {
                        ServiceResult.IsSuccess = false;
                        ServiceResult.UserMsg = string.Format(Properties.Resources.ErrorValidate_Format, displayName, propValue);
                        ServiceResult.ErrorCode = ErrorCode.ErrorCodeValidateFormat;
                        ServiceResult.Data = propName;
                        return false;
                    }
                }

                // Kiểm tra trường duy nhất
                if (prop.IsDefined(typeof(MyUnique)))
                {
                    // Cờ xác định trạng thái trùng lặp của giá trị prop
                    var isDuplicate = false;

                    // Dựa trên hành động (là sửa hay thêm mới?)
                    // gọi hàm kiểm tra tương ứng
                    switch (action)
                    {
                        case SWEPAction.Insert:
                            isDuplicate = _baseRepository.CheckDuplicate(propName, propValue);
                            break;
                        case SWEPAction.Update:
                            isDuplicate = _baseRepository.CheckDuplicateBeforeUpdate(propName, propValue, entity);
                            break;
                    }

                    // Nếu giá trị prop bị trùng
                    if (isDuplicate)
                    {
                        ServiceResult.IsSuccess = false;
                        ServiceResult.UserMsg = string.Format(Properties.Resources.ErrorValidate_Unique, displayName, propValue);
                        ServiceResult.ErrorCode = ErrorCode.ErrorCodeValidateUnique;
                        ServiceResult.Data = propName;
                        return false;
                    }
                }


            }

            // validate riêng
            isValid = ValidateCustom(entity);

            return isValid;
        }

        /// <summary>
        /// Hàm validate riêng
        /// Cho phép lớp kế thừa ghi đè, tự triển khai (nếu cần)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể cần validae</param>
        /// <returns>true - validate thành công, false - validate thất bại</returns>
        /// CreatedBy: PTHIEU 17.6.2022
        protected virtual bool ValidateCustom(MyEntity entity)
        {
            return true;
        }


        #endregion

    }
}
