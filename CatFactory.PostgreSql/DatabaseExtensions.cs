﻿using System;
using System.Collections.Generic;
using System.Linq;
using CatFactory.ObjectRelationalMapping;

namespace CatFactory.PostgreSql
{
    public static class DatabaseExtensions
    {
        public static IEnumerable<DbObject> GetTables(this Database database)
            => database.DbObjects.Where(item => new string[] { "table" }.Contains(item.Type));

        public static IEnumerable<DbObject> GetViews(this Database database)
            => database.DbObjects.Where(item => new string[] { "view" }.Contains(item.Type));

        public static IEnumerable<DbObject> GetSequences(this Database database)
            => database.DbObjects.Where(item => new string[] { "sequence" }.Contains(item.Type));
    }
}
