
**EntityState** has to be set manually...
...everytime I mutate something.
There is logic to avoid overriding *New* with *Updated*,
which is the beggining of DDD.

**Tax calculation on Invoice and Item**
When I change eg. InvoiceItem.Quantity, I have to remember to:
- CalculateMoney on InvoiceItem
- CalculateMoney on Invoice
This is the only way to have valid Invoice.

**InvoiceModel.Items** is public List, i.e. mutable.
Anyone can add any Item, without valdiation and totals calculation.

**MakeFinal** use case requires knowledge of:
- effects of Invoice creation process
- external systems, to implement those effects.
I'm an expert in invoicing, not Tax Administration's services.






