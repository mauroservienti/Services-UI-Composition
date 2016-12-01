﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketing.Data.Models;

namespace Marketing.Data.Migrations
{
    public static class SeedData
    {
        public static List<ProductDescription> ProductDescriptions()
        {
            return new List<ProductDescription>
            {
                new ProductDescription() {
                    StockItemId = "StockItems/1",
                    Title = "Halloween Pumpkin",
                    Description ="Fantastic Halloween Pumpkin, with a smiling face, white battery led included.",
                    ImageUrl =""},
                new ProductDescription() {
                    StockItemId = "StockItems/2",
                    Title = "Banana Trolley",
                    Description ="",
                    ImageUrl =""},
                new ProductDescription() {
                    StockItemId = "StockItems/3",
                    Title = "USB-C Universal power supply",
                    Description ="",
                    ImageUrl =""},
                new ProductDescription() {
                    StockItemId = "StockItems/4",
                    Title = "Learn ServiceFabric",
                    Description ="Editor:MS-Press|Title:Learn ServiceFabric|Author:Alessandro Melchiori",
                    ImageUrl =""},
                new ProductDescription() {
                    StockItemId = "StockItems/5",
                    Title = "LH740 Airplane Model",
                    Description ="",
                    ImageUrl ="/assets/Modell_LH_B748_200P.jpg"}
            };
        }
    }
}
