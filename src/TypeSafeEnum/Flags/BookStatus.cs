using System;
using System.Collections.Generic;
using System.Linq;

namespace TypeSafeEnumRef.TypeSafeEnum.Flags
{
    /// <summary>
    /// This type is an example for the TypeSafeEnum Pattern with Flags semantics.
    /// </summary>
    public abstract class BookStatus
    {
        #region Constants

        private const int OrderedId = 1;
        private const int ReservedId = 2;
        private const int AvailableId = 4;
        private const int RentedId = 8;
        private const int LostId = 16;

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

        #region Overloaded Operators

        public static implicit operator int(BookStatus bookStatus) => bookStatus.Value;
        
        public static implicit operator BookStatus(int value)
        {
            if (Map.ContainsKey(value))
            {
                return Map[value];
            }

            IEnumerable<BookStatus> items = Unfold(value);

            return new Flags(items);
        }

        public static implicit operator BookStatus(int? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            if (Map.ContainsKey(value.Value))
            {
                return Map[value.Value];
            }

            IEnumerable<BookStatus> items = Unfold(value.Value);

            return new Flags(items);
        }

        public static BookStatus operator |(BookStatus item1, BookStatus item2)
        {
            return new Flags(new[] { item1, item2 });
        }

        public static BookStatus operator &(BookStatus item1, BookStatus item2)
        {
            IEnumerable<BookStatus> items1 = Unfold(item1);
            IEnumerable<BookStatus> items2 = Unfold(item2);

            IEnumerable<BookStatus> intersection = items1.Intersect(items2);
            if (!intersection.Any())
            {
                return null;
            }

            return new Flags(intersection);
        }

        public static bool operator ==(BookStatus item1, BookStatus item2)
        {
            return item1.Value == item2.Value;
        }

        public static bool operator !=(BookStatus item1, BookStatus item2)
        {
            return !(item1 == item2);
        }

        #endregion

        #region Public Static Methods

        public static IEnumerable<BookStatus> AsEnumerable() => Map.Values;

        #endregion

        #region Public Methods

        public override int GetHashCode() => this.Value.GetHashCode();
        
        public override bool Equals(object obj)
        {
            if (obj is BookStatus other)
            {
                return this.Value == other.Value;
            }

            return false;
        }

        #endregion

        #region Private Static Methods

        private static List<BookStatus> Unfold(int itemValue)
        {
            var result = new List<BookStatus>();

            int remainder = itemValue;
            foreach (int knownItem in AsEnumerable())
            {
                bool isSet = (remainder & knownItem) != 0;
                if (isSet)
                {
                    result.Add(knownItem);
                    remainder = remainder ^ knownItem;
                }
            }

            if (remainder != 0)
            {
                throw new ArgumentException($"Invalid value: {itemValue}");
            }

            return result;
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

        private sealed class Flags : BookStatus
        {
            private readonly BookStatus[] items;

            public Flags(IEnumerable<BookStatus> items) 
                : base(items.Select(x => x.Value).Aggregate((s, t) => s | t), false)
            {
                this.items = items.ToArray();
            }
        }

        #endregion
    }
}