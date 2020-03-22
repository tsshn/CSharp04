﻿using System;

 namespace Yatsyshyn.Auxiliary.Exceptions
{
    public class EmailExc : Exception
    {
        public EmailExc(string email) : base(message: $"Incorrect email: {email}")
        {
        }
    }
}