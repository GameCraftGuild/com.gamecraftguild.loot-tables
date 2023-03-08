namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Interface for loot tables.
    /// </summary>
    public interface ILootTable {

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <returns>The item or null if there is no valid item.</returns>
        object GetLoot ();

    }

}