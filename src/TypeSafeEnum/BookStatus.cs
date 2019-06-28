using System;
using System.Collections.Generic;

namespace TypeSafeEnumRef.TypeSafeEnum
{
    /// <summary>
    /// This type is an example for the TypeSafeEnum Pattern.
    /// </summary>
    public abstract class BookStatus
    {
        #region Constants

        private const int OrderedId = 1;
        private const int ReservedId = 2;
        private const int AvailableId = 3;
        private const int RentedId = 4;
        private const int LostId = 5;

        #endregion

        #region Members

        private static Dictionary<int, BookStatus> Map = new Dictionary<int, BookStatus>
        {
            { OrderedId, new BookStatusOrdered() },
            { ReservedId, new BookStatusReserved() },
            { AvailableId, new BookStatusAvailable() },
            { RentedId, new BookStatusRented() },
            { LostId, new BookStatusLost() }
        };

        #endregion

        #region Constructors

        internal BookStatus(int value, bool isAvailableForRent)
        {
            Value = value;
            IsAvailableForRent = isAvailableForRent;
        }

        #endregion

        #region Static Properties

        public static BookStatus Ordered => Map[OrderedId];
        public static BookStatus Reserved => Map[ReservedId];
        public static BookStatus Available => Map[AvailableId];
        public static BookStatus Rented => Map[RentedId];
        public static BookStatus Lost => Map[LostId];

        #endregion

        #region Properties

        public int Value { get; }

        public bool IsAvailableForRent { get; }

        #endregion

        #region Public Static Methods

        public static IEnumerable<BookStatus> AsEnumerable() => Map.Values;

        #endregion

        #region Overloaded Operators

        public static implicit operator int(BookStatus bookStatus) => bookStatus.Value;
        
        public static implicit operator BookStatus(int value)
        {
            if (Map.ContainsKey(value))
            {
                return Map[value];
            }

            throw new InvalidCastException($"Invalid value: {value}");
        }

        public static implicit operator BookStatus(int? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return (int)value.Value;
        }

        #endregion

        #region Specific type implementations

        private sealed class BookStatusOrdered : BookStatus
        {
            public BookStatusOrdered() : base(OrderedId, true) { }
        }
        
        private sealed class BookStatusReserved : BookStatus
        {
            public BookStatusReserved() : base(ReservedId, false) { }
        }

        private sealed class BookStatusAvailable : BookStatus
        {
            public BookStatusAvailable() : base(AvailableId, true) { }
        }

        private sealed class BookStatusRented : BookStatus
        {
            public BookStatusRented() : base(RentedId, false) { }
        }

        private sealed class BookStatusLost : BookStatus
        {
            public BookStatusLost() : base(LostId, false) { }
        }

        #endregion
    }
}