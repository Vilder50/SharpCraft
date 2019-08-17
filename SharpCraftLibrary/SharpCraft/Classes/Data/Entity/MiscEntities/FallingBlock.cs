using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for falling block entities
        /// </summary>
        public class FallingBlock : EntityBasic
        {
            /// <summary>
            /// Creates a new falling block
            /// </summary>
            /// <param name="type">the type of entity</param>
            public FallingBlock(ID.Entity? type = ID.Entity.falling_block) : base(type) { }

            /// <summary>
            /// The time the entity has been falling.
            /// If this number is 0 the entity will dissapear the next tick
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time Time { get; set; }

            /// <summary>
            /// The falling block
            /// </summary>
            [Data.CustomDataTag]
            public Block TheBlock { get; set; }

            /// <summary>
            /// If the block should be dropped if the falling block is destroyed
            /// </summary>
            [Data.DataTag]
            public bool? DropItem { get; set; }

            /// <summary>
            /// If the block should damage entities it lands on
            /// </summary>
            [Data.DataTag]
            public bool? HurtEntities { get; set; }

            /// <summary>
            /// The maximum amount of damage it can cause
            /// </summary>
            [Data.DataTag("FallHurtMax")]
            public int? MaxDamage { get; set; }

            /// <summary>
            /// The amount of damage it should cause per block fallen
            /// </summary>
            [Data.DataTag("FallHurtAmount")]
            public float? DamageAmount { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Time != null) { TempList.Add("Time:" + Time.AsTicks()); }
                    if (TheBlock != null)
                    {
                        if (TheBlock.ID != null || TheBlock.HasState)
                        {
                            string blockState = "BlockState:{";
                            if (TheBlock.ID != null) { blockState += "Name:\"minecraft:" + TheBlock.ID.ToString() + "\""; }
                            if (TheBlock.ID != null && TheBlock.HasState) { blockState += ","; }
                            if (TheBlock.HasState) { blockState += "Properties:{" + TheBlock.GetStateString().ToString().Replace("=", ":\"").Replace(",", "\",") + "\"}"; }
                            TempList.Add(blockState + "}");
                        }
                        if (TheBlock.HasData) { TempList.Add(",TileEntityData:{" + TheBlock.GetDataString() + "}"); }
                    }
                    if (DropItem != null) { TempList.Add("DropItem:" + DropItem); }
                    if (HurtEntities != null) { TempList.Add("HurtEntities:" + HurtEntities); }
                    if (MaxDamage != null) { TempList.Add("FallHurtMax:" + MaxDamage); }
                    if (DamageAmount != null) { TempList.Add("FallHurtAmount:" + DamageAmount.ToString().Replace(",", ".") + "f"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
