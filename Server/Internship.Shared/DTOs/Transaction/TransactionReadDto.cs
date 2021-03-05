﻿using System;

namespace Internship.TransactionService.API.DTOs.Transaction
{
    public class TransactionReadDto
    {
        public string DebtorFirstName { get; set; }
        public string DebtorLastName { get; set; }
        public string DebtorAccountNumber { get; set; }
        public string DebtorBankId { get; set; }
        public string CreditorFirstName { get; set; }
        public string CreditorLastName { get; set; }
        public string CreditorAccountNumber { get; set; }
        public string CreditorBankId { get; set; }
        public decimal Amount { get; set; } = 0;
        public Guid TransactionId { get; set; }
    }
}