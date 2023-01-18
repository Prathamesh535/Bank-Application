using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    { 
        public double AmountTransfered { get; set; }
        public string TransactionId { get; set; }
        public string TransactionComment { get; set; }
        public Transaction() { }    
        public Transaction(double amountTransfered, string id, string transactionComment)
        {
            this.AmountTransfered = amountTransfered;
            this.TransactionId = id;
            this.TransactionComment = transactionComment;
        }
    }
}
