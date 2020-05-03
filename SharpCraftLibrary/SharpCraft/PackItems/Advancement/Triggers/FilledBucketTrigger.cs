using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player fills a bucket
    /// </summary>
    public class FilledBucketTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="FilledBucketTrigger"/>
        /// </summary>
        public FilledBucketTrigger() : base("filled_bucket") { }

        /// <summary>
        /// The item the player got from filling a bucket
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item Item { get; set; }
    }
}
