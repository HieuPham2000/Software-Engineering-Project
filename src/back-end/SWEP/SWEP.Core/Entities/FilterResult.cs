using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEP.Core.Entities
{
    /// <summary>
    /// Kết quả lọc/phân trang dữ liệu
    /// </summary>
    /// <typeparam name="MyEntity">Đối tượng thực thể</typeparam>
    /// CreatedBy: PTHIEU 17.6.2022
    public class FilterResult<MyEntity>
    {
        #region Properties

        /// <summary>
        /// Tổng số bản ghi trả về
        /// </summary>
        public int? TotalRecords { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int? TotalPages { get; set; }

        /// <summary>
        /// Danh sách bản ghi trả về
        /// </summary>
        public IEnumerable<MyEntity> Data { get; set; }

        #endregion
    }
}
