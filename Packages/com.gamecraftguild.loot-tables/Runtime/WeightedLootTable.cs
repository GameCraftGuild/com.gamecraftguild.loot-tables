using System.Collections.Generic;
using System.Linq;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Loot table with weights for each item. Selection chance is (item weight / sum of all item weights). Item weight can be decremented on drop to simulate each point of weight representing an individual item.
    /// </summary>
    public class WeightedLootTable : ILootTable {

        /// <summary>
        /// Dictionary containing loot and corresponding weight for each.
        /// </summary>
        private Dictionary<object, int> possibleLoot = new Dictionary<object, int>();

        /// <summary>
        /// Should the weight for a piece of loot be decremented when the loot is dropped.
        /// </summary>
        private bool removeLootOnDrop = false;

        /// <summary>
        /// Create a new WeightedLootTable. Items have a selection chance of (item weight / sum of all item weights).
        /// </summary>
        /// <param name="removeLootOnDrop">Should the weight for a piece of loot be decremented when the loot is dropped.</param>
        public WeightedLootTable (bool removeLootOnDrop = false) {
            this.removeLootOnDrop = removeLootOnDrop;
        }

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <returns>The item or null if there is no valid item.</returns>
        public object GetLoot () {
            object loot = possibleLoot.ToArray().RandomFromWeightedList();

            if (removeLootOnDrop) possibleLoot[loot] -= 1;

            return loot;
        }

        /// <summary>
        /// Add loot to the table.
        /// </summary>
        /// <param name="lootToAdd">Loot to add to the table.</param>
        /// <param name="weight">Weight of the loot.</param>
        /// <returns>True if <paramref name="lootToAdd" /> is added, false otherwise.</returns>
        public bool AddLootToTable (object lootToAdd, int weight) {
            if (possibleLoot.ContainsKey(lootToAdd)) {
                return false;
            }

            possibleLoot[lootToAdd] = weight;
            return true;
        }

        /// <summary>
        /// Remove loot from the table.
        /// </summary>
        /// <param name="lootToRemove">Loot to remove.</param>
        /// <returns>True if <paramref name="lootToRemove" /> is removed, false otherwise.</returns>
        public bool RemoveLootFromTable (object lootToRemove) {
            return possibleLoot.Remove(lootToRemove);
        }

        /// <summary>
        /// Modify the weight for an item in the table.
        /// </summary>
        /// <param name="lootToModify">Loot to modify the weight for.</param>
        /// <param name="newWeight">New weight for the item.</param>
        /// <returns>True if the weight for <paramref name="lootToModify" /> is changed to <paramref name="newWeight" />, false otherwise. </returns>
        public bool ModifyWeight (object lootToModify, int newWeight) {
            if (!possibleLoot.ContainsKey(lootToModify)) {
                return false;
            }

            possibleLoot[lootToModify] = newWeight;
            return true;
        }

    }

}