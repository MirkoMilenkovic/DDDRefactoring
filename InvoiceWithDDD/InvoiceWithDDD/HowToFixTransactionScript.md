See *// DDD* comments in code for changes.

**EntityState** has to be set manually...
...everytime I mutate something.
There is logic to avoid overriding *New* with *Updated*,
which is the beggining of DDD.

**Resolution**: move EntityState.Set into Entity itself. 
This breaks the code immediatelly. 
We can fix it by calling EntytyState.Set on every property.Set, but that sucks.
So, let's make each property.Set private, and replace Setters with methods.
This is a big change!!!
*required* properties are unusable, 
we have to make constructors.

**Tax calculation on Invoice and Item**
When I change eg. InvoiceItem.Quantity, I have to remember to:
- CalculateMoney on InvoiceItem
- CalculateMoney on Invoice

This is the only way to have valid Invoice.

**Resolution**
CalculateMoney methods are moved to InvoiceItem and Invoice iself.
Any method that changes money (eg. AddItem, UpdateQuantity), will call CalculateMoney.
So, CommandHandler does not know that money needs to be calculated.


**InvoiceModel.Items** is public List, i.e. mutable.
Anyone can add any Item, without valdiation and totals calculation.

**Resolution**
InvoiceModel has List<InvoiceItemModel> ItemsPrivate{get;}.
IEnumerable<InvoiceItemModel> Items{get;} is read-only wrapper.

**MakeFinal** use case requires knowledge of:
- effects of Invoice creation process
- external systems, to implement those effects.
I'm an expert in invoicing, not Tax Administration's services.

**Resolution**
Remains unresolved, for now.





