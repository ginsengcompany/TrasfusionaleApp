﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrasfusionaleApp.Model
{
    public class Reparto
    {
        public string id { get; set; }
        public string nomeReparto { get; set; }
        public static readonly string restListaReparti = "http://192.168.125.31:3000/reparti/listareparti";
    }
}
