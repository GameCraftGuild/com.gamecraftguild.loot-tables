using System.Collections.Generic;
using System.Linq;
using GameCraftGuild.UnityExtensions.IList;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Loot table with weights for each item. Selection chance is (item weight / sum of all item weights). Item weight can be decremented on drop to simulate each point of weight representing an individual item.
    /// </summary>
    public class WeightedLootTable<T> : ILootTable<T> {

        /// <summary>
        /// Dictionary containing loot and corresponding weight for each.
        /// </summary>
        private Dictionary<T, int> possibleLoot = new Dictionary<T, int>();

        /// <summary>
        /// Dictionary containing loot removed from the table.
        /// </summary>
        private Dictionary<T, int> removedLoot = new Dictionary<T, int>();

        /// <summary>
        /// Create a new WeightedLootTable. Items have a selection chance of (item weight / sum of all item weights).
        /// </summary>
        public WeightedLootTable () {
        }

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <param name="removeLoot">Should the loot be removed.</param>
        /// <returns>The item or null if there is no valid item.</returns>
        public T GetLoot (bool removeLoot = false) {
            T loot = possibleLoot.Keys.ToArray().GetRandomItemWeighted(possibleLoot.Values.ToArray());
            if (removeLoot) {
                possibleLoot[loot] -= 1;
                if (!removedLoot.ContainsKey(loot)) {
                    removedLoot.Add(loot, 1);
                } else {
                    removedLoot[loot] += 1;
                }
            }

            return loot;
        }

        /// <summary>
        /// Reset the loot table to the original state.
        /// </summary>
        public void ResetLootTable () {
            foreach (KeyValuePair<T, int> removedLoot in removedLoot) {
                if (possibleLoot.ContainsKey (removedLoot.Key)) {
                    possibleLoot[removedLoot.Key] += removedLoot.Value;
                }
            }
            removedLoot.Clear ();
        }

        /// <summary>
        /// Add loot to the table.
        /// </summary>
        /// <param name="lootToAdd">Loot to add to the table.</param>
        /// <param name="weight">Weight of the loot.</param>
        /// <returns>True if <paramref name="lootToAdd" /> is added, false otherwise.</returns>
        public bool AddLootToTable (T lootToAdd, int weight) {
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
        public bool RemoveLootFromTable (T lootToRemove) {
            bool removed = possibleLoot.Remove(lootToRemove);
            if (removed) {
                removedLoot.Remove(lootToRemove);
            }
            return removed;
        }

        /// <summary>
        /// Modify the weight for an item in the table. This will reset the initial weight for the item.
        /// </summary>
        /// <param name="lootToModify">Loot to modify the weight for.</param>
        /// <param name="newWeight">New weight for the item.</param>
        /// <returns>True if the weight for <paramref name="lootToModify" /> is changed to <paramref name="newWeight" />, false otherwise. </returns>
        public bool ModifyWeight (T lootToModify, int newWeight) {
            if (!possibleLoot.ContainsKey(lootToModify)) {
                return false;
            }

            possibleLoot[lootToModify] = newWeight;

            if (removedLoot.ContainsKey (lootToModify)) {
                removedLoot[lootToModify] = 0;
            }

            return true;
        }

    }

}