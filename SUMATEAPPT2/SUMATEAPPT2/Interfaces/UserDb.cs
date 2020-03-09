using SQLite;
using SUMATEAPPT2.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SUMATEAPPT2.Interfaces
{
    public class UserDb
    {
        private SQLiteConnection conn;
        public UserDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<UserFS>();
            conn.CreateTable<Cvisita>();
        }
        public IEnumerable<Cvisita> GetMember_VisitaOffLine(int id) {

            var member = (from mem in conn.Table<Cvisita>() where mem.id == id select mem);
            return member.ToList();
        }
        public IEnumerable<Cvisita> GetMembers_visitas()
        {
            var members = (from mem in conn.Table<Cvisita>() select mem);
            return members ;
        }

        public string AddMember_visitas(Cvisita member)
        {
            try
            {
                conn.Insert(member);
                return "success baby bluye ;*";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public void DeleteMember_visitas(int ID)
        {
            conn.Delete<Cvisita>(ID);
        }

        public void DropTbMember_visitas()
        {
            conn.DropTable<Cvisita>();
        }
         //



        public IEnumerable<UserFS> GetMembers()
        {
            var members = (from mem in conn.Table<UserFS>() select mem);
            return members.ToList();
        }

        public string AddMember(UserFS member)
        {
            try
            {
                conn.Insert(member);
                return "success baby bluye ;*";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }

        public void DeleteMember(int ID)
        {
            conn.Delete<UserFS>(ID);
        }

        public void DropTbMember()
        {
            conn.DropTable<UserFS>();
        }
    }
}
