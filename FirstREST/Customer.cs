using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST
{
    public class Customer
    {
        private string name;
        private string address;
        private bool vip;
        public Customer(string name, string address, bool vip)
        {
            this.name = name;
            this.address = address;
            this.vip = vip;
        }
        public string GetAllValue()
        {
            return $"{name};{address};{ vip }";
        }
    }
}