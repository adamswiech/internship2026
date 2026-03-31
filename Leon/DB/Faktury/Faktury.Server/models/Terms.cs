using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Faktury.Server.models
{

    // implemented
    public class Terms
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public Contract Contract { get; set; }
        public OrderInfo Order { get; set; }

        public string DeliveryTerms { get; set; }

        public TransportInfo Transport { get; set; }

    }

    public class Contract
    {
        public int Id { get; set; }
        public DateTime ContractDate { get; set; }
        public string ContractNumber { get; set; }
    }

    public class OrderInfo
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
    }

    public class TransportInfo
    {
        public int Id { get; set; }

        public int TransportType { get; set; } // <RodzajTransportu>1</RodzajTransportu>
        public Carrier Carrier { get; set; }

        public string TransportOrderNumber { get; set; }
        public int CargoDescription { get; set; }
        public string PackagingUnit { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Address ShipFrom { get; set; }
        public Address ShipVia { get; set; }
        public Address ShipTo { get; set; }
    }



    public class Carrier
    {
        public int Id { get; set; }

        public string CountryCode { get; set; }
        public string TaxId { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
