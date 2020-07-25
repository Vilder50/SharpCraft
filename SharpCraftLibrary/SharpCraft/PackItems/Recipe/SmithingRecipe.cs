using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// Class for stone cutter recipe files
    /// </summary>
    public class SmithingRecipe : BaseRecipe
    {
        private IItemType baseItem = null!;
        private IItemType modifierItem = null!;

        /// <summary>
        /// Intializes a new <see cref="SmithingRecipe"/>
        /// </summary>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        /// <param name="packNamespace">The namespace the file should be added to</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="baseItem">The first item in the recipe (NBT will be copied from this item to the output item)</param>
        /// <param name="modifierItem">The second item in the recipe</param>
        /// <param name="output">The item the recipe outputs (Note that it will copy nbt from <see cref="BaseItem"/>)</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        public SmithingRecipe(bool _, BasePackNamespace packNamespace, string? fileName, IItemType baseItem, IItemType modifierItem, ID.Item output, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(packNamespace, fileName, null, writeSetting, "minecraft:smithing")
        {
            BaseItem = baseItem;
            ModifierItem = modifierItem;
            Output = output;
        }

        /// <summary>
        /// Intializes a new <see cref="SmithingRecipe"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the file should be added to</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="baseItem">The first item in the recipe (NBT will be copied from this item to the output item)</param>
        /// <param name="modifierItem">The second item in the recipe</param>
        /// <param name="output">The item the recipe outputs (Note that it will copy nbt from <see cref="BaseItem"/>)</param>
        /// <param name="writeSetting">The settings for how to write this file</param>
        public SmithingRecipe(BasePackNamespace packNamespace, string? fileName, IItemType baseItem, IItemType modifierItem, ID.Item output, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, packNamespace, fileName, baseItem, modifierItem, output, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// The first item in the recipe (NBT will be copied from this item to the output item)
        /// </summary>
        public IItemType BaseItem { get => baseItem; set => baseItem = value ?? throw new ArgumentNullException(nameof(BaseItem), "BaseItem may not be null for smithing recipe"); }

        /// <summary>
        /// The second item in the recipe
        /// </summary>
        public IItemType ModifierItem { get => modifierItem; set => modifierItem = value ?? throw new ArgumentNullException(nameof(modifierItem), "modifierItem may not be null for smithing recipe"); }

        /// <summary>
        /// The item the recipe outputs (Note that it will copy nbt from <see cref="BaseItem"/>)
        /// </summary>
        public ID.Item Output { get; set; }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            WriteFileStart(stream);

            stream.Write(",\"base\":" + GetItemCompound(BaseItem));
            stream.Write(",\"addition\":" + GetItemCompound(ModifierItem));
            stream.Write(",\"result\":" + GetItemCompound(Output));

            WriteFileEnd(stream);
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            base.AfterDispose();
            baseItem = null!;
            modifierItem = null!;
        }
    }
}
