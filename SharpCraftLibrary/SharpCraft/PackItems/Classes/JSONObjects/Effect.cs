using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft.JsonObjects
{
    /// <summary>
    /// a <see cref="object"/> defining an effect
    /// </summary>
    public class Effects : DataHolderBase
    {
        /// <summary>
        /// Intializes a new <see cref="Effects"/> object
        /// </summary>
        /// <param name="array">An array of effects to check for</param>
        public Effects(EffectArray array)
        {
            CheckEffects = array;
        }

        /// <summary>
        /// Intializes a new <see cref="Effects"/> object
        /// </summary>
        /// <param name="effects">a list of effects to check for</param>
        public Effects(List<Effect> effects)
        {
            CheckEffects = new EffectArray(effects);
        }

        /// <summary>
        /// Intializes a new <see cref="Effects"/> object
        /// </summary>
        /// <param name="effect">a single effect to check for</param>
        public Effects(Effect effect)
        {
            CheckEffects = new EffectArray(effect);
        }

        /// <summary>
        /// A list of effects to check for
        /// </summary>
        [DataTag(Merge = true, JsonTag = true)]
        public EffectArray CheckEffects { get; set; }

        /// <summary>
        /// A list of effects
        /// </summary>
        public class EffectArray : IConvertableToDataObject
        {
            /// <summary>
            /// Intializes a new <see cref="EffectArray"/>
            /// </summary>
            /// <param name="effects">The effects in the array</param>
            public EffectArray(List<Effect> effects)
            {
                Effects = effects;
            }

            /// <summary>
            /// Intializes a new <see cref="EffectArray"/>
            /// </summary>
            /// <param name="effect">The effect in the array</param>
            public EffectArray(Effect effect)
            {
                Effects = new List<Effect>() { effect };
            }

            /// <summary>
            /// The effects in the array
            /// </summary>
            public List<Effect> Effects { get; set; }

            /// <summary>
            /// Converts this object into a <see cref="DataPartObject"/>
            /// </summary>
            /// <param name="conversionData">Not in use</param>
            /// <returns>This object as a <see cref="DataPartObject"/></returns>
            public DataPartObject GetAsDataObject(object?[] conversionData)
            {
                DataPartObject dataObject = new DataPartObject();
                for (int i = 0; i < Effects.Count; i++)
                {
                    dataObject.AddValue(new DataPartPath(Effects[i].EffectName.ToString()!, Effects[i].GetDataTree(), true));
                }
                return dataObject;
            }
        }

        /// <summary>
        /// A single effect
        /// </summary>
        public class Effect : DataHolderBase
        {
            /// <summary>
            /// creates a new <see cref="Effect"/>
            /// </summary>
            /// <param name="Effect">the effect</param>
            public Effect(ID.Effect Effect)
            {
                EffectName = Effect;
            }

            /// <summary>
            /// The effect
            /// </summary>
            public ID.Effect? EffectName { get; set; }

            /// <summary>
            /// the <see cref="Effect"/>'s amplifer
            /// </summary>
            [DataTag("amplifier", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange? Amplifier { get; set; }

            /// <summary>
            /// the <see cref="Effect"/>'s duration
            /// </summary>
            [DataTag("duration", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange? Duration { get; set; }

            /// <summary>
            /// if the <see cref="Effect"/> is ambient or not
            /// </summary>
            [DataTag("ambient", JsonTag = true)]
            public bool? Ambient { get; set; }

            /// <summary>
            /// if the <see cref="Effect"/>'s particles are visible or not
            /// </summary>
            [DataTag("visible", JsonTag = true)]
            public bool? Visible { get; set; }
        }

        /// <summary>
        /// Converts a single <see cref="Effect"/> object into an <see cref="Effects"/> object
        /// </summary>
        /// <param name="effect">The <see cref="Effect"/> to convert</param>
        public static implicit operator Effects(Effect effect)
        {
            return new Effects(effect);
        }

        /// <summary>
        /// Converts a list of <see cref="Effect"/> objects into an <see cref="Effects"/> object
        /// </summary>
        /// <param name="effects">The <see cref="Effect"/> to convert</param>
        public static implicit operator Effects(List<Effect> effects)
        {
            return new Effects(effects);
        }
    }
}
