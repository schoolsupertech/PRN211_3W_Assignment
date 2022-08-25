using BusinessObject.Models;

namespace DataAccess.Repository; 
public interface IMemberRepository {
    IEnumerable<TblMember> GetMembers();
    IEnumerable<TblMember> GetMemberByIdList(int id);
    TblMember GetMemberById(int id);
    TblMember GetMemberByName(string name);
    void InsertMember(TblMember member);
    void UpdateMember(TblMember member);
    void DeleteMember(int id);
    List<TblMember> GetMemberByCityAndCountry(string city, string country);
}
