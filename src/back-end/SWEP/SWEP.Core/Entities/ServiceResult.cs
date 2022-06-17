using System;

namespace SWEP.Core.Entities
{
    /// <summary>
    /// Kết quả thực hiện service
    /// </summary>
    /// CreatedBy: PTHIEU 17.6.2022
    public class ServiceResult
    {
        #region Properties
        /// <summary>
        /// Kết quả thực hiện (trạng thái service): true - thành công, false - lỗi/thất bại
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Thông báo dành cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông báo dành cho lập trình viên
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// CreatedBy: PTHIEU 17.6.2022
        public ServiceResult()
        {
              
        }

        /// <summary>
        /// Hàm khởi tạo trong TH xảy ra exception
        /// </summary>
        /// <param name="e">Ngoại lệ</param>
        /// CreatedBy: PTHIEU (17/08/2021)
        public ServiceResult(Exception e)
        {
            this.IsSuccess = false;
            this.UserMsg = Properties.Resources.ErrorException;
            this.DevMsg = e.Message;
            this.Data = null;
            this.ErrorCode = Constants.ErrorCode.ErrorCodeException;
        }
        #endregion
    }
}
