using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for storing the result of the command
    /// </summary>
    public class ExecuteStoreBossbar : BaseExecuteStoreCommand
    {
        private BossBar bossbar;

        /// <summary>
        /// Intializes a new <see cref="ExecuteStoreBossbar"/> command
        /// </summary>
        /// <param name="bossbar">The <see cref="BossBar"/> to store the result in</param>
        /// <param name="storeAsValue">True if it should store it as the bossbar's value. False if it should store it as it's max value</param>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public ExecuteStoreBossbar(BossBar bossbar, bool storeAsValue, bool storeResult = true) : base(storeResult)
        {
            Bossbar = bossbar;
            StoreAsValue = storeAsValue;
        }

        /// <summary>
        /// The <see cref="BossBar"/> to store the result in
        /// </summary>
        public BossBar Bossbar 
        {
            get => bossbar;
            set
            {
                bossbar = value ?? throw new ArgumentNullException(nameof(bossbar), "Bossbar may not be null.");
            }
        }

        /// <summary>
        /// True if it should store it as the bossbar's value. False if it should store it as it's max value
        /// </summary>
        public bool StoreAsValue { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>bossbar [Bossbar] [StoreAsValue]</returns>
        protected override string GetStorePart()
        {
            return "bossbar " + Bossbar + " " + (StoreAsValue ? "value" : "max");
        }
    }
}
