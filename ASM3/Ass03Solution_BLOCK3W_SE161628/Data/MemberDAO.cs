using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        public static List<Member> GetMember()
        {
            var list = new List<Member>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.Members.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static Member GetMemberfromEmail(Member a)
        {
            var list = new Member();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.Members.SingleOrDefault(x=> x.Email==a.Email);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public static List<Member> GetMemberfromEmails(string a)
        {
            var list = new List<Member>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    list = db.Members.Where(x => x.Email == a).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }



        public static void SaveMember(Member p)
        {

            try
            {
                Member pro = GetListByIDs(p.MemberId);
                if (pro == null)
                {
                    using var db = new FStoreDBContext();

                    db.Members.Add(p);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception(" the Member was Exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static List<Member> LoginMember(string p, string a)
        {

            var mem = new List<Member>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    mem = db.Members.Where(c => c.Email == p && c.Password ==a).ToList();

                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        public static List<Member> GetListByID(int p)
        {
            var mem = new List<Member>();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    mem = db.Members.Where(c => c.MemberId==p).ToList(); ;
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }
        public static Member GetListByIDs(int p)
        {
            var mem = new Member();
            try
            {
                using (var db = new FStoreDBContext())
                {
                    mem = db.Members.SingleOrDefault(c => c.MemberId == p);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }



        public static void UpdateMember(Member p)
        {

            try
            {
                if (GetListByID(p.MemberId) != null)
                {
                    using (var db = new FStoreDBContext())
                    {

                        // db.Entry<Member>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        db.Members.Update(p);
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("the member is already update");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public static void DeleteMember(int p)
        {

            try
            {
                using (var db = new FStoreDBContext())
                {
                    var b = db.Members.SingleOrDefault(c => c.MemberId == p);
                    db.Members.Remove(b);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }

}
