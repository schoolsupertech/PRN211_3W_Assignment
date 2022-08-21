using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        List<Member> GetMember();
        void SaveMember(Member p);
        List<Member> LoginMember(string p , string a);
        List<Member> GetListByID(int p);
        Member GetListByIDs(int p);
        Member GetMemberfromEmail(Member a);

        List<Member> GetMemberfromEmails(string a);
        void UpdateMember(Member p);
        void DeleteMember(int p);
    }
}
