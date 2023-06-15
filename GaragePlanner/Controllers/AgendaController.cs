using Core;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using DAL;
using Domain.dto;
using Domain.utils;

namespace GaragePlanner.Controllers
{
    public class AgendaController : Controller
    {
        private AppointmentCollection appointmentCollection;


        public AgendaController(AppointmentCollection collection)
        {
            appointmentCollection = collection;
        }

        public IActionResult Index(DateTime dataAndTime)
        {
            try
            {


                AgendaViewModel model = new();

                IReadOnlyList<Day> days = appointmentCollection.Days;

                foreach (var day in days)
                {
                    DayViewModel dayViewModel = new() { Date = day.DateOfDay };
                    foreach (TimeSlot timeslot in day.TimeSlots)
                    {
                        TimeSlotViewModel timeslotViewModel = new()
                        {
                            Time = timeslot.StartTime,
                            IsAvailable = timeslot.IsAvailable(),
                        };
                        dayViewModel.TimeSlots.Add(timeslotViewModel);
                    }

                    model.Days.Add(dayViewModel);
                }

                return View(model);
            }
            catch (CouldNotReadDataException)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };

                return View("Error", errorViewModel);
            }

        }



    }
}
