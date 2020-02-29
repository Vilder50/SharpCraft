using System.Collections.Generic;
using SharpCraft.Data;
using System.Reflection;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for debug sticks
    /// </summary>
    public class DebugStick : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public DebugStick() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public DebugStick(ItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// List of properties this debug stick last edited.
        /// </summary>
        [DataTag("DebugProperty")]
        public StateList? LatestProperties { get; set; }

        /// <summary>
        /// Class for holding a list of states
        /// </summary>
        public class StateList : IConvertableToDataObject
        {
            private State[] states = null!;

            /// <summary>
            /// Intializes a new <see cref="StateList"/>
            /// </summary>
            /// <param name="states">states this list is holding</param>
            public StateList(State[] states)
            {
                States = states;
            }

            /// <summary>
            /// states this list is holding
            /// </summary>
            public State[] States { get => states; set => states = value ?? throw new System.ArgumentNullException(nameof(States), "States may not be null"); }

            /// <summary>
            /// Converts this object into a <see cref="Data.DataPartObject"/>
            /// </summary>
            /// <param name="conversionData">Not used</param>
            /// <returns>This object into a <see cref="Data.DataPartObject"/></returns>
            public DataPartObject GetAsDataObject(object?[]? conversionData)
            {
                DataPartObject returnObject = new DataPartObject();

                foreach(State state in States)
                {
                    returnObject.AddValue(new DataPartPath(state.StateOwner.Name, state.GetAsTag(null, null), true));
                }

                return returnObject;
            }
        }

        /// <summary>
        /// Class for holding a property a debug stick should remember
        /// </summary>
        public class State : IConvertableToDataTag
        {
            private BlockType stateOwner = null!;
            private PropertyInfo state = null!;

            private State()
            {
                
            }

            /// <summary>
            /// Intializes a new <see cref="State"/>
            /// </summary>
            /// <typeparam name="T">Block to get the state for</typeparam>
            /// <param name="stateOwner">The block id of the block holding the state</param>
            /// <param name="stateHoldingProperty">The state</param>
            /// <returns></returns>
            public static State New<T>(BlockType stateOwner, Expression<Func<T, object?>> stateHoldingProperty) where T : Block
            {
                State state = new State
                {
                    stateOwner = stateOwner
                };
                state.SetState<T>(stateHoldingProperty);
                return state;
            }

            /// <summary>
            /// The block type holding the property
            /// </summary>
            public BlockType StateOwner { get => stateOwner; set => stateOwner = value ?? throw new ArgumentNullException(nameof(StateOwner), "StateOwner may not be null"); }

            /// <summary>
            /// Sets the state saved in the debug stick
            /// </summary>
            /// <param name="stateHoldingProperty"></param>
            public void SetState<T>(Expression<Func<T, object?>> stateHoldingProperty) where T : Block
            {
                MemberExpression? memberExpression = stateHoldingProperty.Body as MemberExpression;
                UnaryExpression? unaryExpression = stateHoldingProperty.Body as UnaryExpression;
                PropertyInfo property = (PropertyInfo)(memberExpression ?? unaryExpression!.Operand as MemberExpression)!.Member;

                Blocks.BlockStateAttribute? stateInformation = (Blocks.BlockStateAttribute?)property.GetCustomAttribute(typeof(Blocks.BlockStateAttribute));
                if (stateInformation is null)
                {
                    throw new ArgumentException("The given property is not a state holding property");
                }
                state = property;
            }

            /// <summary>
            /// Returns the property holding the state
            /// </summary>
            /// <returns>Property holding the state</returns>
            public PropertyInfo GetState()
            {
                return state;
            }

            /// <summary>
            /// Converts this object into a <see cref="DataPartTag"/>
            /// </summary>
            /// <param name="asType">Not used</param>
            /// <param name="extraConversionData">Not used</param>
            /// <returns>This object into a <see cref="DataPartTag"/></returns>
            public DataPartTag GetAsTag(ID.NBTTagType? asType, object?[]? extraConversionData)
            {
                Blocks.BlockStateAttribute stateInformation = (Blocks.BlockStateAttribute?)state.GetCustomAttribute(typeof(Blocks.BlockStateAttribute))!;
                return new DataPartTag(stateInformation.DataName);
            }
        }
    }
}
