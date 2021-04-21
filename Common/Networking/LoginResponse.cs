using System;
using System.Collections;
using System.Collections.Generic;
using Common.Domain;

namespace Common.Networking
{
    [Serializable]
    public class LoginResponse:Response
    {
        private Employee _emp;

        public LoginResponse(Employee emp)
        {
            _emp = emp;
        }

        public Employee Emp
        {
            get => _emp;
            set => _emp = value;
        }
    }
}