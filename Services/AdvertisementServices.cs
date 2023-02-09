using IServices;
using Models;

namespace Services
{
    public class AdvertisementServices : IAdvertisementServices
    {
        public int Test()
        {
            return 1;
        }

        public List<AdvertisementEntity> TestAOP() => new List<AdvertisementEntity>() { new AdvertisementEntity() { id = 1, name = "laozhang" } };
    }
}