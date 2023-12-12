
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class CustomTimeRangeAttribute : ValidationAttribute
    {
        private readonly int _startTime;
        private readonly int _endTime;

        public CustomTimeRangeAttribute(int startTime, int endTime)
        {
            _startTime = startTime;
            _endTime = endTime;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime)
            {
                DateTime dateTimeValue = (DateTime)value;

                int timeInMinutes = dateTimeValue.Hour * 60 + dateTimeValue.Minute;

                if (timeInMinutes >= _startTime && timeInMinutes <= _endTime)
                {
                    if ((dateTimeValue.Hour == 12 && dateTimeValue.Minute > 0) || (dateTimeValue.Hour == 13 && dateTimeValue.Minute < 30))
                    {
                        return new ValidationResult("12:00 - 13:30 arası boş olmalıdır.");
                    }

                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Geçerli bir saat giriniz.");
        }
    }
    public class AfterCurrentDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime)
            {
                DateTime selectedDate = (DateTime)value;

                if (selectedDate < DateTime.Now)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Geçerli bir tarih seçiniz.");
        }
    }
}