
namespace Services.MappingProfiles
{
    internal class UserProfile : Profile
    {

        public UserProfile()
        {
        CreateMap<AddressDTO, Address>().ReverseMap();
            
        }
    }
}
