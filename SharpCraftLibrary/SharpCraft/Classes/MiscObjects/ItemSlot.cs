using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Slots
{
    /// <summary>
    /// Interface for slots which can hold items
    /// </summary>
    public interface IItemSlot
    {
        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        string GetSlotString();
    }

    /// <summary>
    /// Used for selecting an armor slot
    /// </summary>
    public class ArmorSlot : IItemSlot
    {
        /// <summary>
        /// Intializes a new <see cref="ArmorSlot"/>
        /// </summary>
        /// <param name="slot">The armor slot to select</param>
        public ArmorSlot(ID.ArmorSlot slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The armor slot to select
        /// </summary>
        public ID.ArmorSlot Slot { get; set; }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "armor." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting a weapon slot
    /// </summary>
    public class WeaponSlot : IItemSlot
    {
        /// <summary>
        /// Intializes a new <see cref="WeaponSlot"/>
        /// </summary>
        /// <param name="mainHand">True if it should select the main hand. False if it should select the off hand</param>
        public WeaponSlot(bool mainHand)
        {
            MainHand = mainHand;
        }

        /// <summary>
        /// True if it should select the main hand. False if it should select the off hand
        /// </summary>
        public bool MainHand { get; set; }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "weapon." + (MainHand ? "main" : "off") + "hand";
        }
    }

    /// <summary>
    /// Used for selecting a container slot (eg: chest slot)
    /// </summary>
    public class ContainerSlot : IItemSlot
    {
        private int slot;

        /// <summary>
        /// Intializes a new <see cref="ContainerSlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public ContainerSlot(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public int Slot 
        {
            get => slot;
            set
            {
                if (value < 0 || value > 53)
                {
                    throw new ArgumentOutOfRangeException(nameof(Slot), "Slot may not be less than 0 or higher than 53");
                }
                slot = value;
            }
        }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "container." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting an ender chest slot
    /// </summary>
    public class EnderChestSlot : IItemSlot
    {
        private int slot;

        /// <summary>
        /// Intializes a new <see cref="EnderChestSlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public EnderChestSlot(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public int Slot
        {
            get => slot;
            set
            {
                if (value < 0 || value > 26)
                {
                    throw new ArgumentOutOfRangeException(nameof(Slot), "Slot may not be less than 0 or higher than 26");
                }
                slot = value;
            }
        }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "enderchest." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting a hotbar slot
    /// </summary>
    public class HotbarSlot : IItemSlot
    {
        private int slot;

        /// <summary>
        /// Intializes a new <see cref="HotbarSlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public HotbarSlot(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public int Slot
        {
            get => slot;
            set
            {
                if (value < 0 || value > 8)
                {
                    throw new ArgumentOutOfRangeException(nameof(Slot), "Slot may not be less than 0 or higher than 8");
                }
                slot = value;
            }
        }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "hotbar." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting an inventory slot
    /// </summary>
    public class InventorySlot : IItemSlot
    {
        private int slot;

        /// <summary>
        /// Intializes a new <see cref="InventorySlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public InventorySlot(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public int Slot
        {
            get => slot;
            set
            {
                if (value < 0 || value > 26)
                {
                    throw new ArgumentOutOfRangeException(nameof(Slot), "Slot may not be less than 0 or higher than 26");
                }
                slot = value;
            }
        }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "inventory." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting a horse inventory slot
    /// </summary>
    public class HorseInventorySlot : IItemSlot
    {
        private int slot;

        /// <summary>
        /// Intializes a new <see cref="HorseInventorySlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public HorseInventorySlot(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public int Slot
        {
            get => slot;
            set
            {
                if (value < 0 || value > 26)
                {
                    throw new ArgumentOutOfRangeException(nameof(Slot), "Slot may not be less than 0 or higher than 14");
                }
                slot = value;
            }
        }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "horse." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting a villager inventory slot
    /// </summary>
    public class VillagerInventorySlot : IItemSlot
    {
        private int slot;

        /// <summary>
        /// Intializes a new <see cref="VillagerInventorySlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public VillagerInventorySlot(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public int Slot
        {
            get => slot;
            set
            {
                if (value < 0 || value > 7)
                {
                    throw new ArgumentOutOfRangeException(nameof(Slot), "Slot may not be less than 0 or higher than 7");
                }
                slot = value;
            }
        }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "villager." + Slot;
        }
    }

    /// <summary>
    /// Used for selecting a horse slot
    /// </summary>
    public class HorseSlot : IItemSlot
    {
        /// <summary>
        /// Intializes a new <see cref="HorseSlot"/>
        /// </summary>
        /// <param name="slot">The slot to select</param>
        public HorseSlot(ID.HorseSlot slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// The slot to select
        /// </summary>
        public ID.HorseSlot Slot { get; set; }

        /// <summary>
        /// Returns a string for selecting the slot
        /// </summary>
        /// <returns>A string for selecting the slot</returns>
        public string GetSlotString()
        {
            return "horse." + Slot;
        }
    }
}
