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
    public class BredAnimalsTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="BredAnimalsTrigger"/>
        /// </summary>
        public BredAnimalsTrigger() : base("bred_animals") { }

        /// <summary>
        /// The new child animal
        /// </summary>
        [DataTag("conditions.child", JsonTag = true)]
        public JSONObjects.Entity? Child { get; set; }

        /// <summary>
        /// One of the parent animals
        /// </summary>
        [DataTag("conditions.parent", JsonTag = true)]
        public JSONObjects.Entity? Parent1 { get; set; }

        /// <summary>
        /// The other parent animal
        /// </summary>
        [DataTag("conditions.partner", JsonTag = true)]
        public JSONObjects.Entity? Parent2 { get; set; }
    }
}
