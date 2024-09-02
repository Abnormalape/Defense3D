namespace BHSSolo.DungeonDefense.UI
{
    /// <summary>
    /// UI base interface. Interact with Canvas
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// Canvas's Sorting Order
        /// </summary>
        int sortingOrder { get; set; }

        /// <summary>
        /// Enable action depends on user's input.
        /// </summary>
        bool inputActionEnabled { get; set; }

        /// <summary>
        /// Enable Canvas
        /// </summary>
        void Show();

        /// <summary>
        /// Disable Canvas
        /// </summary>
        void Hide();
    }
}
