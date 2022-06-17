using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEP.Core.Entities
{
    /// <summary>
    /// Lớp cơ sở cho các thực thể
    /// </summary>
    /// Created by: PTHIEU 17.6.2022
    public class BaseEntity
    {
        #region Properties

        /// <summary>
        /// Thời điểm tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Thời điểm cập nhật
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người cập nhật
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
