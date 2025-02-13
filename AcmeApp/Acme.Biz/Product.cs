﻿using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory.
    /// </summary>
    public class Product
    {
        #region Constructors
        public Product()
        {
            #region Dictionary
            var states = new Dictionary<string, string>
            {
                { "CA", "California" },
                { "WA", "Washington" },
                { "NY", "New York" }
            };

            //states.Add("CA", "California");
            //states.Add("WA", "Washington");
            //states.Add("NY", "New York");

            Console.WriteLine(states);
            #endregion

            #region Generic list
            var colorOptions = new List<string> { "Red", "Espresso", "White", "Navy" };

            //var colorOptions = new List<string>();
            //colorOptions.Add("Red");
            //colorOptions.Add("Espresso");
            //colorOptions.Add("White");
            //colorOptions.Add("Navy");

            colorOptions.Insert(2, "Purple");
            colorOptions.Remove("White");

            Console.WriteLine(colorOptions);
            #endregion

            #region Array
            //string[] colorOptions = { "Red", "Espresso", "White", "Navy" };

            //var colorOptions = new string[4] { "Red", "Espresso", "White", "Navy" };

            //var colorOptions = new string[4];
            //colorOptions[0] = "Red";
            //colorOptions[1] = "Espresso";
            //colorOptions[2] = "White";
            //colorOptions[3] = "Navy";

            //Console.WriteLine(colorOptions[1]);

            //var brownIndex = Array.IndexOf(colorOptions, "Espresso");

            //colorOptions.SetValue("Blue", 3);

            //for (int i = 0; i < colorOptions.Length; i++)
            //{
            //    colorOptions[i] = colorOptions[i].ToLower();
            //}

            //foreach (string color in colorOptions)
            //{
            //    Console.WriteLine($"The color is {color}");
            //}
            #endregion

        }
        public Product(int productId,
                        string productName,
                        string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
        }
        #endregion

        #region Properties
        public DateTime? AvailabilityDate { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }

        private string productName;
        public string ProductName
        {
            get {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";

                }
                else
                {
                    productName = value;

                }
            }
        }

        private Vendor productVendor;
        public Vendor ProductVendor
        {
            get {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        public string ValidationMessage { get; private set; }

        #endregion

        /// <summary>
        /// Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up the cost.</param>
        /// <returns></returns>
        public OperationResult<decimal> CalculateSuggestedPrice(decimal markupPercent)
        {
            var message = "";
            if (markupPercent <= 0m)
            {
                message = "Invalid markup percentage";
            }
            else if (markupPercent < 10)
            {
                message = "Below recommanded markup percentage";
            }
            var value = this.Cost + (this.Cost * markupPercent / 100);
            var operationResult = new OperationResult<decimal>(value, message);
            return operationResult;
        }

        //public decimal CalculateSuggestedPrice(decimal markupPercent) =>
        //     this.Cost + (this.Cost * markupPercent / 100);

        public override string ToString()
        {
            return this.ProductName + " (" + this.ProductId + ")";
        }
    }
}
