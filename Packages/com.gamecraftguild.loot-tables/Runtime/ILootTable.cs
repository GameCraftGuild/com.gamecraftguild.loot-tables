namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Interface for loot tables.
    /// </summary>
    public interface ILootTable<T> {

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <returns>The item or null if there is no valid item.</returns>
        T GetLoot (bool removeLoot = false);

        /// <summary>
        /// Reset the loot table to the original state.
        /// </summary>
        void ResetLootTable ();
    }

}