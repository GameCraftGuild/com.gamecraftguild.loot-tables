using GameCraftGuild.UnityExtensions.IList;
using System.Collections.Generic;
using System.Linq;

namespace GameCraftGuild.LootTables {

    /// <summary>
    /// Loot table where there are multiple tiers, each with a different loot table.
    /// </summary>
    public class TieredLootTables<T> : ILootTable<T> {

        /// <summary>
        /// Dictionary of tiers mapped to their loot tables.
        /// </summary>
        private Dictionary<string, ILootTable<T>> possibleTiers = new Dictionary<string, ILootTable<T>>();

        /// <summary>
        /// Dictionary of loot tables mapped to their probabilities.
        /// </summary>
        private Dictionary<ILootTable<T>, int> tierProbabilities = new Dictionary<ILootTable<T>, int>();

        /// <summary>
        /// Get loot from the table.
        /// </summary>
        /// <param name="removeLoot">Should the loot be removed.</param>
        /// <returns>The item or null if there is no valid item.</returns>
        public T GetLoot (bool removeLoot = false) {
            ILootTable<T> lootTableToUse = tierProbabilities.Keys.ToArray().GetRandomItemWeighted(tierProbabilities.Values.ToArray());

            return lootTableToUse.GetLoot(removeLoot);
        }

        /// <summary>
        /// Reset the loot table to the original state.
        /// </summary>
        public void ResetLootTable () {
            foreach (KeyValuePair<string, ILootTable<T>> tiers in possibleTiers) {
                tiers.Value.ResetLootTable();
            }
        }

        /// <summary>
        /// Add a tier to the loot table.
        /// </summary>
        /// <param name="tierToAdd">Tier to add to the loot table.</param>
        /// <param name="lootTableToAdd">Loot table associated with the <paramref name="tierToAdd" />.</param>
        /// <param name="probability">Probability for the tier.</param>
        /// <returns>True if <paramref name="tierToAdd" /> is added, false otherwise.</returns>
        public bool AddTier (string tierToAdd, ILootTable<T> lootTableToAdd, int probability) {
            if (possibleTiers.ContainsKey(tierToAdd)) {
                return false;
            }

            possibleTiers[tierToAdd] = lootTableToAdd;
            tierProbabilities[lootTableToAdd] = probability;
            return true;
        }

        /// <summary>
        /// Remove a tier from the loot table.
        /// </summary>
        /// <param name="tierToRemove">Tier to remove from the loot table.</param>
        /// <returns>True if <paramref name="tierToRemove" /> is removed, false otherwise.</returns>
        public bool RemoveTier (string tierToRemove) {
            if (!possibleTiers.ContainsKey(tierToRemove)) {
                return false;
            }

            tierProbabilities.Remove(possibleTiers[tierToRemove]);
            possibleTiers.Remove(tierToRemove);
            return true;
        }

        /// <summary>
        /// Replace the loot table for a tier.
        /// </summary>
        /// <param name="tierToModify">Tier to modify.</param>
        /// <param name="replacementLootTable">New loot table for <paramref name="tierToModify" />.</param>
        /// <returns>True if the loot table for <paramref name="tierToModify" /> is replaced with <paramref name="replacementLootTable" />, false otherwise.</returns>
        public bool ReplaceLootTableForTier (string tierToModify, ILootTable<T> replacementLootTable) {
            if (!possibleTiers.ContainsKey(tierToModify)) {
                return false;
            }

            tierProbabilities[replacementLootTable] = tierProbabilities[possibleTiers[tierToModify]]; // Add the new table and set the probability to the same as the old one.
            tierProbabilities.Remove(possibleTiers[tierToModify]); // Remove the old table
            possibleTiers[tierToModify] = replacementLootTable; // Change the table in the tier dictionary
            return true;
        }

        /// <summary>
        /// Change the probability for a tier.
        /// </summary>
        /// <param name="tierToModify">Tier to modify.</param>
        /// <param name="newProbability">New probability for <paramref name="tierToModify" />.</param>
        /// <returns>True if the probability for <paramref name="tierToModify" /> was changed to <paramref name="newProbability" />, false otherwise.</returns>
        public bool ChangeTierProbability (string tierToModify, int newProbability) {
            if (!possibleTiers.ContainsKey(tierToModify)) {
                return false;
            }

            tierProbabilities[possibleTiers[tierToModify]] = newProbability;
            return true;
        }

        /// <summary>
        /// Get the loot table associated with a given tier.
        /// </summary>
        /// <param name="tierToGet">Tier to get the loot table for.</param>
        /// <returns>The loot table associated with <paramref name="tierToGet" />, or null if the tier doesn't exist.</returns>
        public ILootTable<T> GetLootTableForTier (string tierToGet) {
            if (!possibleTiers.ContainsKey(tierToGet)) {
                return null;
            }

            return possibleTiers[tierToGet];
        }

    }

}