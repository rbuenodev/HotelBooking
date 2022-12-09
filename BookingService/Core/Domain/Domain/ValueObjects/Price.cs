﻿using Domain.Enums;

namespace Domain.ValueObjects
{
    public class Price
    {
        public decimal Value { get; set; }  
        public AcceptedCurrecies Currency { get; set; }
    }
}
