using System;
using System.Collections.Generic;
using System.Linq;

namespace TarotDB2Model
{
    class EnumsMapper<TModel, TEntity> where TModel : Enum
                                              where TEntity : Enum
    {
        HashSet<Tuple<TModel, TEntity>> mapper = new HashSet<Tuple<TModel, TEntity>>();

        public TModel GetModel(TEntity entity)
        {
            var result = mapper.Where(tuple => tuple.Item2.Equals(entity));
            if(result.Count() != 1)
            {
                return default(TModel);
            }
            return result.First().Item1;
        }

        public TEntity GetEntity(TModel model)
        {
            var result = mapper.Where(tuple => tuple.Item1.Equals(model));
            if(result.Count() != 1)
            {
                return default(TEntity);
            }
            return result.First().Item2;
        }

        internal void Add(TModel model, TEntity entity)
        {
            mapper.Add(Tuple.Create(model, entity));
        }

        internal void AddRange(params Tuple<TModel, TEntity>[] tuples)
        {
            foreach(var t in tuples)
            {
                Add(t.Item1, t.Item2);
            }
        }

        internal EnumsMapper(params Tuple<TModel, TEntity>[] tuples)
        {
            AddRange(tuples);
        }
    }

    static class EnumsMapper
    {
        public static EnumsMapper<Model.Bidding, TarotDB.Bidding> BiddingsMapper { get; }
            = new EnumsMapper<Model.Bidding, TarotDB.Bidding>(
                Tuple.Create(Model.Bidding.None, TarotDB.Bidding.None),
                Tuple.Create(Model.Bidding.Garde, TarotDB.Bidding.Garde),
                Tuple.Create(Model.Bidding.GardeContre, TarotDB.Bidding.GardeContre),
                Tuple.Create(Model.Bidding.GardeSans, TarotDB.Bidding.GardeSans),
                Tuple.Create(Model.Bidding.KingCalled, TarotDB.Bidding.KingCalled),
                Tuple.Create(Model.Bidding.Opponent, TarotDB.Bidding.Opponent),
                Tuple.Create(Model.Bidding.Petite, TarotDB.Bidding.Petite),
                Tuple.Create(Model.Bidding.Pousse, TarotDB.Bidding.Pousse),
                Tuple.Create(Model.Bidding.Prise, TarotDB.Bidding.Prise)
                );

        public static EnumsMapper<Model.Chelem, TarotDB.Chelem> ChelemsMapper { get; }
            = new EnumsMapper<Model.Chelem, TarotDB.Chelem>(
                Tuple.Create(Model.Chelem.Announced, TarotDB.Chelem.Announced),
                Tuple.Create(Model.Chelem.AnnouncedFail, TarotDB.Chelem.AnnouncedFail),
                Tuple.Create(Model.Chelem.AnnouncedSuccess, TarotDB.Chelem.AnnouncedSuccess),
                Tuple.Create(Model.Chelem.Fail, TarotDB.Chelem.Fail),
                Tuple.Create(Model.Chelem.NotAnnouncedSuccess, TarotDB.Chelem.NotAnnouncedSuccess),
                Tuple.Create(Model.Chelem.Success, TarotDB.Chelem.Success),
                Tuple.Create(Model.Chelem.Unknown, TarotDB.Chelem.Unknown)
                );

        public static EnumsMapper<Model.PetitResult, TarotDB.PetitResult> PetitResultsMapper { get; }
            = new EnumsMapper<Model.PetitResult, TarotDB.PetitResult>(
                Tuple.Create(Model.PetitResult.AuBout, TarotDB.PetitResult.AuBout),
                Tuple.Create(Model.PetitResult.Hunted, TarotDB.PetitResult.Hunted),
                Tuple.Create(Model.PetitResult.HuntedAuBout, TarotDB.PetitResult.HuntedAuBout),
                Tuple.Create(Model.PetitResult.Lost, TarotDB.PetitResult.Lost),
                Tuple.Create(Model.PetitResult.LostAuBout, TarotDB.PetitResult.LostAuBout),
                Tuple.Create(Model.PetitResult.NotOwned, TarotDB.PetitResult.NotOwned),
                Tuple.Create(Model.PetitResult.Owned, TarotDB.PetitResult.Owned),
                Tuple.Create(Model.PetitResult.Saved, TarotDB.PetitResult.Saved),
                Tuple.Create(Model.PetitResult.SavedAuBout, TarotDB.PetitResult.SavedAuBout),
                Tuple.Create(Model.PetitResult.Unknown, TarotDB.PetitResult.Unknown)
                );

        public static EnumsMapper<Model.Poignée, TarotDB.Poignée> PoignéesMapper { get; }
            = new EnumsMapper<Model.Poignée, TarotDB.Poignée>(
                Tuple.Create(Model.Poignée.Defense, TarotDB.Poignée.Defense),
                Tuple.Create(Model.Poignée.Double, TarotDB.Poignée.Double),
                Tuple.Create(Model.Poignée.DoubleDefense, TarotDB.Poignée.DoubleDefense),
                Tuple.Create(Model.Poignée.None, TarotDB.Poignée.None),
                Tuple.Create(Model.Poignée.Simple, TarotDB.Poignée.Simple),
                Tuple.Create(Model.Poignée.SimpleDefense, TarotDB.Poignée.SimpleDefense),
                Tuple.Create(Model.Poignée.Triple, TarotDB.Poignée.Triple),
                Tuple.Create(Model.Poignée.TripleDefense, TarotDB.Poignée.TripleDefense),
                Tuple.Create(Model.Poignée.Unknown, TarotDB.Poignée.Unknown)
                );

        public static TModel ToModel<TModel, TEntity>(this TEntity entity) where TModel : Enum
                                                                           where TEntity : Enum
        {
            foreach(var prop in typeof(EnumsMapper).GetProperties())
            {
                if(prop.PropertyType.Equals(typeof(EnumsMapper<TModel, TEntity>)))
                {
                    return (prop.GetValue(null) as EnumsMapper<TModel, TEntity>).GetModel(entity);
                }
            }
            return default(TModel);
        }

        public static TEntity ToEntity<TModel, TEntity>(this TModel model) where TModel : Enum
                                                                           where TEntity : Enum
        {
            foreach(var prop in typeof(EnumsMapper).GetProperties())
            {
                if(prop.PropertyType.Equals(typeof(EnumsMapper<TModel, TEntity>)))
                {
                    return (prop.GetValue(null) as EnumsMapper<TModel, TEntity>).GetEntity(model);
                }
            }
            return default(TEntity);
        }

        public static TarotDB.Chelem ToEntity(this Model.Chelem model)
            => ToEntity<Model.Chelem, TarotDB.Chelem>(model);

        public static TarotDB.Bidding ToEntity(this Model.Bidding model)
            => ToEntity<Model.Bidding, TarotDB.Bidding>(model);

        public static TarotDB.PetitResult ToEntity(this Model.PetitResult model)
            => ToEntity<Model.PetitResult, TarotDB.PetitResult>(model);

        public static TarotDB.Poignée ToEntity(this Model.Poignée model)
            => ToEntity<Model.Poignée, TarotDB.Poignée>(model);

        public static Model.Chelem ToModel(this TarotDB.Chelem entity)
            => ToModel<Model.Chelem, TarotDB.Chelem>(entity);

        public static Model.Bidding ToModel(this TarotDB.Bidding entity)
            => ToModel<Model.Bidding, TarotDB.Bidding>(entity);

        public static Model.PetitResult ToModel(this TarotDB.PetitResult entity)
            => ToModel<Model.PetitResult, TarotDB.PetitResult>(entity);

        public static Model.Poignée ToModel(this TarotDB.Poignée entity)
            => ToModel<Model.Poignée, TarotDB.Poignée>(entity);
    }
}
