﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEP.Core.Constants
{
    /// <summary>
    /// Chứa các mã lỗi
    /// </summary>
    /// Created by: PTHIEU 17.6.2022
    public static class ErrorCode
    {
        /// <summary>
        /// Mã lỗi khi xảy ra exception
        /// </summary>
        public const string ErrorCodeException = "ERR_001";

        /// <summary>
        /// Mã lỗi khi validate trường bắt buộc thất bại
        /// </summary>
        public const string ErrorCodeValidateRequired = "ERR_002";

        /// <summary>
        /// Mã lỗi khi validate trường duy nhất thất bại
        /// </summary>
        public const string ErrorCodeValidateUnique = "ERR_003";

        /// <summary>
        /// Mã lỗi khi validate định dạng thất bại
        /// </summary>
        public const string ErrorCodeValidateFormat = "ERR_004";

        /// <summary>
        /// Mã lỗi khi validate độ dài tối đa thất bại
        /// </summary>
        public const string ErrorCodeValidateMaxLength = "ERR_005";

        /// <summary>
        /// Mã lỗi khi request không hợp lệ
        /// </summary>
        public const string ErrorCodeBadRequest = "ERR_006";

        /// <summary>
        /// Mã lỗi khi param gửi theo request không hợp lệ
        /// </summary>
        public const string ErrorCodeRequestParam = "ERR_007";

        /// <summary>
        /// Mã lỗi khi validate nghiệp vụ riêng thất bại
        /// </summary>
        public const string ErrorCodeValidateCustom = "ERR_008";
    }
}
