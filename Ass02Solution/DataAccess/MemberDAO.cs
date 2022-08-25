using BusinessObject.Models;

namespace DataAccess {
    public class MemberDAO {
        // Using Singleton Pattern
        private static MemberDAO instance;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance {
            get {
                lock(instanceLock) {
                    if(instance == null) {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<TblMember> GetMemberList() {
            List<TblMember> Members;
            try {
                using SalesManagementContext mem = new SalesManagementContext();
                Members = mem.TblMembers.ToList();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
            return Members;
        }

        public TblMember? GetMemberById(int memID) {
            List<TblMember> MemberList = GetMemberList();

            // Using LINQ to Object
            TblMember? member = MemberList.SingleOrDefault(mem => mem.MemberId == memID);
            return member;
        }

        public List<TblMember> GetMemberByIdList(int memID) {
            List<TblMember> MemberList = GetMemberList();
            TblMember member = MemberList.SingleOrDefault(mem => mem.MemberId == memID);
            MemberList.Clear();
            MemberList.Add(member);
            return MemberList;
        }

        public TblMember? GetMemberByName(string memberName) {
            List<TblMember> memberList = GetMemberList();

            // Using LINQ to Object
            TblMember? member = memberList.SingleOrDefault(mem => mem.CompanyName == memberName);
            return member;
        }

        public void AddNew(TblMember member) {
            try {
                using SalesManagementContext salesManagementContext = new SalesManagementContext();
                salesManagementContext.TblMembers.Add(member);
                salesManagementContext.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TblMember member) {
            try {
                using SalesManagementContext salesManagementContext= new SalesManagementContext();
                //salesManagementContext.TblMembers.Update(member);
                salesManagementContext.Entry<TblMember>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                salesManagementContext.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int memID) {
            try {
                using SalesManagementContext salesManagementContext = new SalesManagementContext();
                var m = salesManagementContext.TblMembers.SingleOrDefault(mem => mem.MemberId == memID);
                salesManagementContext.Remove(m);
                salesManagementContext.SaveChanges();
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public List<TblMember> GetMemberByCityAndCountry(string? city, string? country) {
            List<TblMember> tblMembers = new List<TblMember>();
            List<TblMember> memberList = GetMemberList();

            city = (city.ToLower() != "all city" && city.ToLower() != "city")? city : null;
            country = (country.ToLower() != "all country" && country.ToLower() != "country")? country : null;

            if(city != null) {
                memberList = memberList.Where(mem => mem.City.ToLower().Contains(city.ToLower())).ToList();
            }
            if(country != null) {
                memberList = memberList.Where(mem => mem.Country.ToLower().Contains(country.ToLower())).ToList();
            }

            return memberList;
        }
    }
}