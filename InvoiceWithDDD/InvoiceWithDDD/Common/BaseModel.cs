namespace InvoiceWithDDD.Common
{
    public abstract class BaseModel
    {
        /// <summary>
        /// DDD
        /// </summary>
        public int Id { get; protected set; }

        public void SetId(int id)
        {
            Id = id;
        }
        // END DDD


        private EntityStates _entityState = EntityStates.Loaded;


        // Example of business logic in Model,
        // Our model is not fully anemic
        public EntityStates EntityState
        {
            get
            {
                return _entityState;
            }
            protected set
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

        protected BaseModel(
            int id,
            EntityStates entityState)
        {
            Id = id;
            EntityState = entityState;
        }

    }
}
