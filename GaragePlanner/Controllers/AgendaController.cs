﻿using Core;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using DAL;
using Domain.dto;
using Domain.utils;
using System;

namespace GaragePlanner.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AppointmentCollection _appointmentCollection;


        public AgendaController(AppointmentCollection collection)
        {
            _appointmentCollection = collection;
        }

        public IActionResult Index(DateTime dataAndTime)
        {
            try
            {
                _appointmentCollection.LoadAgenda();

                AgendaViewModel model = new();

                IReadOnlyList<Day> days = _appointmentCollection.Days;

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
            catch (DalException exception)
            {
                Console.WriteLine(exception);
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };

                return View("Error", errorViewModel);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Something went wrong, please contact support"
                };

                return View("Error", errorViewModel);
            }

        }



    }
}
