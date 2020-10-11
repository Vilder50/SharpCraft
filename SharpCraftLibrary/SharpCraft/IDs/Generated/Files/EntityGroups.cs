//
//This file was generated by SharpCraft.Generator.
//Do not make changes directly to this file. Change the template file instead.
//

using SharpCraft.Data;

namespace SharpCraft
{
    public partial class ID
    {
        /// <summary>
        /// File names used by Minecraft
        /// </summary>
        public static partial class Files
        {
		    /// <summary>
            /// Names of the different types of groups in the game (Normally called "tags" in the game)
            /// </summary>
			public static partial class Groups
			{
				#pragma warning disable 1591
				/// <summary>
				/// Entity groups
				/// </summary>
				public class Entity : NamespacedEnumLike<string>, IEntityType
				{
				    public Entity(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
				    {
				    }

				    /// <summary>
				    /// Converts this type into a <see cref="DataPartObject"/>
				    /// </summary>
				    /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
				    /// <returns></returns>
				    public DataPartObject GetAsDataObject(object?[] conversionData)
				    {
				        return (this as IGroupable).GetGroupData(conversionData);
				    }

					/// <summary>
					/// Returns a string that represents the current object
					/// </summary>
					/// <returns>A string that represents the current object</returns>
					public override string ToString()
					{
					    return "#" + base.ToString();
					}

				    public string Name => ToString();

				    public bool IsAGroup => true;

				    public static readonly Entity arrows = new Entity("arrows");

/// <summary>
///entities which can be in beehives
/// </summary>
				    public static readonly Entity beehive_inhabitors = new Entity("beehive_inhabitors");

/// <summary>
///Entities which can break chorus fruit.
/// </summary>
				    public static readonly Entity impact_projectiles = new Entity("impact_projectiles");

/// <summary>
///Entities which glows when the bell rings. Entities which don't override ravager AI when riding on one
/// </summary>
				    public static readonly Entity raiders = new Entity("raiders");
				    public static readonly Entity skeletons = new Entity("skeletons");

				}
			}
		}
	}
}