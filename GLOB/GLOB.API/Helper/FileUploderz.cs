using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;

namespace GLOB.Common.API;
[ApiExplorerSettings(IgnoreApi = true)]
public class FileUploderz
{
  private readonly IWebHostEnvironment hostEnv;
    private readonly ModelStateDictionary ms;
    private string storageFiles = "/assets/ouz";
  public FileUploderz(
  IWebHostEnvironment hostingEnvironment,
  ModelStateDictionary ms
  )
  {
    hostEnv = hostingEnvironment;
        this.ms = ms;
    }
  // Async Methods Cannot have output parameters
  // Don't use this methods
  public async Task<string> UploadFile(IFormFile file, string fileName)
  {
    if (file == null || file.Length == 0){
      ms.AddModelError(fileName, "Invalid File");
      return "";
    } 

    // Specify the directory where you want to save the file
    //var uploadDirectory = "D:/Directory";
    var uploadDirectory = hostEnv.ContentRootPath + storageFiles;
    hostEnv.ContentRootPath.Print();

    // Create the directory if it doesn't exist
    if (!Directory.Exists(uploadDirectory))
      Directory.CreateDirectory(uploadDirectory);

    // Generate a unique file name (e.g., using a Guid)
    string extension = Path.GetExtension(file.FileName);
    string allowedExtensions = ".png .jpeg .webp .jpg";
    if (allowedExtensions.IndexOf(extension) == -1)
    {
      ms.AddModelError(fileName, $"Invalid File Type {file.FileName} Only {allowedExtensions} allowed");
      return "";
    }
    fileName += Guid.NewGuid().ToString() + extension;

    // Combine the directory and file name to get the full path
    var filePath = Path.Combine(uploadDirectory, fileName);

    // Save the file to the specified path
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }
    return this.storageFiles + "/" + fileName;
  }

  public async Task<string> UploadFileReflection(
    IFormFile file,
    string propertyName,
    object obj
  )
  {
    if (file == null || file.Length == 0){
      ms.AddModelError(propertyName, "Invalid File");
      return "";
    } 
    // Specify the directory where you want to save the file
    //var uploadDirectory = "D:/Directory";
    var uploadDirectory = hostEnv.ContentRootPath + storageFiles;
    hostEnv.ContentRootPath.Print();

    // Create the directory if it doesn't exist
    if (!Directory.Exists(uploadDirectory))
      Directory.CreateDirectory(uploadDirectory);

    // Generate a unique file name (e.g., using a Guid)
    string extension = Path.GetExtension(file.FileName);
    string allowedExtensions = ".png .jpeg .webp .jpg";
    if (allowedExtensions.IndexOf(extension) == -1)
    {
      ms.AddModelError(propertyName, $"Invalid File Type {file.FileName} Only {allowedExtensions} allowed");
    }
    PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
    propertyName += Guid.NewGuid().ToString() + extension;

    // Combine the directory and file name to get the full path
    var filePath = Path.Combine(uploadDirectory, propertyName);

    // Save the file to the specified path
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }
    // Assign Property Value using Reflection
    if (propertyInfo != null)
    {
      propertyInfo.SetValue(obj, this.storageFiles + "/" + propertyName);
    }
    return this.storageFiles + "/" + propertyName;
  }
}