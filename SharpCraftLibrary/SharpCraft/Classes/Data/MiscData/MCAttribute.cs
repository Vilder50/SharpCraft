using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// An object used for minecraft attributes
    /// </summary>
    public class MCAttribute : IConvertableToDataObject
    {
        /// <summary>
        /// Creates a new attribute
        /// </summary>
        public MCAttribute()
        {

        }

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
        public ID.AttributeType ID { get; set; }

        /// <summary>
        /// The base amount of the attribute
        /// </summary>
        public double Base { get; set; }

        /// <summary>
        /// The slot the attribute affects
        /// </summary>
        public ID.AttributeSlot? Slot { get; set; }

        /// <summary>
        /// The operation used to add the <see cref="ChangeAmount"/> with
        /// </summary>
        public ID.AttributeOperation Operation { get; set; }

        /// <summary>
        /// The amount to change the atttribute with
        /// </summary>
        public double? ChangeAmount { get; set; }

        /// <summary>
        /// The UUID of the attribute
        /// </summary>
        public UUID UUID { get; set; }

        /// <summary>
        /// Converts this <see cref="MCAttribute"/> into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: "Entity" for entity tag and "Item" for item tag</param>
        /// <returns>the made <see cref="DataPartObject"/></returns>
        public DataPartObject GetAsDataObject(object[] conversionData)
        {
            if (conversionData.Length == 1 && conversionData[0] is string type)
            {
                if (type == "Entity")
                {
                    DataPartObject returnObject = new DataPartObject();
                    returnObject.AddValue(new DataPartPath("Name", new DataPartTag(ID.ToString())));
                    returnObject.AddValue(new DataPartPath("Base", new DataPartTag(Base)));
                    if (!(ChangeAmount is null))
                    {
                        DataPartObject modifier = new DataPartObject();
                        modifier.AddValue(new DataPartPath("Name", new DataPartTag(ID.ToString())));
                        modifier.AddValue(new DataPartPath("Amount", new DataPartTag(ChangeAmount)));
                        modifier.AddValue(new DataPartPath("Operation", new DataPartTag((int)Operation)));
                        modifier.MergeDataPartObject(UUID.GetAsDataObject(new object[] { "UUIDMost", "UUIDLeast" }));

                        returnObject.AddValue(new DataPartPath("Modifiers", new DataPartArray(modifier, null, null)));
                    }

                    return returnObject;
                }
                else if (type == "Item")
                {
                    DataPartObject returnObject = new DataPartObject();
                    returnObject.AddValue(new DataPartPath("AttributeName", new DataPartTag(ID.ToString())));
                    returnObject.AddValue(new DataPartPath("Name", new DataPartTag(ID.ToString())));
                    returnObject.AddValue(new DataPartPath("Amount", new DataPartTag(ChangeAmount)));
                    returnObject.AddValue(new DataPartPath("Operation", new DataPartTag((int)Operation)));
                    if (!(Slot is null))
                    {
                        returnObject.AddValue(new DataPartPath("Slot", new DataPartTag(Slot.ToString())));
                    }
                    if (!(UUID is null))
                    {
                        returnObject.MergeDataPartObject(UUID.GetAsDataObject(new object[] { "UUIDMost", "UUIDLeast" }));
                    }

                    return returnObject;
                }
            }
            
            throw new System.ArgumentException("The conversion data is not correct for converting this MCAttribute");
        }
    }
}
