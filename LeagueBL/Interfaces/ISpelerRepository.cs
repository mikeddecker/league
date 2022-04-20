﻿using LeagueBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Interfaces {
    public interface ISpelerRepository {
        Speler SchrijfSpelerInDB(Speler speler);
        bool HeeftSpeler(Speler speler);
    }
}