﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatLoco.Models.Error_Model
{
    public class ErrorsModel
    {
        public List<ErrorModel> Errors = new List<ErrorModel>();

        public void AddError(string msg)
        {
            Errors.Add(new ErrorModel(msg));
        }

        public bool Logout { get; set; }
    }
}