using Bogus;
using System.Collections.Generic;
using System.Web.Http;

namespace NetFwVirtualMachineWebMvc.Api
{
    public class UserController : ApiController
    {
        public IEnumerable<UserDto> Get()
        {
            var result = new Faker<UserDto>()
                .RuleFor(o => o.Id, f => f.Random.Int(1, 100))
                .RuleFor(o => o.FistName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Email, f => f.Internet.Email());

            return result.Generate(5);
        }
    }
}