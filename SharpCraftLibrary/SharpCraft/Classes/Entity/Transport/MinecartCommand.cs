using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for command block minecarts
        /// </summary>
        public class MinecartCommand : BaseMinecart
        {
            /// <summary>
            /// Creates a new command block minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartCommand(ID.Entity? type = ID.Entity.command_block_minecart) : base(type) { }

            /// <summary>
            /// The command to run
            /// </summary>
            [Data.DataTag]
            public string Command { get; set; }
            /// <summary>
            /// The command's text output
            /// </summary>
            [Data.DataTag]
            public string LastOutput { get; set; }
            /// <summary>
            /// The command's output
            /// </summary>
            [Data.DataTag]
            public int? SuccessCount { get; set; }
            /// <summary>
            /// Makes it so last output will be stored in <see cref="LastOutput"/>
            /// </summary>
            [Data.DataTag]
            public bool? TrackOutput { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MinecartDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Command != null) { TempList.Add("Command:\"" + Command.Escape() + "\""); }
                    if (LastOutput != null) { TempList.Add("LastOutput:\"" + LastOutput.Escape() + "\""); }
                    if (SuccessCount != null) { TempList.Add("SuccessCount:" + SuccessCount); }
                    if (TrackOutput != null) { TempList.Add("TrackOutput:" + TrackOutput.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
