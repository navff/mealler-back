using Tests.ToolsTests;
using web.api.DataAccess;

namespace Tests.Creators
{
    public class Creator
    {
        private readonly AppDbContext _context;

        public Creator()
        {
            var builder = new DIServiceBuilder();
            _context = builder.GetService<AppDbContext>();
            UsersCreator = new UsersCreator(_context);
            TeamsCreator = new TeamsCreator(_context);
            RecipesCreator = new RecipesCreator(_context);
            EventsCreator = new EventsCreator(_context);
            ReferenceIngredientCreator = new ReferenceIngredientCreator(_context);
        }

        public UsersCreator UsersCreator { get; }
        public TeamsCreator TeamsCreator { get; }

        public RecipesCreator RecipesCreator { get; }
        public EventsCreator EventsCreator { get; }

        public ReferenceIngredientCreator ReferenceIngredientCreator { get; }

        public AppDbContext GetContext()
        {
            return _context;
        }
    }
}