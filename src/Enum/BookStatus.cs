namespace TypeSafeEnumRef.Enum
{
    /// <summary>
    /// A books lifecycle status.
    /// </summary>
    public enum BookStatus
    {
        /// <summary>
        /// The book is orderer but not yet available but can be reserved for rent.
        /// </summary>
        Ordered = 1,

        /// <summary>
        /// The book is ordered and reserved for rent.
        /// </summary>
        Reserved = 2,

        /// <summary>
        /// The book is available for rent.
        /// </summary>
        Available = 3,

        /// <summary>
        /// The book is rented.
        /// </summary>
        Rented = 4,

        /// <summary>
        /// The book is lost
        /// </summary>
        Lost = 5     
    } 
}