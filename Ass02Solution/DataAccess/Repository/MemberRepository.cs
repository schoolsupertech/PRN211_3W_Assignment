using BusinessObject.Models;

namespace DataAccess.Repository;
public class MemberRepository : IMemberRepository {
    public void DeleteMember(int id) => MemberDAO.Instance.Remove(id);

    public List<TblMember> GetMemberByCityAndCountry(string city, string country) => MemberDAO.Instance.GetMemberByCityAndCountry(city, country);

    public TblMember? GetMemberById(int id) => MemberDAO.Instance.GetMemberById(id);

    public IEnumerable<TblMember> GetMemberByIdList(int id) => MemberDAO.Instance.GetMemberByIdList(id);

    public TblMember? GetMemberByName(string name) => MemberDAO.Instance.GetMemberByName(name);

    public IEnumerable<TblMember> GetMembers() => MemberDAO.Instance.GetMemberList();

    public void InsertMember(TblMember member) => MemberDAO.Instance.AddNew(member);

    public void UpdateMember(TblMember member) => MemberDAO.Instance.Update(member);
}
