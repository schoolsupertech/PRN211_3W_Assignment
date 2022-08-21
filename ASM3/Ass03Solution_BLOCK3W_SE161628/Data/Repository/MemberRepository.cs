using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(int p)
        {
            MemberDAO.DeleteMember(p);
        }

        public List<Member> GetListByID(int p) => MemberDAO.GetListByID(p);

        public Member GetListByIDs(int p) => MemberDAO.GetListByIDs(p);


        public List<Member> GetMember() => MemberDAO.GetMember();


        public Member GetMemberfromEmail(Member a) => MemberDAO.GetMemberfromEmail(a);

        public List<Member> GetMemberfromEmails(string a) => MemberDAO.GetMemberfromEmails(a);


        public List<Member> LoginMember(string p, string a) => MemberDAO.LoginMember(p, a);


        public void SaveMember(Member p)
        {
            MemberDAO.SaveMember(p);
        }

        public void UpdateMember(Member p)
        {
            MemberDAO.UpdateMember(p);
        }
    }

}