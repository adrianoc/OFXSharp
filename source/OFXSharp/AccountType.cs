﻿using System.ComponentModel;

namespace OFXSharp
{
    public enum AccountType
    {
        [Description("Bank Account")]
        BANK,
        [Description("Credit Card")]
        CC,
        [Description("Accounts Payable")]
        AP,
        [Description("Accounts Recievable")]
        AR,
        [Description("Investment")]
        INVESTMENT,
        [Description("Security List")]
        SECURITYLIST,
        NA,
    }
}