using System;
using System.Collections.Generic;
using GLib;
using Common.Domain;
using Common.Interfaces;
using Common.Observer;
using Server.Repo;
using Server.Repo.Interfaces;
using DateTime = System.DateTime;

namespace Server.srv
{
    public class Service :  AbstractObserable, IService
    {
        private string _conn;
        private IArtistRepo _artistRepo = null;
        private IEmployeeRepo _employeeRepo = null;
        private IOfficeRepo _officeRepo = null;
        private IShowRepo _showRepo = null;
        private ITransactionRepo _transactionRepo = null;
        
        public Service(string conn)
        {
            this._conn = conn;
            _artistRepo = new ArtistDbRepo(_conn);
            _employeeRepo = new EmployeesDbRepo(_conn);
            _officeRepo = new OfficeDbRepo(_conn);
            _showRepo = new ShowDbRepo(_conn);
            _transactionRepo = new TransDbRepo(_conn);
        }

        public Show FindOneShow(long id)
        {
            return _showRepo.FindOne(id);
        }

        public IList<Show> GetAllListShows()
        {
            return _showRepo.GetAllList();
        }
        
        public Employee EmployeeByUser(string user)
        {
            return _employeeRepo.GetEmployeeByUser(user);
        }

        public Employee Login(string user, string pass)
        {
            if (_employeeRepo.UsernameExists(user)) {
                if (_employeeRepo.GetPasswordByUser(user).Equals(pass)) {
                    return _employeeRepo.GetEmployeeByUser(user);
                } else {
                    return null;
                }
            }
            return null;
        }

        public IList<Show> FilterShowsByDate(DateTime date)
        {
            return _showRepo.FilterByDate(date);
        }

        public Transaction BuyTicket(Show s, int no, string client)
        {
            if (s.TicketNumber == 0) throw new Exception("There are no more tickets for this show!");
           
            var trans = new Transaction(client, s, DateTime.Now, no);
            var x = _transactionRepo.Save(trans);
            if (x == null)
            {
                s.DecreaseTicketNumber(no);
                _showRepo.Update(s);
            }

            return trans;
        }

        public Employee GetEmployeeByUser(string user)
        {
            return _employeeRepo.GetEmployeeByUser(user);
        }


        // public Artist FindOneArtist(long id)
        // {
        //     return _artistRepo.FindOne(id);
        // }
        //
        // public IDictionary<long, Artist> FindAllArtist()
        // {
        //     return _artistRepo.FindAll();
        // }
        //
        // public IList<Artist> GetAllListArtist()
        // {
        //     return _artistRepo.GetAllList();
        // }
        //
        // public Artist SaveArtist(List<string> parameteres)
        // {
        //     return _artistRepo.Save(new Artist(long.Parse(parameteres[0]), parameteres[1], parameteres[2]));
        // }
        //
        // public Artist DeleteArtist(long id)
        // {
        //     return _artistRepo.Delete(id);
        // }
        //
        // public Artist UpdateArtist(List<string> parameteres)
        // {
        //     return _artistRepo.Update(new Artist(long.Parse(parameteres[0]), parameteres[1], parameteres[2]));
        // }
        //
        // public IList<Artist> FilterByNameArtist(string name)
        // {
        //     return _artistRepo.FilterByName(name);
        // }
        //
        // public IList<Artist> FilterByGenreArtist(string genre)
        // {
        //     return _artistRepo.FilterByGenre(genre);
        // }
        //
        //
        //
        // public Employee FindOneEmployee(long id)
        // {
        //     return _employeeRepo.FindOne(id);
        // }
        //
        // public IDictionary<long, Employee> FindAllEmployees()
        // {
        //     return _employeeRepo.FindAll();
        // }
        //
        // public IList<Employee> GetAllListEmployees()
        // {
        //     return _employeeRepo.GetAllList();
        // }
        //
        // public Employee SaveEmployee(List<string> parameteres)
        // {
        //     return _employeeRepo.Save(new Employee(long.Parse(parameteres[0]),
        //         parameteres[1],
        //         parameteres[2], new Office(long.Parse(parameteres[3]), parameteres[4]),
        //         parameteres[5],
        //         parameteres[6]
        //         )
        //     );
        // }
        //
        // public Employee DeleteEmployee(long id)
        // {
        //     return _employeeRepo.Delete(id);
        // }
        //
        // public Employee UpdateEmployee(List<string> parameteres)
        // {
        //     return _employeeRepo.Update(new Employee(long.Parse(parameteres[0]),
        //         parameteres[1],
        //         parameteres[2], new Office(long.Parse(parameteres[3]), parameteres[4]),
        //         parameteres[5],
        //         parameteres[6]
        //         )
        //     );
        // }
        //
        // public IList<Employee> FilterByNameEmployees(string name)
        // {
        //     return _employeeRepo.FilterByName(name);
        // }
        //
        // public IList<Employee> FilterByPositionEmployees(string position)
        // {
        //     return _employeeRepo.FilterByPosition(position);
        // }
        //
        // public IList<Employee> FilterByOffice(long office)
        // {
        //     return _employeeRepo.FilterByOffice(office);
        // }
        //
        //
        //
        //
        // public IDictionary<long, Show> FindAllsShows()
        // {
        //     return _showRepo.FindAll();
        // }
        // public Show SaveShow(List<string> parameteres)
        // {
        //     return _showRepo.Save(new Show(long.Parse(parameteres[0]),
        //         new Artist(long.Parse(parameteres[1]),
        //             parameteres[2],
        //             parameteres[3]), 
        //         int.Parse(parameteres[4]), 
        //         DateTime.Parse(parameteres[5])));
        // }
        //
        // public Show DeleteShow(long id)
        // {
        //     return _showRepo.Delete(id);
        // }
        //
        // public Show UpdateShow(List<string> parameteres)
        // {
        //     return _showRepo.Update(new Show(long.Parse(parameteres[0]),
        //         new Artist(long.Parse(parameteres[1]),
        //             parameteres[2],
        //             parameteres[3]), 
        //         int.Parse(parameteres[4]),
        //         DateTime.Parse(parameteres[5])));
        // }
        //
        // public IList<Show> FilterByArtist(long artist)
        // {
        //     return _showRepo.FilterByArtist(artist);
        // }
        //
        //
        // public Office FindOneOffice(long id)
        // {
        //     return _officeRepo.FindOne(id);
        // }
        //
        // public IDictionary<long, Office> FindAllOffices()
        // {
        //     return _officeRepo.FindAll();
        // }
        //
        // public IList<Office> GetAllListOffices()
        // {
        //     return _officeRepo.GetAllList();
        // }
        //
        // public Office SaveOffice(List<string> parameteres)
        // {
        //     return _officeRepo.Save(new Office(long.Parse(parameteres[0]),parameteres[1]));
        // }
        //
        // public Office DeleteOffice(long id)
        // {
        //     return _officeRepo.Delete(id);
        // }
        //
        // public Office UpdateOffice(List<string> parameteres)
        // {
        //     return _officeRepo.Update(new Office(long.Parse(parameteres[0]),parameteres[1]));
        // }
        //
        // public IList<Office> FilterByLocation(string office)
        // {
        //     return _officeRepo.FilterByLocation(office);
        // }
        //
        //other services
        }
}