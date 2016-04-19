using IIM.ViewModels;
using IIM.ViewModels.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIM.Models;
using IIM.Models.Domain;

namespace IIM.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IUserRepository userRepository, IReservationRepository reservationRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
        }

        
        public ActionResult Index(ApplicationUser user)
        {
            return View(user.Reservations.Select(r => new ReservationViewModel(r)));
        }

<<<<<<< HEAD
       

=======
        [HttpPost]
        public ActionResult ChangeReservationRange()
        {
            throw new NotImplementedException();
        }
>>>>>>> 4158838f9fab0cddf68e9e0b857ba899950be783
    }
}