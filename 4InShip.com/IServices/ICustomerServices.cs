using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _4InShip.com.Repository;

namespace _4InShip.com.IServices
{
    public interface ICustomerServices<T, TS> where T : class
    {
        string CustomerSignUp(T entity);
        TS GetAddressBookData(int ID);
        T GetTblCustomerIdByUserNamePassword(T entity);
        string GetTblCustomerIdByUserNamePasswords(string entity);
        string CustomerPayBillingAddress(TS entity);
       string  UpdateUserEmailPassword(string entity);
    }
}