using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// allows instantiating rules by name
    /// </summary>
    public static class RulesFactory
    {
        static Dictionary<string, Func<IRules>> factory = new Dictionary<string, Func<IRules>>();

        static RulesFactory()
        {
            factory.Add(new FrenchTarotRules().Name, () => new FrenchTarotRules());
        }

        /// <summary>
        /// creates rules by giving a name
        /// </summary>
        /// <param name="rulesName">name of the rules to create</param>
        /// <returns>rules if the name is known, null if not</returns>
        public static IRules Create(string rulesName)
        {
            if(!factory.TryGetValue(rulesName, out Func<IRules> value))
                return null;

            return value();
        }

        /// <summary>
        /// all known rules names
        /// </summary>
        public static IEnumerable<string> RulesNames => factory.Keys;
    }
}
