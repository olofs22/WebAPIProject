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
        public bool Update(int id, Tournaments tournament)
        {
            Tournaments? existingTournament = null;
            {
                foreach (Tournaments candidate in _tournaments)
                {
                    if (candidate.Id == id)
                    {
                        existingTournament = candidate;
                        break;
                    }
                }
                if (existingTournament == null)
                    return false;

                existingTournament.Title = tournament.Title;
                existingTournament.Description = tournament.Description;
                return true;
            }
        }
        public bool Delete(int id)
        {
            for (int i = _tournaments.Count - 1; i >= 0; 1--)
            {
                if (_tournaments[i].Id == id)
                {
                    _tournaments.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
