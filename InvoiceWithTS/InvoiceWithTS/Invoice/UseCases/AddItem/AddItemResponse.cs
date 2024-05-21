﻿using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public record UpdateItemResponse(
        InvoiceDTO Invoice, 
        IEnumerable<InvoiceItemDTO> ItemList);
}
