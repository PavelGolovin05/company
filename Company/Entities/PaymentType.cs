﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    class PaymentType
    {
        private int id;
        private string paymentType;

        public PaymentType(int id, string paymentType)
        {
            this.id = id;
            this.paymentType = paymentType;
        }

        public int Id { get => id; set => id = value; }
        public string GetPaymentType { get => paymentType; set => paymentType = value; }

        public override string ToString()
        {
            return "Id: " + id + " Тип оплаты: " + paymentType;
        }
    }
}
