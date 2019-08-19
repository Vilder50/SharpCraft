﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for door blocks
        /// </summary>
        public class Door : Block, IBlock.IFacing, IBlock.IPowered, IBlock.IPart, IBlock.IOpen
        {
            /// <summary>
            /// Creates a new door block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Door(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Door(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block.ToString().Contains("_door");
            }

            /// <summary>
            /// The direction the door is placed in.
            /// (The direction the door will fill less in.)
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// The part of the door
            /// </summary>
            [BlockState("half")]
            public ID.StatePart? SPart { get; set; }

            /// <summary>
            /// What side the hinges are on
            /// </summary>
            [BlockState("hinge")]
            public ID.StateDoorHinge? SHingeLocation { get; set; }

            /// <summary>
            /// If the door is open
            /// </summary>
            [BlockState("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// If the door is powered by redstone
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
