using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLApi.Payloads
{
    public record PlayerDTOPayload(string firstName, string lastName, string nickName);
}
