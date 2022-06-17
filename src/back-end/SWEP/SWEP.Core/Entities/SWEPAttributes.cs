using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEP.Core.Entities
{
    /// <summary>
    /// Thuộc tính xác định tên hiển thị của Property
    /// </summary>
    /// CreatedBy: PTHIEU 17.6.2022
    [AttributeUsage(AttributeTargets.Property)]
    public class MyDisplayName : Attribute
    {
        public string DisplayName = string.Empty;

        public MyDisplayName(string displayName)
        {
            DisplayName = displayName;
        }
    }

    /// <summary>
    /// Thuộc tính xác định Property là trường bắt buộc (không được để trống)
    /// </summary>
    /// CreatedBy: PTHIEU (17/08/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class MyRequired : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính xác định Property là trường duy nhất (không được phép trùng)
    /// </summary>
    /// CreatedBy: PTHIEU (17/08/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class MyUnique : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính xác định Property là trường email
    /// </summary>
    /// CreatedBy: PTHIEU (17/08/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class MyEmail : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính xác định Property là trường số điện thoại
    /// </summary>
    /// CreatedBy: PTHIEU (17/08/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class MyPhoneNumber : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính xác định Property là trường số (chỉ chấp nhận chữ số)
    /// </summary>
    /// CreatedBy: PTHIEU (17/08/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class MyNumber : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính xác định độ dài tối đa
    /// </summary>
    /// CreatedBy: PTHIEU (17/08/2021)
    [AttributeUsage(AttributeTargets.Property)]
    public class MyMaxLength : Attribute
    {
        public int MaxLength = 0;

        public MyMaxLength(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}
