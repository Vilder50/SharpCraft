namespace SharpCraft
{
    /// <summary>
    /// An object used for minecraft attributes
    /// </summary>
    public class MCAttribute
    {
        /// <summary>
        /// Creates a new attribute for an item
        /// </summary>
        /// <param name="ID">The attribute type</param>
        /// <param name="ChangeAmount">The amount to change the attribute with</param>
        /// <param name="Operation">The operation used to change</param>
        /// <param name="UUID">The UUID of the attribute</param>
        public MCAttribute(ID.AttributeType ID, double ChangeAmount, ID.AttributeOperation Operation, UUID UUID)
        {
            this.ID = ID;
            if (Operation == SharpCraft.ID.AttributeOperation.multiply_base ||  Operation == SharpCraft.ID.AttributeOperation.multiply_total)
            {
                this.ChangeAmount = ChangeAmount - 1;
            }
            else
            {
                this.ChangeAmount = ChangeAmount;
            }
            this.Operation = Operation;
            this.UUID = UUID;
        }

        /// <summary>
        /// Creates a new attribute for an entity
        /// </summary>
        /// <param name="ID">the attribute type</param>
        /// <param name="Base">the base value of the attribute</param>
        public MCAttribute(ID.AttributeType ID, double Base)
        {
            this.ID = ID;
            this.Base = Base;
        }

        /// <summary>
        /// The type of attribute
        /// </summary>
        public ID.AttributeType ID;

        /// <summary>
        /// The base amount of the attribute
        /// </summary>
        public double Base;

        /// <summary>
        /// The slot the attribute affects
        /// </summary>
        public ID.AttributeSlot? Slot;

        /// <summary>
        /// The operation used to add the <see cref="ChangeAmount"/> with
        /// </summary>
        public ID.AttributeOperation Operation;

        /// <summary>
        /// The amount to change the atttribute with
        /// </summary>
        public double? ChangeAmount;

        /// <summary>
        /// The UUID of the attribute
        /// </summary>
        public UUID UUID;

        /// <summary>
        /// Gets the raw data used for items
        /// </summary>
        /// <returns>Raw data used by the game in items</returns>
        public string ItemString()
        {
            string TempString = "{";

            TempString += "AttributeName:\"" + ID.ToString().Replace("_", ".") + "\",Name:\"" + ID + "\",Amount:" + ChangeAmount.ToMinecraftDouble() + ",Operation:" + (int)Operation;
            if (Slot != null) { TempString += ",Slot:" + Slot; }
            if (UUID != null) { TempString += ",UUIDMost:" + UUID.Most + ",UUIDLeast:" + UUID.Least; }

            return TempString + "}";
        }

        /// <summary>
        /// Gets the raw data used for entities
        /// </summary>
        /// <returns>Raw data used by the game in entities</returns>
        public string EntityString()
        {
            string TempString = "{";

            TempString += "Name:\"" + ID.ToString().Replace("_", ".") + "\",Base:" + Base.ToMinecraftDouble();
            if (ChangeAmount != null)
            {
                TempString += " Modifiers:{Name:\"" + ID + "\",Amount:" + ChangeAmount.ToMinecraftDouble() + ",Operation:" + (int)Operation + ",UUIDMost:" + UUID.Most + ",UUIDLeast:" + UUID.Least + "}";
            }

            return TempString + "}";
        }
    }
}
