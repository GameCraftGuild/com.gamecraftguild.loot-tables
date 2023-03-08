using System.Collections.Generic;
using System.Linq;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// A basic loot loot table. All items have an equal chance of being selected. Items can be removed when they are selected. Duplicates are not allowed.
    /// </summary>
    public class BasicLootTable : ILootTable {

        /// <summary>
        /// All the possible loot options.
        /// </summary>
        private HashSet<object> possibleLoot = new HashSet<object>();

        /// <summary>
        /// Should loot be removed from the table when it is selected.
        /// </summary>
        private bool removeLootOnDrop = false;

        /// <summary>
        /// Create a new BasicLootTable. Items have an equal chance of being selected.
        /// </summary>
        /// <param name="removeLootOnDrop">Should loot be removed from the table when it is selected.</param>
        public BasicLootTable (bool removeLootOnDrop = false) {
            this.removeLootOnDrop = removeLootOnDrop;
        }

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <returns>The item or null if there is no valid item.</returns>
        public object GetLoot () {
            object loot = possibleLoot.ToArray().RandomFromList();

            if (removeLootOnDrop) RemoveLootFromTable(loot);

            return loot;
        }

        /// <summary>
        /// Add loot to the table.
        /// </summary>
        /// <param name="lootToAdd">Loot to add to the table.</param>
        /// <returns>True if <paramref name="lootToAdd" /> was added, false otherwise.</returns>
        public bool AddLootToTable (object lootToAdd) {
            return possibleLoot.Add(lootToAdd);
        }

        /// <summary>
        /// Remove loot from the table.
        /// </summary>
        /// <param name="lootToRemove">Loot to remove from the table.</param>
        /// <returns>True if <paramref name="lootToRemove" /> was removed, false otherwise.</returns>
        public bool RemoveLootFromTable (object lootToRemove) {
            return possibleLoot.Remove(lootToRemove);
        }

    }

}