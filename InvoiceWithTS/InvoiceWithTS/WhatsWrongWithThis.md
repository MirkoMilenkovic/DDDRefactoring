
**EntityState** has to be set manually...
...everytime I mutate something.
There is logic to avoid overriding *New* with *Updated*,
which is the beggining of DDD.

**Invoice** and **InvoiceItem** can be invalid.
Logic for calculating tax is outside. 
I have to remember to call it, every time I change something.

**MakeFinal** use case requires knowledge of:
- effects of Invoice creation process
- external systems, to implement those effects.
I'm an expert in invoicing, not Tax Administration's services.




