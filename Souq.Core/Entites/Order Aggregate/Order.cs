﻿using Souq.Core.DataBase;
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, int deliveryMethodId, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethodId = deliveryMethodId;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }


        public ICollection<OrderItem> Items=new HashSet<OrderItem>();