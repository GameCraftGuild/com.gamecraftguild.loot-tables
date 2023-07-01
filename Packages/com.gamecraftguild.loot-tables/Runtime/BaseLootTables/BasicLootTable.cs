using System.Collections.Generic;
using System.Linq;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// A basic loot loot table. All items have an equal chance of being selected. Items can be removed when they are selected. Duplicates are not allowed.
    /// </summary>
    public class BasicLootTable<T> : ILootTable<T> {

        /// <summary>
        /// All the possible loot options.
        /// </summary>
        private HashSet<T> possibleLoot = new HashSet<T>();

        /// <summary>
        /// Loot that has been removed from the loot table.
        /// </summary>
        private HashSet<T> removedLoot = new HashSet<T>();

        /// <summary>
        /// Create a new BasicLootTable. Items have an equal chance of being selected.
        /// </summary>
        public BasicLootTable () {
        }

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <param name="removeLoot">Should the loot be removed.</param>
        /// <returns>The item or null if there is no valid item.</returns>
        public T GetLoot (bool removeLoot = false) {
            T loot = possibleLoot.ToArray().RandomFromList();

            if (removeLoot && RemoveLootFromTable(loot)) removedLoot.Add(loot); 

            return loot;
        }

        /// <summary>
        /// Reset the loot table to the original state.
        /// </summary>
        public void ResetLootTable() {
            possibleLoot.UnionWith(removedLoot);
            removedLoot.Clear();
        }

        /// <summary>
        /// Add loot to the table.
        /// </summary>
        /// <param name="lootToAdd">Loot to add to the table.</param>
        /// <returns>True if <paramref name="lootToAdd" /> was added, false otherwise.</returns>
        public bool AddLootToTable (T lootToAdd) {
            return possibleLoot.Add(lootToAdd);
        }

        /// <summary>
        /// Remove loot from the table.
        /// </summary>
        /// <param name="lootToRemove">Loot to remove from the table.</param>
        /// <returns>True if <paramref name="lootToRemove" /> was removed, false otherwise.</returns>
        public bool RemoveLootFromTable (T lootToRemove) {
            return possibleLoot.Remove(lootToRemove);
        }

    }

}