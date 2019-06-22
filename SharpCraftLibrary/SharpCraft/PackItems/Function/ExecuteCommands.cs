using System.Linq;
using System;
using System.Collections.Generic;

namespace SharpCraft
{
    public partial class Function
    {
        /// <summary>
        /// All the execute commands
        /// </summary>
        public class ExecuteCommands
        {
            FunctionWriter Writer;

            internal ExecuteCommands(FunctionWriter commandsList)
            {
                Writer = commandsList;
            }

            /// <summary>
            /// Marks that the execute command shouldnt have any run command
            /// </summary>
            public void Stop()
            {
                Writer.NewLine();
            }

            /// <summary>
            /// Aligns the execute coordinates on the given axis
            /// </summary>
            /// <param name="axis">The axis to align to insert x and/or z and/or y</param>
            public void Align(string axis)
            {
                axis = axis.ToLower();
                if (string.IsNullOrWhiteSpace(axis))
                {
                    throw new ArgumentNullException(nameof(axis) + " cannot be empty");
                }
                else if (!axis.All(letter => letter == 'x' || letter == 'y' || letter == 'z'))
                {
                    throw new ArgumentException(nameof(axis) + " only allows xyz");
                }
                Writer.Add("align " + axis + " ", true);
            }

            /// <summary>
            /// Auto aligns the execute coordinates to all the axis
            /// </summary>
            /// <param name="center">True if it should center to the block</param>
            public void Align(bool center = false)
            {
                Writer.Add("align xyz ", true);
                if (center)
                {
                    Positioned(new Coords(0.5, 0.5, 0.5));
                }
            }

            /// <summary>
            /// Executes at the given <see cref="Selector"/>
            /// </summary>
            /// <param name="atEntity">The <see cref="Selector"/> to execute at</param>
            public void At(Selector atEntity)
            {
                if(atEntity is null)
                {
                    throw new ArgumentNullException(nameof(atEntity) + " cannot be null");
                }
                Writer.Add("at " + atEntity + " ", true);
            }

            /// <summary>
            /// Executes at using the @s <see cref="Selector"/>
            /// </summary>
            public void At()
            {
                Writer.Add("at @s ", true);
            }

            /// <summary>
            /// Executes as the given <see cref="Selector"/>
            /// </summary>
            /// <param name="asEntity">The <see cref="Selector"/> to execute as</param>
            public void As(Selector asEntity)
            {
                if (asEntity is null)
                {
                    throw new ArgumentNullException(nameof(asEntity) + " cannot be null");
                }
                Writer.Add("as " + asEntity + " ", true);
            }

            /// <summary>
            /// Executes if the given <see cref="Block"/> is at the <see cref="Coords"/>
            /// </summary>
            /// <param name="blockCoords">the <see cref="Coords"/> of the block</param>
            /// <param name="findBlock">the <see cref="Block"/> to find</param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfBlock(Block findBlock, Coords blockCoords, bool want = true)
            { 
                Writer.Add((want ? "if" : "unless") + " block " + blockCoords + " " + findBlock + " ", true);
            }

            /// <summary>
            /// Executes if the <see cref="Block"/>s between the 2 corners are the same as the <see cref="Block"/>s at the <paramref name="testCoords"/>
            /// </summary>
            /// <param name="corner1">The first corner</param>
            /// <param name="corner2">The second corner</param>
            /// <param name="testCoords">The coordinate to check at</param>
            /// <param name="masked">true if it should ignore air blocks</param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfBlocks(Coords corner1, Coords corner2, Coords testCoords, bool masked = false, bool want = true)
            {
                Writer.Add((want ? "if" : "unless") + " blocks " + corner1 + " " + corner2 + " " + testCoords + " " + (masked ? "masked" : "all") + " ", true);
            }

            /// <summary>
            /// Executes if the <paramref name="entitySelector"/> finds an <see cref="Entity"/>
            /// </summary>
            /// <param name="entitySelector">The <see cref="Selector"/> used to search for entities</param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfEntity(Selector entitySelector, bool want = true)
            {
                Writer.Add((want ? "if" : "unless") + " entity " + entitySelector + " ", true);
            }

            /// <summary>
            /// Executes if the <see cref="Entity"/> selected with <paramref name="dataPath"/> has the given datapath
            /// </summary>
            /// <param name="entitySelector">The <see cref="Selector"/> which selects the entity</param>
            /// <param name="dataPath">The datapath the entity should contain</param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfData(Selector entitySelector, string dataPath, bool want = true)
            {
                entitySelector.Limit = 1;
                Writer.Add($"{(want ? "if" : "unless")} data entity {entitySelector} {dataPath} ",true);
            }

            /// <summary>
            /// Executes if the <see cref="Block"/> at the <see cref="Coords"/> has the given datapath
            /// </summary>
            /// <param name="block">the <see cref="Coords"/> of the <see cref="Block"/></param>
            /// <param name="dataPath">The datapath the entity should contain</param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfData(Coords block, string dataPath, bool want = true)
            {
                Writer.Add($"{(want ? "if" : "unless")} data block {block} {dataPath} ", true);
            }

            /// <summary>
            /// Executes if the <paramref name="mainSelector"/>'s score value is <paramref name="operation"/> than <paramref name="otherSelector"/>'s score value
            /// </summary>
            /// <param name="mainSelector">The first <see cref="Selector"/></param>
            /// <param name="mainObject">The first <see cref="Selector"/>'s <see cref="ScoreObject"/></param>
            /// <param name="operation">The operation used to check the scores</param>
            /// <param name="otherSelector">The second <see cref="Selector"/></param>
            /// <param name="otherObject">The second <see cref="Selector"/>'s <see cref="ScoreObject"/></param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfScore(Selector mainSelector, ScoreObject mainObject, ID.IfScoreOperation operation,Selector otherSelector, ScoreObject otherObject, bool want = true)
            {
                mainSelector.Limit = 1;
                otherSelector.Limit = 1;
                string OperationString;

                switch (operation)
                {
                    case ID.IfScoreOperation.Equel:
                        OperationString = "=";
                        break;
                    case ID.IfScoreOperation.Higher:
                        OperationString = ">";
                        break;
                    case ID.IfScoreOperation.HigherOrEquel:
                        OperationString = ">=";
                        break;
                    case ID.IfScoreOperation.Smaller:
                        OperationString = "<";
                        break;
                    case ID.IfScoreOperation.SmallerOrEquel:
                        OperationString = "<=";
                        break;

                    default:
                        OperationString = "=";
                        break;
                }
                Writer.Add((want ? "if" : "unless") + " score " + mainSelector + " " + mainObject + " " + OperationString + " " + otherSelector + " " + otherObject + " ", true);

            }

            /// <summary>
            /// Executes if the given <see cref="Selector"/>'s score is in the given <see cref="Range"/>
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/>'s score to check</param>
            /// <param name="scoreObject">the <see cref="ScoreObject"/> to containing the score</param>
            /// <param name="range">the <see cref="Range"/> the score should be in</param>
            /// <param name="want">false if it should execute when it's false</param>
            public void IfScore(Selector selector, ScoreObject scoreObject, Range range, bool want = true)
            {
                selector.Limit = 1;
                string Unless = "if";
                if (!want) { Unless = "unless"; }

                Writer.Add(Unless + " score " + selector + " " + scoreObject + " matches " + range.SelectorString() + " ", true);

            }

            /// <summary>
            /// Executes at the given position
            /// </summary>
            /// <param name="position">the <see cref="Coords"/> to execute at</param>
            public void Positioned(Coords position)
            {
                Writer.Add("positioned " + position + " ", true);
            }

            /// <summary>
            /// Executes at the given <see cref="Selector"/>'s <see cref="Coords"/>
            /// </summary>
            /// <param name="entity">The <see cref="Selector"/> to execute at</param>
            public void Positioned(Selector entity)
            {
                Writer.Add("positioned as " + entity + " ", true);
            }

            /// <summary>
            /// Stores the command's success output inside the <see cref="Entity"/>
            /// </summary>
            /// <param name="entity">the <see cref="Selector"/> which choses the <see cref="Entity"/></param>
            /// <param name="dataPath">the datapath to store the output in</param>
            /// <param name="dataType">the path to the place to store the score</param>
            /// <param name="scale">the number the output should be multiplied with before being inserted</param>
            /// <param name="storeSucces">true if it only should store if the command was successfull</param>
            public void Store(Selector entity,string dataPath, ID.StoreTypes dataType, double scale = 1, bool storeSucces = false)
            {
                entity.Limit = 1;
                Writer.Add("store " + (storeSucces ? "success" : "result") + " entity " + entity + " " + dataPath + " " + dataType.ToString().ToLower() + " " + scale.ToMinecraftDouble() + " ", true);
            }

            /// <summary>
            /// Stores the command's success output inside the <see cref="Block"/>
            /// </summary>
            /// <param name="blockCoords">the <see cref="Coords"/> of the <see cref="Block"/> to store the output in</param>
            /// <param name="dataPath">the datapath to store the output in</param>
            /// <param name="dataType">the path to the place to store the score</param>
            /// <param name="scale">the number the output should be multiplied with before being inserted</param>
            /// <param name="storeSucces">true if it only should store if the command was successfull</param>
            public void Store(Coords blockCoords, string dataPath, ID.StoreTypes dataType, double scale = 1, bool storeSucces = false)
            {
                Writer.Add("store " + (storeSucces ? "success" : "result") + " block " + blockCoords + " " + dataPath + " " + dataType.ToString().ToLower() + " " + scale.ToMinecraftDouble() + " ", true);
            }

            /// <summary>
            /// Stores the command's success output inside the given <see cref="Entity"/>'s <see cref="ScoreObject"/>
            /// </summary>
            /// <param name="entity">The <see cref="Entity"/> to store in</param>
            /// <param name="scoreObject">The <see cref="ScoreObject"/> to store in</param>
            /// <param name="storeSucces">true if it only should store if the command was successfull</param>
            public void Store(Selector entity, ScoreObject scoreObject, bool storeSucces = false)
            {
                Writer.Add("store " + (storeSucces ? "success" : "result") + " score " + entity + " " + scoreObject + " ", true);
            }

            /// <summary>
            /// Stores the command's success output inside the given <see cref="BossBar"/>
            /// </summary>
            /// <param name="bossBar">The <see cref="BossBar"/> to store the output in</param>
            /// <param name="value">true if it should store the output in the value, false if it should store it as maxvalue</param>
            /// <param name="storeSucces">true if it only should store if the command was successfull</param>
            public void Store(BossBar bossBar, bool value = true, bool storeSucces = false)
            {
                Writer.Add("store " + (storeSucces ? "success" : "result") + " bossbar " + bossBar + " " + (value ? "value" : "max") + " ", true);
            }

            /// <summary>
            /// Executes rotated in the direction of the given <see cref="Entity"/>
            /// </summary>
            /// <param name="entity">the <see cref="Entity"/> it should be rotated at</param>
            /// <param name="facing">the part of the <see cref="Entity"/> to be faced at</param>
            public void Facing(Selector entity, ID.FacingAnchor facing = ID.FacingAnchor.feet)
            {
                Writer.Add("facing entity " + entity + " " + facing + " ",true);
            }

            /// <summary>
            /// Executes rotated in the direction of the given <see cref="Coords"/>
            /// </summary>
            /// <param name="coords">the <see cref="Coords"/> to be rotated at</param>
            public void Facing(Coords coords)
            {
                Writer.Add("facing " + coords + " ", true);
            }

            /// <summary>
            /// Executes rotated
            /// </summary>
            /// <param name="rotation">the <see cref="Rotation"/> to execute with</param>
            public void Rotated(Rotation rotation)
            {
                Writer.Add("rotated " + rotation + " ", true);
            }

            /// <summary>
            /// Executes rotated as the given <see cref="Entity"/>
            /// </summary>
            /// <param name="entity">the <see cref="Entity"/> to execute rotated as</param>
            public void Rotated(Selector entity)
            {
                Writer.Add("rotated as " + entity + " ", true);
            }

            /// <summary>
            /// Executes in the given dimension
            /// </summary>
            /// <param name="dimension">The dimension</param>
            public void Dimension(ID.Dimension dimension)
            {
                Writer.Add("in " + dimension + " ", true);
            }

            /// <summary>
            /// The place to anchor local <see cref="Coords"/> at 
            /// </summary>
            /// <param name="Anchor">The place</param>
            public void Anchored(ID.FacingAnchor Anchor)
            {
                Writer.Add("anchored " + Anchor + " ", true);
            }
        }
    }
}
