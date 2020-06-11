using Exam.Business.Common;
using Exam.DataSource;
using Exam.Entities.Common;
using System;
using System.Collections.Generic;



namespace Exam.Business
{
    public class BssUsers : BusinessBase
    {
        public BssUsers() { }

        public Exam.Entities.User Retrieve(string filter)
        {
            InitalizeMessage();

            AppendMessage(filter == "" , "Nombre usuario no es valido");

            if (IsValid)
                return new Repository().GetUser(filter);
            else
                EndMessage();
            throw new ArgumentException(GetMessage());

        }

        public bool AuthenticatePath(Exam.Entities.User data)
        {
            InitalizeMessage();

            AppendMessage(string.IsNullOrEmpty(data.Nombre), "Favor de indicar un usuario");

            AppendMessage(string.IsNullOrEmpty(data.Password), "Favor de indicar un password");

            bool result = false;

            if (IsValid)
            {
                result = DbsAuth.GetAuth(data.Nombre,data.Password);
            }
            else
            {
                throw new ArgumentException(Message.ToString());
            }

            return result;
        }

    }
}
