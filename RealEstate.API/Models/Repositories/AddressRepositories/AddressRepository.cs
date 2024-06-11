using Dapper;
using RealEstate.API.DTOs.AddressDtos;
using RealEstate.API.Models.DapperContext;

namespace RealEstate.API.Models.Repositories.AddressRepositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly Context _context;

        public AddressRepository(Context context)
        {
            _context = context;
        }
        public async Task<GetByIDAddressDto> GetAddress(int addressId)
        {
            string query = "Select * From Address Where AddressID=@addressID";
            var parameters = new DynamicParameters();
            parameters.Add("@addressID", addressId);
            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIDAddressDto>(query,parameters);
                return value;
            }
        }

        public async Task UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            string query = "Update Address set AddressTitle1=@addressTitle1, Description=@description, AddressTitle2=@addressTitle2, Phone1=@phone1, Phone2=@phone2, Email=@email, Location=@location where AddressID=@addressID";
            var parameters = new DynamicParameters();
            parameters.Add("@addressTitle1", updateAddressDto.AddressTitle1);
            parameters.Add("@description", updateAddressDto.Description);
            parameters.Add("@addressTitle2", updateAddressDto.AddressTitle2);
            parameters.Add("@phone1", updateAddressDto.Phone1);
            parameters.Add("@phone2", updateAddressDto.Phone2);
            parameters.Add("@email", updateAddressDto.Email);
            parameters.Add("@location", updateAddressDto.Location);
            parameters.Add("@addressID", updateAddressDto.AddressID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
