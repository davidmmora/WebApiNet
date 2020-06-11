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

    }
}
