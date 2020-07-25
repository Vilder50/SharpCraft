using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public enum StructureRotation { NONE, CLOCKWISE_90, CLOCKWISE_180, COUNTERCLOCKWISE_90 }
        public enum StructureMirror { NONE, LEFT_RIGHT, FRONT_BACK }
        public enum StructureDataMode { DATA, SAVE, LOAD, CORNER }
        public enum StructureMode { data, save, load, corner }
        public enum FacingFull { down, up, north, south, west, east }
        public enum PistonType { normal, sticky }

        public enum StateWallConnection
        {
            /// <summary>
            /// Wall isn't connected
            /// </summary>
            none,
            /// <summary>
            /// Wall is connected. But no connection upwards.
            /// </summary>
            low,
            /// <summary>
            /// Wall is connected upwards
            /// </summary>
            tall,
        }

        public enum StateCompareMode
        {
            /// <summary>
            /// Outputs the signal if it's the greatest signal
            /// </summary>
            compare,
            /// <summary>
            /// Subtracts the side signal from the input signal
            /// </summary>
            subtract
        }
        public enum StatePortalAxis { x, z }
        public enum StateNoteInstrument { basedrum, xylophone, chime, guitar, bell, flute, bass, hat, snare, harp }
        public enum StateSlabPart { bottom, top, both }
        public enum StateHopperFacing { north, south, east, west, down }
        public enum StateChestType
        {
            /// <summary>
            /// Its a single chest
            /// </summary>
            single,
            /// <summary>
            /// Connected with the chest to the left
            /// </summary>
            left,
            /// <summary>
            /// Connected with the chest to the right
            /// </summary>
            right
        }
        public enum StateDoorHinge { left, right }
        public enum StatePlaced { wall, ceiling, floor }
        public enum StateBambooLeave { none, small, large }
        public enum StateBedPart { foot, head }
        public enum StateBellAttachment { ceiling, double_wall, floor, single_wall }
        public enum StatePart { lower, upper }
        public enum StateNote
        {
            /// <summary>
            /// F♯/G♭
            /// </summary>
            FSharp1,
            G1,
            /// <summary>
            /// G♯/A♭
            /// </summary>
            GSharp1,
            A1,
            /// <summary>
            /// A♯/B♭
            /// </summary>
            ASharp1,
            B1,
            C1,
            /// <summary>
            /// C♯/D♭
            /// </summary>
            CSharp1,
            D1,
            /// <summary>
            /// D♯/E♭
            /// </summary>
            DSharp1,
            E1,
            F1,
            /// <summary>
            /// F♯/G♭
            /// </summary>
            FSharp2,
            G2,
            /// <summary>
            /// G♯/A♭
            /// </summary>
            GSharp2,
            A2,
            /// <summary>
            /// A♯/B♭
            /// </summary>
            ASharp2,
            B2,
            C2,
            /// <summary>
            /// C♯/D♭
            /// </summary>
            CSharp2,
            D2,
            /// <summary>
            /// D♯/E♭
            /// </summary>
            DSharp2,
            E2,
            F2,
            /// <summary>
            /// F♯/G♭
            /// </summary>
            FSharp3,
        }
        public enum StateRailShape { east_west, north_east, north_south, north_west, south_east, south_west, ascending_east, ascending_north, ascending_south, ascending_west }
        public enum StateSpecailRailShape { east_west, north_south, ascending_east, ascending_north, ascending_south, ascending_west }
        public enum StateRedstoneConnection { none, side, up }
        public enum StateStairShape
        {
            /// <summary>
            /// 7/8 of a block.
            /// </summary>
            inner_left,
            /// <summary>
            /// 7/8 of a block.
            /// </summary>
            inner_right,
            /// <summary>
            /// 5/8 of a block.
            /// </summary>
            outer_left,
            /// <summary>
            /// 5/8 of a block.
            /// </summary>
            outer_right,
            /// <summary>
            /// 6/8 of a block.
            /// </summary>
            straight
        }
        public enum StateSimplePlaced { bottom, top }
        public enum Axis { x, y, z }
        public enum Facing { north, south, east, west }
        public enum JigsawOrientation
        {
            down_east,
            down_south,
            down_north,
            down_west,
            east_up,
            north_up,
            south_up,
            up_east,
            up_north,
            up_south,
            up_west,
            west_up
        }
        public enum JigsawJoint
        {
            /// <summary>
            /// If it can be rotated
            /// </summary>
            rollable,
            /// <summary>
            /// If it can't be rotated
            /// </summary>
            aligned
        }
        public enum BannerPattern
        {
            /// <summary>
            /// Bottom Stripe
            /// </summary>
            bs,
            /// <summary>
            /// Top Stripe
            /// </summary>
            ts,
            /// <summary>
            /// Left Stripe
            /// </summary>
            ls,
            /// <summary>
            /// Right Stripe
            /// </summary>
            rs,
            /// <summary>
            /// Center Stripe
            /// (bottom to top)
            /// </summary>
            cs,
            /// <summary>
            /// Middle Stripe
            /// (left to right)
            /// </summary>
            ms,
            /// <summary>
            /// Down Right Stripe
            /// (Starts at top left ends at bottom right)
            /// </summary>
            drs,
            /// <summary>
            /// Down Left Stripe
            /// (Starts at top right ends at bottom left)
            /// </summary>
            dls,
            /// <summary>
            /// Small Stripes
            /// (multiple lines going from bottom to top)
            /// </summary>
            ss,
            /// <summary>
            /// Cross
            /// (X)
            /// </summary>
            cr,
            /// <summary>
            /// Cross
            /// (+)
            /// </summary>
            sc,
            /// <summary>
            /// Left Top Diagonal
            /// (Goes from bottom left to top right with the left side being colored)
            /// </summary>
            ld,
            /// <summary>
            /// Right Top Diagonal
            /// (Goes from top left to bottom right with the right side being colored)
            /// </summary>
            rud,
            /// <summary>
            /// Left Bottom Diagonal
            /// (Goes from top left to bottom right with the left side being colored)
            /// </summary>
            lud,
            /// <summary>
            /// Right Bottom Diagonal
            /// (Goes from bottom left to top right with the right side being colored)
            /// </summary>
            rd,
            /// <summary>
            /// Vertical Half Left
            /// </summary>
            vh,
            /// <summary>
            /// Vertical Half Right
            /// </summary>
            vhr,
            /// <summary>
            /// Horizontal Half Top
            /// </summary>
            hh,
            /// <summary>
            /// Horizontal Half Bottom
            /// </summary>
            hhb,
            /// <summary>
            /// Bottom Left Square
            /// </summary>
            bl,
            /// <summary>
            /// Bottom Right Square
            /// </summary>
            br,
            /// <summary>
            /// top Left Square
            /// </summary>
            tl,
            /// <summary>
            /// Top Right Square
            /// </summary>
            tr,
            /// <summary>
            /// Bottom Triangle
            /// </summary>
            bt,
            /// <summary>
            /// Top Triangle
            /// </summary>
            tt,
            /// <summary>
            /// Multiple Bottom Triangles
            /// </summary>
            bts,
            /// <summary>
            /// Multiple Top Triangles
            /// </summary>
            tts,
            /// <summary>
            /// Middle Circle
            /// </summary>
            mc,
            /// <summary>
            /// Middle Rhombus
            /// </summary>
            mr,
            /// <summary>
            /// Border
            /// </summary>
            bo,
            /// <summary>
            /// Weird waving border
            /// </summary>
            cbo,
            /// <summary>
            /// Bricks
            /// </summary>
            bri,
            /// <summary>
            /// Top Gradiant
            /// (Solid at top and gone at bottom)
            /// </summary>
            gra,
            /// <summary>
            /// Bottom Gradiant
            /// (Solid at bottom and gone at top)
            /// </summary>
            gru,
            /// <summary>
            /// Creeper
            /// </summary>
            cre,
            /// <summary>
            /// Skull
            /// </summary>
            sku,
            /// <summary>
            /// Flower
            /// </summary>
            flo,
            /// <summary>
            /// Mojang
            /// </summary>
            moj
        }
#pragma warning restore 1591
    }
}
