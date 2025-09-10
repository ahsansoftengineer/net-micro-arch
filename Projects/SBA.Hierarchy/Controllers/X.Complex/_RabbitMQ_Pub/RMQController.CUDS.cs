using RabbitMQ.Client;

using GLOB.Infra.Utils.Attributez;
namespace SBA.Auth.Controllers;

public partial class __RabbitMQController 
{

  [HttpPost] [NoCache]
  public async Task<IActionResult> Createz([FromBody] ProjectzLookupDtoCreate dto)
  {
    // dto.Status = Status.Active;
    try
    {
      _rmqPub.Publish(dto, (channel, bytes) =>
      {
       channel.BasicPublish(
        exchange: "sba.direct",
        routingKey: "auth.lookup.create",
        basicProperties: null,
        body: bytes
      );
      });
      return dto.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      // return ex.Ok();
      return $"[Rabbit MQ] Error : {ex.Message}".ToExtVMSingle().Ok();
    }

  }
  [HttpPut("{Id}")] [NoCache]
  public async Task<IActionResult> Update(string Id, [FromBody] ProjectzLookupDtoCreate dto)
  {
    try
    {
      _rmqPub.Publish(dto, (channel, bytes) =>
      {
       channel.BasicPublish("sba.direct", "auth.lookup.update", null, bytes);
      });
      return dto.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      // return ex.Ok();
      return $"[Rabbit MQ] Error : {ex.Message}".ToExtVMSingle().Ok();
    }
    
  }

  [HttpDelete("{Id}")] [NoCache]
  public async Task<IActionResult> Delete(string Id)
  {
    try
    {
      _rmqPub.Publish(Id, (channel, bytes) =>
      {
       channel.BasicPublish("sba.direct", "auth.lookup.delete", null, bytes);
      });
      return $"Deleted Successfully".ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      // return ex.Ok();
      return $"[Rabbit MQ] Error : {ex.Message}".ToExtVMSingle().Ok();
    }
    
  }
  
  [HttpPatch("{Id}")] [NoCache]
  public async Task<IActionResult> UpdateStatus(string Id, [FromBody] DtoRequestStatus dto)
  {
    try
    {
      _rmqPub.Publish(dto, (channel, bytes) =>
      {
       channel.BasicPublish("sba.direct", "auth.lookup.status", null, bytes);
      });
      return dto.ToExtVMSingle().Ok();
    }
    catch (Exception ex)
    {
      // return ex.Ok();
      return $"[Rabbit MQ] Error : {ex.Message}".ToExtVMSingle().Ok();
    }
    
  }
}