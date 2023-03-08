using System.Collections.Generic;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Loot table with each level having different loot tables.
    /// </summary>
    public class LevelableTieredLootTables : ILootTable {

        /// <summary>
        /// Dictionary mapping levels to loot tables.
        /// </summary>
        private Dictionary<int, ILootTable> possibleLevels = new Dictionary<int, ILootTable>();

        /// <summary>
        /// Current level of the loot table.
        /// </summary>
        private int currentLevel = 0;

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <returns>The item or null if there is no valid item or there is no loot table for the current level.</returns>
        public object GetLoot () {
            if (!possibleLevels.ContainsKey(currentLevel)) {
                return null;
            }

            return possibleLevels[currentLevel].GetLoot();
        }

        /// <summary>
        /// Add a new level to the loot table.
        /// </summary>
        /// <param name="levelToAdd">Level to add to the loot table.</param>
        /// <param name="lootTableToAdd">Loot table associated with the <paramref name="levelToAdd" />.</param>
        /// <returns>True if <paramref name="levelToAdd" /> is added, false otherwise.</returns>
        public bool AddLevel (int levelToAdd, ILootTable lootTableToAdd) {
            if (possibleLevels.ContainsKey(levelToAdd)) {
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
            return possibleLevels.Remove(levelToRemove);
        }

        /// <summary>
        /// Replace the loot table for a level.
        /// </summary>
        /// <param name="levelToModify">Level to modify.</param>
        /// <param name="replacementLootTable">New loot table for <paramref name="levelToModify" />.</param>
        /// <returns>True if the loot table for <paramref name="levelToModify" /> is replaced with <paramref name="replacementLootTable" />, false otherwise.</returns>
        public bool ReplaceLootTableForLevel (int levelToModify, ILootTable replacementLootTable) {
            if (!possibleLevels.ContainsKey(levelToModify)) {
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
        public ILootTable GetLootTableForLevel (int levelToGet) {
            if (!possibleLevels.ContainsKey(levelToGet)) {
                return null;
            }

            return possibleLevels[levelToGet];
        }

        /// <summary>
        /// Get the loot table for the current level.
        /// </summary>
        /// <returns>Loot table associated with the current level or null if the level doesn't have a loot table.</returns>
        public ILootTable GetCurrentLootTable () {
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
            return possibleLevels.ContainsKey(currentLevel) && possibleLevels[currentLevel] != null;
        }

        /// <summary>
        /// Increment the current level by 1.
        /// </summary>
        public void AdvanceLevel () {
            currentLevel++;
        }

        /// <summary>
        /// Set the current level to a given level.
        /// </summary>
        /// <param name="newLevel">Level to set the current level to.</param>
        public void SetLevel (int newLevel) {
            currentLevel = newLevel;
        }

    }

}