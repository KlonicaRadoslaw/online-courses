﻿using OnlineCourses.Entities;
using System.Diagnostics.CodeAnalysis;

namespace OnlineCourses.Comparers
{
    public class CompareCategories : IEqualityComparer<Category>
    {
        public bool Equals(Category x, Category y)
        {
            if (y == null) return false;

            if (x.Id == y.Id)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Category obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
