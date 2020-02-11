using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the amount of damage on the item
    /// </summary>
    public class DamageChange : BaseChange
    {
        private MCRange damage;

        /// <summary>
        /// Intializes a new <see cref="DamageChange"/>
        /// </summary>
        /// <param name="damage">The amount of damage on the item. (0 = no durability, 1 = full)</param>
        public DamageChange(MCRange damage) : base("set_damage")
        {
            Damage = damage;
        }

        /// <summary>
        /// The amount of damage on the item. (0 = no durability, 1 = full)
        /// </summary>
        [DataTag("damage", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange Damage { get => damage; set => damage = value ?? throw new ArgumentNullException(nameof(Damage), "Damage may not be null"); }
    }
}
