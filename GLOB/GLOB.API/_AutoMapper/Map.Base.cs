using AutoMapper;

namespace GLOB.API.Mapper;

public partial class API_Base_Mapper : Profile
{
  public API_Base_Mapper()
  {
    MapCommon();
    MapCustom();
  }
  // Used When The Base (Dtos and Entity)
  protected void MapDefaults<TEntity>()
    where TEntity : EntityBase
  {
    MapCRUD<TEntity, DtoCreate, DtoUpdate, DtoRead, DtoSearch, DtoSearch>();
  }

  // Three Param
  protected void MapCRUD<TEntity, TDtoCreateUpdateRead, TDtoSearch>()
  {
    MapCRUD<TEntity, TDtoCreateUpdateRead, TDtoSearch, DtoSelect>();
  }
  // Four Param
  protected void MapCRUD<TEntity, TDtoCreateUpdate, TDtoRead, TDtoSearch>()
  {
    MapCRUD<TEntity, TDtoCreateUpdate, TDtoCreateUpdate, TDtoRead, TDtoSearch, DtoSelect>();
  }
  // Six Param
  protected void MapCRUD<TEntity, TDtoCreate, TDtoUpdate, TDtoRead, TDtoSearch, TDtoSelect>()
  {
    CreateMap<TDtoCreate, TEntity>().ReverseMap();
    CreateMap<TDtoUpdate, TEntity>().ReverseMap();

    CreateMap<TEntity, TDtoRead>().ReverseMap();
    CreateMap<TEntity, TDtoSearch>().ReverseMap();
    CreateMap<TEntity, TDtoSelect>().ReverseMap();
  }
}

// .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.DateFrom))
// .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.DateTo));