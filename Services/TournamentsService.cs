using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class TournamentsService
    {
        private readonly List<Tournaments> _tournaments = new();
        private int _nextId = 1;

        public IEnumerable<Tournaments> GetAll() => _tournaments;

        public Tournaments? GetById (int id)
        {
            foreach (var tournament in _tournaments)
            {
                return tournament;
            }
            return null;
        }
        public Tournaments? GetByTitle(string Title)
        {
            foreach (var tournament in _tournaments)
            {
                if (tournament.Title == Title)
                    return tournament;
            }
            return null;
        }
        public Tournaments? Create(Tournaments tournament)
        {
            tournament.Id = _nextId;
            _nextId++;
            _tournaments.Add(tournament);
            return tournament;
        }
        public 
    }
}
