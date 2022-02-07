using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLApi.Payloads
{
    public record PlayerDTOPayload(string firstname, string lastname, string nickname);
}
