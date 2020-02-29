using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which gives/takes a recipe from one or more players
    /// </summary>
    public class RecipeCommand : BaseCommand
    {
        private IRecipe recipe = null!;
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="RecipeCommand"/>
        /// </summary>
        /// <param name="giveRecipe">True if the recipe should be given. False if it should be taken</param>
        /// <param name="recipe">The recipe to give/take</param>
        /// <param name="selector">Selector selecting the players to give/take the recipe from</param>
        public RecipeCommand(IRecipe recipe, BaseSelector selector, bool giveRecipe)
        {
            GiveRecipe = giveRecipe;
            Recipe = recipe;
            Selector = selector;
        }

        /// <summary>
        /// True if the recipe should be given. False if it should be taken
        /// </summary>
        public bool GiveRecipe { get; set; }

        /// <summary>
        /// The recipe to give/take
        /// </summary>
        public IRecipe Recipe { get => recipe; set => recipe = value ?? throw new ArgumentNullException(nameof(Recipe), "Recipe may not be null"); }

        /// <summary>
        /// Selector selecting the players to give/take the recipe from
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>recipe [GiveRecipe] [Selector] [Recipe]</returns>
        public override string GetCommandString()
        {
            return $"recipe {(GiveRecipe ? "give" : "take")} {Selector.GetSelectorString()} {Recipe.GetNamespacedName()}";
        }
    }

    /// <summary>
    /// Command which gives/takes all recipes from one or more players
    /// </summary>
    public class RecipeAllCommand : BaseCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="RecipeAllCommand"/>
        /// </summary>
        /// <param name="giveRecipe">True if the recipe should be given. False if it should be taken</param>
        /// <param name="selector">Selector selecting the players to give/take the recipe from</param>
        public RecipeAllCommand(BaseSelector selector, bool giveRecipe)
        {
            GiveRecipe = giveRecipe;
            Selector = selector;
        }

        /// <summary>
        /// True if the recipes should be given. False if they should be taken
        /// </summary>
        public bool GiveRecipe { get; set; }

        /// <summary>
        /// Selector selecting the players to give/take the recipes from
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>recipe [GiveRecipe] [Selector] *</returns>
        public override string GetCommandString()
        {
            return $"recipe {(GiveRecipe ? "give" : "take")} {Selector.GetSelectorString()} *";
        }
    }
}
