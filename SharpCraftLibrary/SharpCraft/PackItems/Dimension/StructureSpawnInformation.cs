using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Class for holding data about a structure used for dimension generation.
    /// </summary>
    public class StructureSpawnInformation : DataHolderBase
    {
        private int spacing;
        private int separation;

        /// <summary>
        /// Intializes a new <see cref="Structure"/>
        /// </summary>
        /// <param name="structure">The type of structure</param>
        /// <param name="spacing">The maximum distance in chunks between the structures</param>
        /// <param name="separation">The minimum distance in chunks between the structures</param>
        /// <param name="salt">A random number added to the seed to make structures random</param>
        public StructureSpawnInformation(ID.Structure structure, int spacing, int separation, int salt)
        {
            StructureType = structure;
            SetSpacingAndSeperation(spacing, separation);
            Salt = salt;
        }

        /// <summary>
        /// The type of structure
        /// </summary>
        public ID.Structure StructureType { get; set; }

        /// <summary>
        /// The maximum distance in chunks between the structures
        /// </summary>
        [DataTag("spacing", JsonTag = true)]
        public int Spacing { get => spacing; set => spacing = SetSpacingAndSeperation(value, spacing).spacing; }

        /// <summary>
        /// The minimum distance in chunks between the structures
        /// </summary>
        [DataTag("separation", JsonTag = true)]
        public int Separation { get => separation; set => separation = SetSpacingAndSeperation(Spacing, value).seperation; }

        /// <summary>
        /// A random number added to the seed to make structures random over multiple seeds
        /// </summary>
        [DataTag("salt", JsonTag = true)]
        public int Salt { get; set; }

        /// <summary>
        /// Sets both <see cref="Spacing"/> and <see cref="Separation"/>
        /// </summary>
        /// <param name="spacing">The new spacing value</param>
        /// <param name="separation">The new seperation value</param>
        /// <returns>The set values</returns>
        public (int spacing, int seperation) SetSpacingAndSeperation(int spacing, int separation)
        {
            if (separation >= spacing)
            {
                throw new ArgumentOutOfRangeException("Separation may not be higher than or equal to spacing");
            }
            if (separation < 0 || separation > 4096)
            {
                throw new ArgumentOutOfRangeException(nameof(separation), "Separation may not be less than 0 or higher than 4096");
            }
            if (spacing < 0 || spacing > 4096)
            {
                throw new ArgumentOutOfRangeException(nameof(spacing), "Spacing may not be less than 0 or higher than 4096");
            }

            this.spacing = spacing;
            this.separation = separation;

            return (spacing, separation);
        }
    }

    /// <summary>
    /// Class for a list of structures
    /// </summary>
    public class StructureList : DataHolderBase
    {
        private List<StructureSpawnInformation> structures = new List<StructureSpawnInformation>();

        /// <summary>
        /// Intializes a new <see cref="StructureList"/>
        /// </summary>
        /// <param name="structures">The list of structures</param>
        public StructureList(List<StructureSpawnInformation> structures)
        {
            Structures = structures;
        }

        /// <summary>
        /// Intializes a new <see cref="StructureList"/>
        /// </summary>
        /// <param name="structures">The list of structures</param>
        public StructureList(params StructureSpawnInformation[] structures)
        {
            Structures = structures.ToList();
        }

        /// <summary>
        /// The list of structures
        /// </summary>
        public List<StructureSpawnInformation> Structures { get => structures; set => structures = Utils.ValidateNoneNullList(value, nameof(Structures), nameof(StructureList)); }

        /// <summary>
        /// Returns the data from the list
        /// </summary>
        /// <returns>The list's data</returns>
        public override DataPartObject GetDataTree()
        {
            DataPartObject baseObject = new DataPartObject(true);

            foreach (StructureSpawnInformation structure in Structures)
            {
                baseObject.AddValue(new DataPartPath("minecraft:" + structure.StructureType.ToString().ToLower(), structure.GetDataTree(), true));
            }

            return baseObject;
        }

        /// <summary>
        /// Converts a list of <see cref="Structure"/>s into a <see cref="StructureList"/>
        /// </summary>
        /// <param name="structures">The list to convert</param>
        public static implicit operator StructureList(List<StructureSpawnInformation> structures)
        {
            return new StructureList(structures);
        }

        /// <summary>
        /// Converts an array of <see cref="Structure"/>s into a <see cref="StructureList"/>
        /// </summary>
        /// <param name="structures">The array to convert</param>
        public static implicit operator StructureList(StructureSpawnInformation[] structures)
        {
            return new StructureList(structures);
        }
    }
}
