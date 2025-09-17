
//using GLOB.Domain.DTOs.Donor;
//using System.ComponentModel.DataAnnotations;

//namespace GLOB.Domain.Attributez.Validationsz
//{
//  public class MyCustomValidationAttribute : ValidationAttribute
//  {
//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//      var model = (DonorGSBDtoCreate)validationContext.ObjectInstance;
//      if (model.Field1 != model.Field2)
//      {
//        return new ValidationResult(ErrorMessage);
//      }
//      return ValidationResult.Success;
//    }
//  }
//}