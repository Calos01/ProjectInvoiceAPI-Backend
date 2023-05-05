﻿namespace ProjectInvoiceAPI_Backend.DTO
{
    public class CustomerDTO
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phoneno { get; set; }

        public string? Email { get; set; }

        public string? Status { get; set; }
    }
}
