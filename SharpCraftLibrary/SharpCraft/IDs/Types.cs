using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Interface for block types
    /// </summary>
    public interface IBlockType: IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {
        
    }

    /// <summary>
    /// Interface for item types
    /// </summary>
    public interface IItemType : IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {
        
    }

    /// <summary>
    /// Interface for entity types
    /// </summary>
    public interface IEntityType : IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {

    }

    /// <summary>
    /// Interface for fluid types
    /// </summary>
    public interface IFluidType : IGroupable, IConvertableToDataTag, IConvertableToDataObject
    {

    }
}
