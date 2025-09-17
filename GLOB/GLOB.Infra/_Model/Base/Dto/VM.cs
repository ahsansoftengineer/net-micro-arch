using System.Net;

namespace GLOB.Infra.Model.Base;
public class VMPaginate<T> 
{
  public List<T> Records { get; set; }
  public int PageNo { get; set; } = 1;
  public int PageSize { get; set; } = 10;
  public int Count { get; set; } = 0;
  public int TotalPages => (int)Math.Ceiling(Count / (double)PageSize);

  public bool Back => PageNo > 1 && TotalPages >= PageNo - 1;
  public bool Next => PageNo < TotalPages;
  public HttpStatusCode Status = HttpStatusCode.OK;

  public VMPaginate()
  {
    
  }

  public VMPaginate(DtoPage data)
  {
    PageNo = data.PageNo;
    PageSize = data.PageSize;
  }
  public VMPaginate(VMPaginate<T> data)
  {

    Records = data.Records;
    Count = data.Count;
    PageSize = data.PageSize;
    PageNo = PageNo;
  }
}

