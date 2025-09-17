using Newtonsoft.Json;
using GLOB.Domain.Enumz;

namespace GLOB.Domain.DTOs.Donor;
public class DonorGSBDto : DonorBasicDto
{
  public string? Area { get; set; }
  public string? NearBy { get; set; }
  public int SubUnitId { get; set; }
  public YesNo? WillingToJoinDI { get; set; }
  public int? DepartmentId { get; set; }
  public int? BranchId { get; set; }
  public YesNo Followup { get; set; }
  public int? FollowupDate { get; set; }
  public DonationOption DonationOption { get; set; }
  [JsonIgnore]
  public string? DonationOptionData { get; set; }
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public List<DonorGSBOptionMarhoom>? donorGSBOptionMarhooms
  {
    get
    {
      if (!string.IsNullOrEmpty(DonationOptionData))
      {
        if (DonationOptionData.IndexOf("name") != -1)
        {
          return JsonConvert.DeserializeObject
            <List<DonorGSBOptionMarhoom>>(DonationOptionData);
        }

      }
      return null;

    }
    set
    {
      if (value != null) DonationOptionData = JsonConvert.SerializeObject(value);
    }
  }
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public int? donorGSBOptionSelf
  {
    get
    {
      int.TryParse(DonationOptionData, out int result);
      if (result == 0) { return null; }
      return result;
    }
    set
    {
      if (value != null) DonationOptionData = JsonConvert.SerializeObject(value);
    }
  }
  public virtual DtoSelect? SU { get; set; }
  public virtual DtoSelect? Department { get; set; }
  public virtual DtoSelect? Branch { get; set; }
}
public class DonorGSBDtoCreate : DonorBasicDtoCreate
{
  public string? Area { get; set; }
  public string? NearBy { get; set; }
  public int SubUnitId { get; set; }
  public YesNo WillingToJoinDI { get; set; }
  public int? DepartmentId { get; set; }
  public int? BranchId { get; set; }
  public YesNo Followup { get; set; }
  public int? FollowupDate { get; set; }
  public DonationOption DonationOption { get; set; }
  [JsonIgnore]
  public string? DonationOptionData { get; set; }
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public List<DonorGSBOptionMarhoom>? donorGSBOptionMarhooms
  {
    get
    {
      if (!string.IsNullOrEmpty(DonationOptionData))
      {
        if (DonationOptionData.IndexOf("name") != -1)
        {
          return JsonConvert.DeserializeObject
            <List<DonorGSBOptionMarhoom>>(DonationOptionData);
        }

      }
      return null;

    }
    set
    {
      if (value != null) DonationOptionData = JsonConvert.SerializeObject(value);
    }
  }
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public int? donorGSBOptionSelf
  {
    get
    {
      int.TryParse(DonationOptionData, out int result);
      if (result == 0) { return null; }
      return result;
    }
    set
    {
      if (value != null) DonationOptionData = JsonConvert.SerializeObject(value);
    }
  }
}
public class DonorGSBDtoSearch : DonorBasicDtoSearch
{
  public string? Area { get; set; }
  public string? NearBy { get; set; }
  public int? SubUnitId { get; set; }
  public YesNo? WillingToJoinDI { get; set; }
  public int? DepartmentId { get; set; }
  public int? BranchId { get; set; }
  public YesNo? Followup { get; set; }
  public int? FollowupDate { get; set; }
  public DonationOption? DonationOption { get; set; }
  [JsonIgnore]
  public string? DonationOptionData { get; set; }
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public List<DonorGSBOptionMarhoom>? donorGSBOptionMarhooms
  {
    get
    {
      if (!string.IsNullOrEmpty(DonationOptionData))
      {
        if (DonationOptionData.IndexOf("name") != -1)
        {
          return JsonConvert.DeserializeObject
            <List<DonorGSBOptionMarhoom>>(DonationOptionData);
        }

      }
      return null;

    }
    set
    {
      if (value != null) DonationOptionData = JsonConvert.SerializeObject(value);
    }
  }
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public int? donorGSBOptionSelf
  {
    get
    {
      int.TryParse(DonationOptionData, out int result);
      if (result == 0) { return null; }
      return result;
    }
    set
    {
      if (value != null) DonationOptionData = JsonConvert.SerializeObject(value);
    }
  }
}