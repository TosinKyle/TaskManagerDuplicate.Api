using AutoMapper;
using TaskManagerDuplicate.Domain.DataTransferObjects;
using TaskManagerDuplicate.Domain.DbModels;

namespace TaskManagerDuplicate.Service.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            #region UserMapping
            CreateMap<User,DisplaySingleUserDto>().ReverseMap();
            CreateMap<DisplayUserFirstLastNameDto, User>().ReverseMap()
                .ForMember(destination => destination.FullName, options => options
                .MapFrom(source => source.FirstName+" " + source.LastName));
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<UserCreationDto,User>().ReverseMap();
            CreateMap<UserCreationResponseDto, User>().ReverseMap();
            CreateMap<CreateTaskDto,ToDoTask>().ReverseMap();
            #endregion
        }
    }
}
