using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SUMATEAPPT2.Interfaces
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
        SQLiteConnection GetConnection_VisitaOffLine();
    }
}
