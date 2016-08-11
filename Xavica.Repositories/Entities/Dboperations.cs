using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.Shared.Enums
{
    public enum DBOperation
    {
        Unknown = 0,
        Contains = 1,
        DoesNotContain = 2,
        StartsWith = 3,
        EndsWith = 4,
        EqualTo = 5,
        NotEqualTo = 6,
        GreaterThan = 7,
        GreaterThanOrEqualTo = 8,
        LessThan = 9,
        LessThanOrEqualTo = 10,
        In = 11,
        NotIn = 12,
        IsNULL = 13,
        IsNotNull = 14,
        DateEqual = 15,
        DateNotEqual = 16,
        DateGreaterThan = 17,
        DateGreaterThanOrEqual = 18,
        DateLessThan = 19,
        DateLessThanOrEqual = 20,
        OrderBy = 21,
        DoesNotStartsWith = 22,
        DoesNotEndsWith = 23
    }


    public enum SortOperation
    {
        None = 0,
        Ascending = 1,
        Descending = 2
    }

    public enum LogicalOperation
    {
        None = 0,
        AND = 1,
        OR = 2
    }
    public enum PairOperation
    {
        None = 0,
        Open = 1,
        Close = 2
    }
}
