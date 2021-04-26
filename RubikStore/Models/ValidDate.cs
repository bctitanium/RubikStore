using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RubikStore.Models
{
    public class ValidDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            DateTime date1 = new DateTime(1753, 1, 1);
            DateTime date2 = new DateTime(9999, 12, 31);

            var isValid = DateTime.TryParseExact(Convert.ToString(value), "dd/M/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime);

            return isValid && dateTime > DateTime.Now;
        }
    }
}