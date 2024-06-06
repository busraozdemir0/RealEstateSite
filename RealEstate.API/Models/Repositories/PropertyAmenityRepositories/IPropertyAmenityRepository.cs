using RealEstate.API.DTOs.PropertyAmenityDtos;

namespace RealEstate.API.Models.Repositories.PropertyAmenityRepositories
{
    public interface IPropertyAmenityRepository
    {
        Task<List<ResultPropertyAmenityByStatusTrueDto>> ResultPropertyAmenityByStatusTrue(int id); // Product id gonderilerek PropertyAmenity tablosunda o ilana ait True olan degerler listelenecek (Amenitie => kolaylıklar)
    }
}
