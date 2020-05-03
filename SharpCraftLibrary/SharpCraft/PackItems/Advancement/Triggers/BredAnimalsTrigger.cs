using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when 2 animals are bred together
    /// </summary>
    public class BredAnimalsTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="BredAnimalsTrigger"/>
        /// </summary>
        public BredAnimalsTrigger() : base("bred_animals") { }

        /// <summary>
        /// The new child animal
        /// </summary>
        [DataTag("conditions.child", JsonTag = true)]
        public Conditions.EntityCondition[] Child { get; set; }

        /// <summary>
        /// One of the parent animals
        /// </summary>
        [DataTag("conditions.parent", JsonTag = true)]
        public Conditions.EntityCondition[] Parent1 { get; set; }

        /// <summary>
        /// The other parent animal
        /// </summary>
        [DataTag("conditions.partner", JsonTag = true)]
        public Conditions.EntityCondition[] Parent2 { get; set; }
    }
}
