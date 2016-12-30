using _4InShip.com.Areas.User.Models;
using _4InShip.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _4InShip.com.Areas.User.IServices
{
    public interface ISettingService<T, T1> where T : class
    {
        string CreateUpdateNewAddress(T entity);
        List<T> GetAddressBookData(int? Id, bool Is_Default);
        T GetAddressBookData(int Id);
        string DeleteSingleAddressByID(int id);
        string ChangeUserPassword(ChangePasswordModel entity);
        string ChangeUserEmail(ChangeEmailId entity);
        bool CheckDefaultAddress(string entity, string e);
        bool CheckPassword(string entity);
        T1 GetTblCustomerData();
        string CreateUpdateBillingAddress(tblBillingAddress entity);
        string insertrewardpoint(int entity);

    }
}