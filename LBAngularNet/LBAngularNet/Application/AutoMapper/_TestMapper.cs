using AutoMapper;
using LBAngularNet.Core.Domain.Entities;

namespace LBAngularNet.Application.AutoMapper
{
    public class _TestMapper
    {
        public void TestMapper() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
             });

            IMapper mapper = config.CreateMapper();

            var demoList = new List<Demo>
            {
                new Demo{ Id = 1 , Name = "ABC" , Birth = new DateTime(1991,12,14)}
            };

            var demoDTO= mapper.Map<List<Demo>>(demoList);  
        }
    }
}
