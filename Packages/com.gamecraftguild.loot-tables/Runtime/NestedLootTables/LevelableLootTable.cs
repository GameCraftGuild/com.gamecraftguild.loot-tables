using System.Collections.Generic;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Loot table with each level having different loot tables.
    /// </summary>
    public class LevelableLootTable<T> : ILootTable<T> {

        /// <summary>
        /// Dictionary mapping levels to loot tables.
        /// </summary>
        //private Dictionary<int, ILootTable<T>> possibleLevels = new Dictionary<int, ILootTable<T>>();
        private ILootTable<T>[] possibleLevels;

        /// <summary>
        /// Current level of the loot table.
        /// </summary>
        private int currentLevel = 0;

        /// <summary>
        /// Max level for the loot table.
        /// </summary>
        private int maxLevel;

        /// <summary>
        /// Create a loot table with different loot tables for different levels.
        /// </summary>
        /// <param name="maxLevel">Max level for the loot table.</param>
        public LevelableLootTable(int maxLevel) {
            this.maxLevel = maxLevel;
            possibleLevels = new ILootTable<T>[maxLevel];
        }

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <param name="removeLoot">Should the loot be removed.</param>
        /// <returns>The item or default(T) if there is no valid item or there is no loot table for the current level.</returns>
        public T GetLoot (bool removeLoot) {
            if (possibleLevels[currentLevel] == null) {
                return default(T);
            }

            return possibleLevels[currentLevel].GetLoot(removeLoot);
        }

        /// <summary>
        /// Reset the loot table to the original state, including the level.
        /// </summary>
        public void ResetLootTable() {
            ResetLevel();
            foreach (ILootTable<T> lootTable in possibleLevels) {
                if (lootTable != null) {
                    lootTable.ResetLootTable();
                }
            }
        }

        /// <summary>
        /// Add a new level to the loot table.
        /// </summary>
        /// <param name="levelToAdd">Level to add to the loot table.</param>
        /// <param name="lootTableToAdd">Loot table associated with the <paramref name="levelToAdd" />.</param>
        /// <returns>True if <paramref name="levelToAdd" /> is added, false otherwise.</returns>
        public bool AddLevel (int levelToAdd, ILootTable<T> lootTableToAdd) {
            if (possibleLevels[levelToAdd] != null){
                return false;
            }

            possibleLevels[levelToAdd] = lootTableToAdd;
            return true;
        }

        /// <summary>
        /// Remove a level from the loot table.
        /// </summary>
        /// <param name="levelToRemove">Level to remove from the loot table.</param>
        /// <returns>True if <paramref name="levelToRemove" /> is removed, false otherwise.</returns>
        public bool RemoveLevel (int levelToRemove) {
            bool removed = possibleLevels[levelToRemove] != null;
            if (removed) possibleLevels[levelToRemove] = null;
            return removed;
        }

        /// <summary>
        /// Replace the loot table for a level.
        /// </summary>
        /// <param name="levelToModify">Level to modify.</param>
        /// <param name="replacementLootTable">New loot table for <paramref name="levelToModify" />.</param>
        /// <returns>True if the loot table for <paramref name="levelToModify" /> is replaced with <paramref name="replacementLootTable" />, false otherwise.</returns>
        public bool ReplaceLootTableForLevel (int levelToModify, ILootTable<T> replacementLootTable) {
            if (possibleLevels[levelToModify] == null) {
                return false;
            }

            possibleLevels[levelToModify] = replacementLootTable;
            return true;
        }

        /// <summary>
        /// Get the loot table associated with a given level.
        /// </summary>
        /// <param name="levelToGet">Level to get the loot table for.</param>
        /// <returns>Loot table associated with <paramref name="levelToGet" /> or null if the level doesn't have a loot table.</returns>
        public ILootTable<T> GetLootTableForLevel (int levelToGet) {
            return possibleLevels[levelToGet];
        }

        /// <summary>
        /// Get the loot table for the current level.
        /// </summary>
        /// <returns>Loot table associated with the current level or null if the level doesn't have a loot table.</returns>
        public ILootTable<T> GetCurrentLootTable () {
            return GetLootTableForLevel(currentLevel);
        }

        /// <summary>
        /// Get the current level of the loot table.
        /// </summary>
        /// <returns>The current level of the loot table.</returns>
        public int GetCurrentLevel () {
            return currentLevel;
        }

        /// <summary>
        /// Does the current level have a loot table.
        /// </summary>
        /// <returns>True if there is a loot table for the current level, false otherwise.</returns>
        public bool HasTableForCurrentLevel () {
            return possibleLevels[currentLevel] != null;
        }

        /// <summary>
        /// Increment the current level by 1 if the current level is less than the max level.
        /// </summary>
        /// <returns>True if the level was advanced and false otherwise.</returns>
        public bool AdvanceLevel () {
            if (currentLevel < maxLevel) {
                currentLevel++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set the current level to a given level.
        /// </summary>
        /// <param name="newLevel">Level to set the current level to.</param>
        /// <returns>True if the level was set to the new level, false otherwise.</returns>
        public bool SetLevel (int newLevel) {
            if (newLevel <= maxLevel) {
                currentLevel = newLevel;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set the current level to 0;
        /// </summary>
        public void ResetLevel () {
            currentLevel = 0;
        }

    }

}