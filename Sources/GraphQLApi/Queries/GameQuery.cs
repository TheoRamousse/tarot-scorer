using GraphQL.Types;
using Model;
using StubLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDB2Model;
using TarotDTO;

namespace GraphQLApi.Queries
{
    public class GameQuery : ObjectGraphType<object>
    {
        private const string QUERY_NAME = "GameQuery";
        private IDataManager _dataManager;
        public GameQuery(IDataManager dataManager)
        {
            this._dataManager = dataManager;
            Name = QUERY_NAME;

        }


    }
}
