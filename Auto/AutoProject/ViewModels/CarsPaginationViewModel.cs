﻿using Front.Areas.Cars.Models;
using Front.ViewModels;

namespace AutoProject.ViewModels
{
    public class CarsPaginationViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Car> Cars { get; set; }

        public CarsPaginationViewModel()
        {
            PageViewModel = new PageViewModel(0, 1, 5);
            Cars = new List<Car>();
        }
    }
}
