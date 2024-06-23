using InvoiceWithDDD.Common;

namespace InvoiceWithDDD.Invoice.BusinessModel
{
    public abstract class BaseModel
    {
        public required int Id { get; set; }

        private EntityStates _entityState = EntityStates.Loaded;

        // Example of business logic in Model,
        // Our model is not fully anemic
        public required EntityStates EntityState
        {
            get
            {
                return _entityState;
            }
            set
            {
                // Business logic: If something is New, we want to keep it New, even if we are sending Update
                // This is to force INSERT for New entity.
                if (_entityState == EntityStates.New)
                {
                    if (value == EntityStates.Updated)
                    {
                        // keep it new
                        _entityState = EntityStates.New;

                        return;
                    }
                }

                _entityState = value;
            }
        }
    }
}
